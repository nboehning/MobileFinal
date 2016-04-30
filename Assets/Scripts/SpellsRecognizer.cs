using UnityEngine;
using System.Collections.Generic;

public static class SpellsRecognizer {

    public static float Compare(List<Vector2> points, List<Vector2>[][] trainingSets, int resolution, out int index, float scaleSize = 250f)
    {
        float scoreOfBestMatch;
        points = Read(points, resolution, scaleSize);
        Recognize(points, trainingSets, out index, out scoreOfBestMatch, scaleSize);


        return scoreOfBestMatch;
    }

    #region Comparison

    static void Recognize(List<Vector2> points, List<Vector2>[][] templates, out int indexOfBestMatch, out float scoreOfBestMatch, float scaleSize)
    {
        scoreOfBestMatch = 0;
        indexOfBestMatch = 0;

        for(int i = 0; i < templates.Length; i ++)
        {
            float score = RecognizeSingleShape(points, templates[i], scaleSize);
            if (score > scoreOfBestMatch)
            {
                indexOfBestMatch = i;
                scoreOfBestMatch = score;
            }
        }
    }

    static float RecognizeSingleShape(List<Vector2> points, List<Vector2>[] shape, float scaleSize)
    {
        float bestDistance = float.MaxValue;
        foreach (List<Vector2> trainingSet in shape)
        {
            float distance = DistanceAtBestAngle(points, trainingSet);
            bestDistance = Mathf.Min(bestDistance, distance);
        }

        float bestScore = 1 - bestDistance / (.5f * scaleSize * Mathf.Sqrt(2));
        return bestScore;
    }

    static float DistanceAtBestAngle(List<Vector2> candidate, List<Vector2> template, float minAngle = -45f, float maxAngle = 45f, float angleIncrement = 2f)
    {
        float uglyNumber = .5f * (-1 + Mathf.Sqrt(5));
        float testAngle1 = uglyNumber * minAngle + (1 - uglyNumber) * maxAngle;
        float testAngle2 = uglyNumber * maxAngle + (1 - uglyNumber) * minAngle;
        float distance1 = DistanceAtAngle(candidate, template, testAngle1);
        float distance2 = DistanceAtAngle(candidate, template, testAngle2);
        while (Mathf.Abs(testAngle1 - testAngle2) > angleIncrement)
        {
            if (distance1 < distance2)
            {
                maxAngle = testAngle2;
                testAngle2 = testAngle1;
                distance2 = distance1;
                testAngle1 = uglyNumber * minAngle + (1 - uglyNumber) * maxAngle;
                distance1 = DistanceAtAngle(candidate, template, testAngle1);
            }
            else
            {
                minAngle = testAngle1;
                testAngle1 = testAngle2;
                distance1 = distance2;
                testAngle2 = uglyNumber * maxAngle + (1 - uglyNumber) * minAngle;
                distance2 = DistanceAtAngle(candidate, template, testAngle2);
            }
        }

        return Mathf.Min(distance1, distance2);
    }

    static float DistanceAtAngle(List<Vector2> candidate, List<Vector2> template, float angle)
    {
        List<Vector2> newPoints = RotateBy(candidate, angle, Vector2.zero);
        return PathDistance(newPoints, template);
    }

    static float PathDistance(List<Vector2> candidate, List<Vector2> template)
    {
        float distance = 0;
        for (int i = 0; i < candidate.Count; i++)
        {
            distance += (candidate[i] - template[i]).magnitude;
        }
        return (distance / candidate.Count);
    }

    #endregion

    #region Standardizing the path

    public static List<Vector2> Read(List<Vector2> points, int resolution, float scaleSize = 250f)
    {
        points = Resample(points, resolution);
        Vector2 centroid = FindCentroid(points);
        float angle = FindIndicativeAngle(points[0], centroid);
        points = RotateBy(points, angle, centroid);
        centroid = FindCentroid(points);
        points = TranslateToOrigin(points, centroid);
        points = ScaleToSquare(points, scaleSize);
        return points;
    }

    
    static List<Vector2> ScaleToSquare(List<Vector2> points, float size)
    {
        List<Vector2> newPoints = new List<Vector2>();
        size *= .5f; //change size from side length to distance from center
        Vector2 min, max;
        FindBoundingBox(out min, out max, points);
        Vector2 scale = new Vector2(size / max.x, size / max.y);
        for (int i = 0; i < points.Count; i++)
        {
            newPoints.Add(points[i]);
            newPoints[i].Scale(scale);
        }
        return newPoints;
    }

    static List<Vector2> TranslateToOrigin(List<Vector2> points, Vector2 centroid)
    {
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            newPoints.Add(points[i]);
            newPoints[i] -= centroid;
        }
        return newPoints;
    }

    static List<Vector2> RotateBy(List<Vector2> points, float angle, Vector2 centroid)
    {
        float cosAngle = Mathf.Cos(angle);
        float sinAngle = Mathf.Sin(angle);
        List<Vector2> newPoints = new List<Vector2>();
        for (int i = 0; i < points.Count; i++)
        {
            float x = points[i].x * cosAngle - points[i].y * sinAngle +
                centroid.x * (1 - cosAngle) + centroid.y * sinAngle;
            float y = points[i].x * sinAngle + points[i].y * cosAngle +
                            centroid.y * (1 - cosAngle) - centroid.x * sinAngle;
            newPoints.Add(new Vector2(x, y));
        }
        return newPoints;
    }

    static float FindIndicativeAngle(Vector2 firstPoint, Vector2 centroid)
    {
        Vector2 centroidToPoint = firstPoint - centroid;
        if (centroidToPoint.y > 0)
        {
            return Mathf.Atan(centroidToPoint.y / centroidToPoint.x);
        }
        else if (centroidToPoint.y < 0)
        {
            return Mathf.PI + Mathf.Atan(centroidToPoint.y / centroidToPoint.x);
        }
        else
        {
            if (centroidToPoint.x < 0)
            {
                return Mathf.PI;
            }
            else
            {
                return 0f;
            }
        }
    }

    static Vector2 FindCentroid(List<Vector2> points)
    {
        Vector2 min, max;
        FindBoundingBox(out min, out max, points);
        return (.5f * (min + max));
    }

    static void FindBoundingBox(out Vector2 min, out Vector2 max, List<Vector2> points)
    {
        min = points[0];
        max = min;
        for (int i = 1; i < points.Count; i++)
        {
            if (points[i].x > max.x)
            {
                max.x = points[i].x;
            }
            else if (points[i].x < min.x)
            {
                min.x = points[i].x;
            }

            if (points[i].y > max.y)
            {
                max.y = points[i].y;
            }
            else if (points[i].y < min.y)
            {
                min.y = points[i].y;
            }
        }
    }

    static List<Vector2> Resample(List<Vector2> points, int resolution)
    {
        float interval = PathLength(points) / (resolution - 1);
        float distance = 0f;
        List<Vector2> newPoints = new List<Vector2>();
        newPoints.Add(points[0]);
        for (int i = 1; i < points.Count; i++)
        {
            float localDistance = (points[i - 1] - points[i]).magnitude;
            if (distance + localDistance >= interval)
            {
                Vector2 newPoint = Vector2.Lerp(points[i - 1], points[i], (interval - distance) / localDistance);
                distance = 0;
                newPoints.Add(newPoint);
                points.Insert(i + 1, newPoint);
            }
            else
            {
                distance += localDistance;
            }
        }
        return newPoints;
    }

    static float PathLength(List<Vector2> points)
    {
        float toReturn = 0f;
        for (int i = 1; i < points.Count; i++)
        {
            toReturn += (points[i - 1] - points[i]).magnitude;
        }
        return toReturn;
    }

    #endregion
}

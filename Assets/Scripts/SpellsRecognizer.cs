using UnityEngine;
using System.Collections.Generic;

public static class SpellsRecognizer {

    public static void Compare(List<Vector2> points, List<Vector2>[] trainingSets,int resolution) //Need to change return type to float
    {
        Read(points, resolution);
    }


	public static void Read(List<Vector2> points, int resolution) //need to change return type to List<Vector2>
    {
        points = Resample(points, resolution);
        Vector2 centroid = FindCentroid(points);
        float angle = FindIndicativeAngle(points[0], centroid);
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
}

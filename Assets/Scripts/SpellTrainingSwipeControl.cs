using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class SpellTrainingSwipeControl : MonoBehaviour {

    public Button previousSymbolButton;
    public Button nextSymbolButton;
    public Text symbolsDisplay;
    public Text numTrainingSetsDisplay;

    bool swipeStarted = false;
    List<Vector2> inputSwipe = new List<Vector2>();

    int numSymbols = 0;
    int[] numTrainingSets;
    int currentSymbol = 0;

	// Use this for initialization
	void Start ()
    {
        //Find out how many symbols we have folders for
        while(Directory.Exists(Application.dataPath + "/Resources/Symbol" + numSymbols + "TrainingSets"))
        {
            numSymbols++;
        }
        if(numSymbols == 0)
        {
            CreateSymbolFolder();
        }
        numTrainingSets = new int[numSymbols];
        //Find out how many training sets each has
        for (int i = 0; i < numSymbols; i++)
        {
            while (File.Exists(Application.dataPath + "/Resources/Symbol" + i + "TrainingSets/Set" + numTrainingSets[i] + ".csv"))
            {
                numTrainingSets[i]++;
            }
        }
        UpdateDisplay();
	}

    void UpdateDisplay()
    {
        symbolsDisplay.text = "Symbols: " + (1 + currentSymbol) + "/" + numSymbols;
        numTrainingSetsDisplay.text = "Training Sets: " + numTrainingSets[currentSymbol];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    swipeStarted = true;
                    goto case TouchPhase.Moved;
                case TouchPhase.Moved:
                    inputSwipe.Add(touch.position);
                    break;
                case TouchPhase.Ended:
                    List<Vector2> setToSave = SpellsRecognizer.Read(inputSwipe, 64);
                    //Save the training set in a new file
                    CreateNewTrainingSetFile(setToSave);
                    swipeStarted = false;
                    break;
            }
        }
        else if (swipeStarted)
        {
            List<Vector2> setToSave = SpellsRecognizer.Read(inputSwipe, 64);
            //Save the training set in a new file
            CreateNewTrainingSetFile(setToSave);
            swipeStarted = false;
        }
    }

    #region Button Functions

    public void _NextSymbolButton()
    {
        if (currentSymbol == numSymbols - 1)
        {
            nextSymbolButton.interactable = false;
        }
        else
        {
            currentSymbol++;
            if (currentSymbol == numSymbols - 1)
            {
                nextSymbolButton.interactable = false;
            }
            if (currentSymbol > 0)
            {
                previousSymbolButton.interactable = true;
            }
            UpdateDisplay();
        }
    }

    public void _PreviousSymbolButton()
    {
        if (currentSymbol == 0)
        {
            previousSymbolButton.interactable = false;
        }
        else
        {
            currentSymbol++;
            if (currentSymbol == 0)
            {
                previousSymbolButton.interactable = false;
            }
            if (currentSymbol < numSymbols)
            {
                nextSymbolButton.interactable = true;
            }
            UpdateDisplay();
        }
    }

    public void _AddNewSymbol()
    {
        //Create a folder for the training sets to go in
        CreateSymbolFolder();

        //Identify an image to associate with that symbol???

        //Enable the next symbol button
        nextSymbolButton.interactable = true;

        //Update the array tracking the number of training sets
        numSymbols ++;
        int[] temp = new int[numSymbols];
        for (int i = 0; i < numTrainingSets.Length; i++)
        {
            temp[i] = numTrainingSets[i];
        }
        temp[numTrainingSets.Length] = 0;
        numTrainingSets = temp;
        UpdateDisplay();
    }

    #endregion

    void CreateNewTrainingSetFile(List<Vector2> setToSave)
    {
        if (File.Exists(Application.dataPath + "/Resources/Symbol" + numSymbols + "TrainingSets/Temp.txt"))
        {
            File.Delete(Application.dataPath + "/Resources/Symbol" + numSymbols + "TrainingSets/Temp.txt");
        }

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/Symbol" + currentSymbol + "TrainingSets/Set" + numTrainingSets[currentSymbol] + ".csv"))
        {
            foreach(Vector2 point in setToSave)
            {
                writer.WriteLine(point.x + "," + point.y);
            }
        }
        numTrainingSets[currentSymbol]++;
        UpdateDisplay();
    }

    void CreateSymbolFolder()
    {
        Directory.CreateDirectory(Application.dataPath + "/Resources/Symbol" + numSymbols + "TrainingSets");
        numSymbols++;
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Resources/Symbol" + numSymbols + "TrainingSets/Temp.txt"))
        {
            writer.WriteLine("This is a temporary file, placed to ensure that source control does not delete the folder");
        }
    }
}

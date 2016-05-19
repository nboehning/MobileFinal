using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellListScript : MonoBehaviour {

    public enum SpellList { WIND, EARTH, FIRE, WATER, SWORD, PENTACLE, WAND, CUP, MARK, HOLD, CHARM, GROWTH, SPIRIT};

    public List<int[,]> spellList = new List<int[,]>();

    // Use this for initialization
    void Start()
    {
        int[,] wind;
        wind  = new  int[,] { {1,1,1,1,1,1},
                              {0,0,0,0,0,1},
                              {0,1,1,1,0,1},
                              {0,1,0,1,0,1},
                              {0,1,0,1,0,1},
                              {0,1,0,0,0,1},
                              {0,1,1,1,1,1} };
        spellList.Add(wind);
        int[,] earth;
        earth = new  int[,] { {1,1,1,1,1,1},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {1,1,0,0,1,1} };
        spellList.Add(earth);
        int[,] fire;
        fire  =  new int[,] { {0,0,0,1,0,0},
                              {0,0,1,0,0,0},
                              {0,0,0,1,0,0},
                              {0,0,0,0,1,0},
                              {0,1,1,1,1,0},
                              {1,0,0,0,0,0},
                              {0,1,1,1,1,1} };
        spellList.Add(fire);
        int[,] water;
        water =  new int[,] { {0,0,0,0,1,0},
                              {0,0,0,1,0,0},
                              {0,0,1,0,0,1},
                              {0,1,0,0,1,0},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {0,1,1,1,1,0} };
        spellList.Add(water);
        int[,] sword;
        sword =  new int[,] { {0,0,1,0,0,0},
                              {0,0,1,1,0,0},
                              {0,0,1,1,0,0},
                              {0,0,1,1,0,0},
                              {0,0,1,1,0,0},
                              {0,0,1,0,0,0},
                              {0,0,1,0,0,0} };
        spellList.Add(sword);
        int[,] penta;
        penta  = new int[,] { {0,0,1,0,0,0},
                              {0,0,1,1,0,0},
                              {1,1,0,0,1,1},
                              {0,1,0,0,1,0},
                              {0,0,1,1,0,0},
                              {0,1,1,1,1,0},
                              {1,1,0,0,1,1} };
        spellList.Add(penta);
        int[,] wand;
        wand  =  new int[,] { {0,1,0,0,0,0},
                              {0,0,1,0,0,0},
                              {0,0,0,1,0,0},
                              {0,0,0,0,1,0},
                              {0,0,0,1,0,0},
                              {0,0,0,0,1,0},
                              {0,0,0,0,0,1} };
        spellList.Add(wand);
        int[,] cups;
        cups  =  new int[,] { {0,0,0,0,0,0},
                              {0,1,0,0,1,0},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {0,1,0,0,1,0},
                              {0,0,1,1,0,0} };
        spellList.Add(cups);
        int[,] mark;
        mark  =  new int[,] { {1,0,0,0,0,0},
                              {1,1,0,0,0,1},
                              {1,0,1,0,1,0},
                              {1,0,0,1,0,0},
                              {1,0,1,0,1,0},
                              {1,1,0,0,0,1},
                              {1,0,0,0,0,0} };
        spellList.Add(mark);
        int[,] hold;
        hold  =  new int[,] { {0,0,0,1,0,0},
                              {0,0,0,1,0,0},
                              {0,0,0,1,0,0},
                              {0,0,0,1,0,0},
                              {0,0,0,1,0,0},
                              {0,0,1,0,1,0},
                              {0,0,0,1,0,0} };
        spellList.Add(hold);
        int[,] charm;
        charm =  new int[,] { {0,0,0,0,0,0},
                              {0,1,0,0,1,0},
                              {1,0,1,1,0,1},
                              {1,0,0,0,0,1},
                              {0,1,0,0,1,0},
                              {0,0,1,1,0,0},
                              {0,0,0,0,0,0} };
        spellList.Add(charm);
        int[,] growth;
        growth = new int[,] { {0,0,1,1,0,0},
                              {0,1,0,0,1,0},
                              {1,0,0,0,0,1},
                              {0,1,0,0,1,0},
                              {0,0,1,1,0,0},
                              {0,0,1,1,0,0},
                              {1,1,1,1,1,1} };
        spellList.Add(growth);
        int[,] spirit;
        spirit = new int[,] { {0,0,1,1,0,0},
                              {0,1,0,0,1,0},
                              {1,0,0,0,0,1},
                              {1,0,0,0,0,1},
                              {0,1,0,0,1,0},
                              {0,0,1,1,0,0},
                              {0,0,0,1,1,0} };
        spellList.Add(spirit);


    }

    // Update is called once per frame
    void Update () {
	
	}
}

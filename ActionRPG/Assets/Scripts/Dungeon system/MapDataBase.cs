using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapDataBase : MonoBehaviour
{
    private static bool dataBaseLoaded = false;
    static protected Sprite[] floors_dataBase;
    static protected Sprite[] decorates_dataBase;
    static protected Sprite[] backGrounds_dataBase;
    static protected Sprite[] fronts_dataBase;
    static protected Sprite[] roofs_dataBase;
    static protected GameObject[] map_obj_dataBase;

    static public void InitDataBase()
    {
        if (dataBaseLoaded == false)
        {
            floors_dataBase = Resources.LoadAll<Sprite>("Forest/Floors");
            decorates_dataBase = Resources.LoadAll<Sprite>("Forest/Decorate");
            backGrounds_dataBase = Resources.LoadAll<Sprite>("Forest/BackGround");
            fronts_dataBase = Resources.LoadAll<Sprite>("Forest/Front");
            roofs_dataBase = Resources.LoadAll<Sprite>("Forest/Roof");
            map_obj_dataBase = Resources.LoadAll<GameObject>("Forest/MapObjects");

        }
    }

    static public bool isLoaded()
    {
        //Return true if the date is loaded at least once from the start of the program.
        return dataBaseLoaded;
    }
}

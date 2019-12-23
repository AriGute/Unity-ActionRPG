using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButton : MonoBehaviour
{
    public EventMaster eventMaster;
    public GameObject mapHolder;

    public void enterMap()
    {
        if (eventMaster == null)
        {
            print("worldMap is not defined");
        }
        else
        {
            if (mapHolder == null)
            {
                print("mapHolder is not defined");
            }
            else
            {
                Instantiate<GameObject>(mapHolder);
                eventMaster.worldMap(false);
            }
        }
    }
}

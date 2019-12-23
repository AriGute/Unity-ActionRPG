using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    static MonoBehaviour[] effect_list;

    void Start()
    {
        effect_list = Resources.LoadAll<MonoBehaviour>("Effects");
    }
}

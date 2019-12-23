using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjScript : MonoBehaviour
{
    public GameObject player = null;

    void Update()
    {
        if(player != null)
        {
            if(player.transform.position.y > this.transform.position.y)
            {
                this.GetComponent<SpriteRenderer>().sortingLayerName = "8";
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sortingLayerName = "1";

            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            //print("looking for player");
        }
    }
}

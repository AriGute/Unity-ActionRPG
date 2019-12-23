using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MapDataBase
{
    const int bounds = 5;
    private GameObject roof;
    private GameObject floor;
    private GameObject front;
    private GameObject backGround;
    private GameObject[] decorates;
    private GameObject[] mapObj;

    public bool traced = false;
    public bool visited = false;

    public int i;
    public int j;

    public int maxDoors;
    public int[] availableDoors = null;
    public GameObject[] doors = new GameObject[4];

    public void generateRoom(MapType mt, int i, int j)
    {
        this.i = i;
        this.j = j;

        floor = createSpriteObj(floors_dataBase[Random.Range(0, floors_dataBase.Length)], 2);

        front = createSpriteObj(fronts_dataBase[Random.Range(0, fronts_dataBase.Length)], 4);
        front.transform.position = new Vector3(0, -1.2f, 3);
        front.GetComponent<SpriteRenderer>().sortingLayerName = "5";

        roof = createSpriteObj(roofs_dataBase[Random.Range(0, roofs_dataBase.Length)], 4);
        roof.transform.position = new Vector3(0, 1f, 1);

        backGround = createSpriteObj(backGrounds_dataBase[Random.Range(0, backGrounds_dataBase.Length)], 1);
        backGround.transform.localScale = new Vector3(1, 1.2f, 1);

        decorates = new GameObject[5];
        for (int a = 0; a < decorates.Length; a++)
        {
            decorates[a] = createSpriteObj(decorates_dataBase[Random.Range(0, decorates_dataBase.Length)], 3);
            decorates[a].transform.position = new Vector3(0 + Random.Range(-bounds, bounds), 0, 0);
            if (Random.Range(0, 2) == 0)
            {
                decorates[a].transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        mapObj = new GameObject[Random.Range(0,3)];
        for (int a = 0; a < mapObj.Length; a++)
        {
            mapObj[a] = Instantiate(map_obj_dataBase[Random.Range(0, map_obj_dataBase.Length)]);
            mapObj[a].transform.position = new Vector3(0 + Random.Range(-bounds+1.5f, bounds-1.5f), 0 + Random.Range(-2f, -1f), 0);
            if (Random.Range(0, 2) == 0)
            {
                mapObj[a].transform.localScale = new Vector3(-1 * mapObj[a].transform.localScale.x, 1 * mapObj[a].transform.localScale.y, 1);
            }
            mapObj[a].transform.parent = this.transform;
        }


    }


    public bool freeSpace()
    {
        print("\n-------------------");
        int count = 0;
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                count++;
            }
        }

        print(this.name + ":\nmax doors:" + maxDoors + ", count: " + count);
        print("\ntraced: " + traced);
        print("\nvisited: " + visited);

        if (count == maxDoors)
        {
            return false;
        }


        return true;
    }

    public int getRandomDoor()
    {
        if (availableDoors != null)
        {
            int count = 0;
            foreach (int num in availableDoors)
            {
                if (num == 1)
                {
                    count++;
                }

            }
            int[] randomDoor = new int[count];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {

                if (availableDoors[i] == 1)
                {
                    randomDoor[index] = i;
                    index++;
                }

            }
            if (count == 0)
            {
                return -1;
            }
            int randomTemp = Random.Range(0, count);
            int randNum = randomDoor[Random.Range(0, count)];
            return randNum;
        }

        print("[RoomTest.getRandomDoor: aviableDoors = null.]");
        return -1;
    }

    public ref GameObject getDoor(int i)
    {
        return ref doors[i];
    }

    private GameObject createSpriteObj(Sprite sprite, int layer)
    {
        //create gameObject with spriteRenderer componenet for use. 
        GameObject go = new GameObject();
        go.transform.parent = this.transform;
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.GetComponent<SpriteRenderer>().sortingOrder = layer;
        return go;
    }
}

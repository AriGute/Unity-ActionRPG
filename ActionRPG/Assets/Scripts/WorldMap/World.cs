using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private int numOfTerrains;
    private const int high = 6;
    private const int width = 4;
    private const int step = 110;
    private const float fixedX = 230;
    private const float fixedY = 200;

    public EventMaster eventMaster;
    
    private GameObject[] Terrains;
    private GameObject[,] mapPoints;
    private GameObject startVillage;
    private GameObject playerPointer;


    void Start()
    {
        numOfTerrains = high * width / 2;
        mapPoints = new GameObject[high, width];
        Terrains = Resources.LoadAll<GameObject>("WorldMap/Terrains");
        startVillage = Resources.Load<GameObject>("WorldMap/MapObj/village");
        playerPointer = Instantiate(Resources.Load<GameObject>("WorldMap/playerPointer"));
        playerPointer.GetComponent<playerPointer>().eventMaster = this.eventMaster;
        playerPointer.transform.parent = this.transform;
        generateTerrains();
        playerPointer.transform.SetAsLastSibling();
    }

    private void generateTerrains()
    {
        GameObject terrain = null;
        Point point;

        for (int n = 0; n < numOfTerrains; n++)
        {
            point = findFreePoint();
            terrain = Terrains[Random.Range(0, Terrains.Length)];
            terrain = Instantiate(terrain);
            terrain.transform.parent = this.transform;
            terrain.transform.position = new Vector3(fixedX + step * point.Ypos + Random.Range(-(step / 7), (step / 7)), fixedY + Random.Range(-(step / 7), (step / 7)) + step * point.Xpos, 0);
            terrain.transform.localScale = new Vector3(0.07f, 0.07f, 1);
            mapPoints[point.Ypos, point.Xpos] = terrain;
        }

        point = findFreePoint();
        terrain = Instantiate(startVillage);
        terrain.transform.parent = this.transform;
        terrain.transform.position = new Vector3(fixedX + step * point.Ypos + Random.Range(-(step / 2), (step / 2)), fixedY + Random.Range(-(step / 2), (step / 2)) + step * point.Xpos, 0);
        terrain.transform.localScale = new Vector3(0.07f, 0.07f, 1);
        playerPointer.transform.position = terrain.transform.position;

    }

    private Point findFreePoint()
    {
        int i = Random.Range(0, high), j = Random.Range(0, width);
        while (mapPoints[i, j] != null)
        {
            j++;
            if (j >= width)
            {
                j = 0;
                i++;
            }
            if (i >= high)
            {
                i = 0;
            }
        }
        return new Point(i,j);
    }

}

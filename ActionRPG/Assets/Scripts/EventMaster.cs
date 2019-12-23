using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaster : MonoBehaviour
{
    private GameObject player;
    private GameObject buttons;
    private GameObject world;

    void Start()
    {
        MapDataBase.InitDataBase();
        Instantiate(Resources.Load<GameObject>("blackFrame"));
        Instantiate(Resources.Load<GameObject>("bounds"));

        player = generatPlayer();
        world = generateWorld();

        player.SetActive(false);

        
    }

    private GameObject generatPlayer()
    {

        GameObject tempPlayer = Instantiate(Resources.Load<GameObject>("Character"));
        tempPlayer.name = "player";
        tempPlayer.transform.position = new Vector3(0, -2, -5);

        return tempPlayer;
    }

    private GameObject generateWorld()
    {
        GameObject canvas = Instantiate(Resources.Load<GameObject>("WorldMap/Canvas"));
        Bag.setInventory(canvas.transform.Find("Bag/bagUi").gameObject);
        Loot.setPlayer(this.player);

        GameObject tempWorld = canvas.transform.Find("world").gameObject;
        tempWorld.AddComponent<World>();
        tempWorld.GetComponent<World>().eventMaster = this;
        return tempWorld;
    }

    public void worldMap(bool open)
    {
        world.SetActive(open);
        player.SetActive(!open);
        player.GetComponent<Player>().playerCamera.SetActive(!open);
        player.transform.position = new Vector3(0,0,0);
    }

    public void startEvent()
    {
        worldMap(false);
        GameObject map = Instantiate(new GameObject());
        map.name = "map";
        map.AddComponent<Map>();
        map.GetComponent<Map>().generateMap(null,2);
    }

}

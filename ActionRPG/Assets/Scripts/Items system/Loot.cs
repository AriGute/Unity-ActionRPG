using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    private static GameObject player;
    private static Transform firstSlot;

    private Bag bag;
    private List<GameObject> showItems = new List<GameObject>();

    void Start()
    {
        bag = new Bag();
        bag.initBag(false);
        GenerateLoot();
    }

    public static void setPlayer(GameObject _player)
    {
        player = _player;
    }

    private void GenerateLoot()
    {
        Weapon item1 = new Weapon();
        item1.initItem();
        bag.addItem(item1);

        Weapon item2 = new Weapon();
        item2.initItem();
        bag.addItem(item2);

        Weapon item3 = new Weapon();
        item3.initItem();
        bag.addItem(item3);

    }

    public void showLoot()
    {
        float dis = Vector2.Distance(this.transform.position, player.transform.position);

        if (dis < 2)
        {
            bag.openBag("Loot");
        }
    }



}




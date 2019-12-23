using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipments : MonoBehaviour
{
    private GameObject equipUi;
    private Item rightHandMount;
    private Item leftHandMount;
    private Item helmt;
    private Item chest;
    private Item legs;

    public GameObject rightHandMountObj;
    public GameObject leftHandMountObj;
    public GameObject helmtObj;
    public GameObject chestObj;
    public GameObject legsObj;

    private Item quickDrawSlot1;
    private Item quickDrawSlot2;
    private Item quickDrawSlot3;
    private Item quickDrawSlot4;

    private void Start()
    {
        equipUi = Resources.Load<GameObject>("Items/bagUi Variant");
    }

    public void equipItem(Item item)
    {
        switch (item.getEquipType)
        {
            case EquipType.oneHand:
                rightHandMount = item;
                rightHandMountObj.GetComponent<SpriteRenderer>().sprite = item.getSprite;
                break;
            case EquipType.twoHands:
                break;
            case EquipType.otherHand:
                break;
            case EquipType.helmet:
                break;
            case EquipType.chest:
                break;
            case EquipType.quickDraw:
                break;
            case EquipType.legs:
                break;
        }
    }
}

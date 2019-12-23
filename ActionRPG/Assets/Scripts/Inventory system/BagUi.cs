using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BagUi : MonoBehaviour, IDragHandler, BagFunctions
{
    private Bag bag;

    public void addToBag(Item item)
    {
        bag.addItem(item);
    }


    public bool checkBags(Bag otherBag)
    {
        //Return true if both itams from the same bag.
        if (bag.BagId == otherBag.BagId)
        {
            return true;
        }
        return false;
    }


    public void initBagUi(Bag bag)
    {
        this.bag = bag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

}

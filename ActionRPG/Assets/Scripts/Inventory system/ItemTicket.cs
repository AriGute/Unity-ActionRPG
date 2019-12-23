using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTicket : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler, BagFunctions
{
    private Bag owenr;
    private int bagPos;
    private Vector2 startPos;

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = this.transform.position;
        transform.parent.SetAsLastSibling();
        print(this.owenr.getItem(bagPos));
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {
        PointerEventData pointerEvent = new PointerEventData(EventSystem.current);
        List<RaycastResult> resultList = new List<RaycastResult>();
        pointerEvent.position = Input.mousePosition;

        Bag oldBag = owenr;

        EventSystem.current.RaycastAll(pointerEvent, resultList);

        for (int i = 0; i < resultList.Count; i++)
        {
            //check on what(ui) the icon fall.
            if ((resultList[i].gameObject.tag != "Icon" && resultList[i].gameObject.tag != "Bag") || resultList[i].gameObject == this.gameObject)
            {
                resultList.RemoveAt(i);
                i--;
            }
        }

        if (resultList.Count == 2)
        {
            //if resultList.count==2 then the icon fall on another icon and bag ui.
            if (resultList[0].gameObject.GetComponent<ItemTicket>().checkBags(owenr))
            {
                //if the items from the same bag then switch items slots in the bag.
                print("(icons)the items from the same bag.");
                ItemTicket otherItem = resultList[0].gameObject.GetComponent<ItemTicket>();

                owenr.switchPlaces(bagPos, otherItem.itemBagPos);

            }
            else
            {
                //if the items not from the same bag then move this item to the other bag.
                print("(icons)the items from diffrent bags.");

                ItemTicket otherItem = resultList[0].gameObject.GetComponent<ItemTicket>();
                otherItem.tradeItem(owenr, this);

            }

        }
        else if (resultList.Count == 1)
        {
            //if resultList.count==1 then the icon fall on bag ui.

            if (resultList[0].gameObject.GetComponent<BagUi>().checkBags(owenr))
            {
                print("(icons)from the this bag.");

                //if its someone else bag.
                resultList[0].gameObject.GetComponent<BagUi>().addToBag(this.owenr.getItem(bagPos));
                this.owenr.removeItem(bagPos);
 
            }
            else
            {
                print("(icons)the items NOT! from the this bag.");
                BagUi otherBag = resultList[0].gameObject.GetComponent<BagUi>();

                otherBag.addToBag(this.owenr.getItem(bagPos));
                this.owenr.removeItem(bagPos);

            }
        }
        else
        {
            //if the icon fall on nothing(resultList.count==0).
            this.transform.position = startPos;
        }
        Bag.refreshIcons();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    public void initTicket(Bag bag, int pos)
    {
        this.owenr = bag;
        this.bagPos = pos;
    }

    public void backToPlace()
    {
        transform.position = startPos;
    }

    public void tradeItem(Bag otherBag, ItemTicket otherItemTicket)
    {
        print("Trade items with other bag.");
        if (otherBag.addItem(owenr.getItem(bagPos)))
        {
            owenr.removeItem(bagPos);

            owenr.addItem(otherBag.getItem(otherItemTicket.bagPos));
            otherBag.removeItem(otherItemTicket.bagPos);
        }
    }

    public bool checkBags(Bag otherBag)
    {
        //Return true if both itams from the same bag.
        if (owenr.BagId == otherBag.BagId)
        {
            return true;
        }
        return false;
    }

    public int itemBagPos
    {
        get
        {
            return bagPos;
        }
        set
        {
            this.bagPos = value;
        }
    }
    public Vector2 getIconPos
    {
        get
        {
            return startPos;
        }
    }

    public override string ToString()
    {
        return "Bag onwer: " + (owenr == null ? "Null" : owenr.gameObject.name) + ", bag index: " + bagPos;
    }

    public void addToBag(Item item)
    {
        owenr.addItem(item);
    }
}

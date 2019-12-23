using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHolder : MonoBehaviour, IDragHandler, IDropHandler, IPointerDownHandler
{
    private Bag owenr;
    private int bagPos;
    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void initTicket(Bag bag, int pos)
    {
        this.owenr = bag;
        this.bagPos = pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = this.transform.position;
        transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position += (Vector3)eventData.delta;
    }

    public void OnDrop(PointerEventData eventData)
    {

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
}

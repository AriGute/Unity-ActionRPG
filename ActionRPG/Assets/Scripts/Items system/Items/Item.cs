using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected string itemName = "none";
    protected string descrip = "item descrip";
    protected int _weight = 0;
    protected int _value = 0;
    
    protected Sprite _obj;
    protected GameObject _icon;
    protected EquipType equipType;
    public abstract void initItem(int mapLevel = 1);

    public Sprite getSprite
    {
        get
        {
            return _obj;
        }
    }
    public GameObject getIcon
    {
        get
        {
            return _icon;
        }
    }

    public EquipType getEquipType
    {
        get
        {
            return equipType;
        }
    }

    public int weight
    {
        get
        {
            return this._weight;
        }
    }
    public int value
    {
        get
        {
            return this._value;
        }
    }

    public override string ToString()
    {
        return "name: " + this.itemName + ", descrip: " + descrip + ", weight: " + _weight + ", value: " + _value;
    }
}

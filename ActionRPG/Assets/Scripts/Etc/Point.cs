using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private int x,y;
    public Point(int i = 0, int j = 0)
    {
        this.x = j;
        this.y = i;
    }


    public void initPoint(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int Xpos
    {
        set
        {
            this.x = value;
        }
        get
        {
            return this.x;
        }
    }

    public int Ypos
    {
        set
        {
            this.y = value;
        }
        get
        {
            return this.y;
        }
    }
}

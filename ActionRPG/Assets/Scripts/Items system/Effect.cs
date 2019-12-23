using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected string name;
    protected float timer;
    protected int stacks;

    public abstract void activeEffect();
}

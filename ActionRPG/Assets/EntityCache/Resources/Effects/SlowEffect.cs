using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : Effect
{
    void Start()
    {
        this.name = "slow";
        this.timer = 5;
        this.stacks = 0;
    }

    public override void activeEffect()
    {

    }


}

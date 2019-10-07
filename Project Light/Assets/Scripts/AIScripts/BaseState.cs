using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    protected int playerLayer = 1 << 9;
    protected int environmentLayer = 1 << 10;
    protected GameObject gameObject;
    protected Transform transform;
    protected bool reset, init;

    public abstract Type Tick();
}

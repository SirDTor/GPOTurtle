using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ObjectOnMap
{
    public enum Effect
    {
        NOTHING,
        CHANGEFUEL,
        CHANGEHEALTH,
        CHANGEAMMO,
        DESTROY,
        FINISH
    }
    public Effect effect;
    public int value;
    public bool destroyable;

    void Start()
    {
        base.Start();
        collider.radius *= 0.5f;
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name);
        other.SendMessage(effect.ToString(), value);
        if (destroyable) GameObject.Destroy(this.gameObject);
    }

}

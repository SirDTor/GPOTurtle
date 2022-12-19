using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatValue : ValueContent
{
    private float val;
    public float Value
    {
        get => val;
        set => val = value;
    }

    public FloatValue(float value) : base(ValueContentType.FloatValueType)
    {
        val = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolValue : ValueContent
{
    private bool val;
    public bool Value
    {
        get => val;
        set => val = value;
    }

    public BoolValue(bool value) : base(ValueContentType.BoolValueType)
    {
        val = value;
    }
}

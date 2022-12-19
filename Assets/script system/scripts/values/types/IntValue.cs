using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntValue : ValueContent
{
    private int val;
    public int Value
    {
        get => val;
        set => val = value;
    }

    public IntValue(int value) : base(ValueContentType.IntValueType)
    {
        val = value;
    }
}

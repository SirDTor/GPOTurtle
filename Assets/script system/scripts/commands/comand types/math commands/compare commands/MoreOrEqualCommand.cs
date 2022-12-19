using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreOrEqualCommand : BaseCompareCommand
{
    public MoreOrEqualCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.MoreOrEqualCommandType, out resultValue, firstArgument, secondArgument)
    { }

    protected override bool compareFunction(float firstValue, float secondValue)
    {
        return firstValue >= secondValue;
    }
}

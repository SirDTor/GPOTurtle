using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCommand : BaseCompareCommand
{
    public MoreCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.MoreCommandType, out resultValue, firstArgument, secondArgument)
    { }

    protected override bool compareFunction(float firstValue, float secondValue)
    {
        return firstValue > secondValue;
    }
}

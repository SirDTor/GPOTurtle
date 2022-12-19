using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EqualCommand : BaseCompareCommand
{
    public EqualCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.EqualCommandType, out resultValue, firstArgument, secondArgument)
    { }

    protected override bool compareFunction(float firstValue, float secondValue)
    {
        return firstValue == secondValue;
    }
}

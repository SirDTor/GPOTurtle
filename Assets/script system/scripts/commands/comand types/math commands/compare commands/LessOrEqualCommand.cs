using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessOrEqualCommand : BaseCompareCommand
{
    public LessOrEqualCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.LessOrEqualCommandType, out resultValue, firstArgument, secondArgument)
    { }

    protected override bool compareFunction(float firstValue, float secondValue)
    {
        return firstValue <= secondValue;
    }
}

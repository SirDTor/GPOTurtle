using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessCommand : BaseCompareCommand
{
    public LessCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.LessCommandType, out resultValue, firstArgument, secondArgument)
    { }

    protected override bool compareFunction(float firstValue, float secondValue)
    {
        return firstValue < secondValue;
    }
}

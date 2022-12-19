using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public abstract class BaseCompareCommand : TwoArgumentsMathCommand
{
    public BaseCompareCommand(MathCommandType mathCommandType,out Value resultValue, Value firstArgument, Value secondArgument) :
        base(mathCommandType, firstArgument, secondArgument)
    { 
        ResultValue = new Value(new BoolValue(false));
        resultValue = ResultValue;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---" + MathType.ToString() + "---");
        float firstValue;
        float secondValue;
        try
        {
            bool isCalculated = false;
            firstValue = getFirstArgumentFloat(out isCalculated);
            if (!isCalculated) return false;
            secondValue = getSecondArgumentFloat(out isCalculated);
            if (!isCalculated) return false;
        }
        catch (System.Exception)
        {
            throw new System.Exception(MathType.ToString() + " unsuported type of arguments");
        }
        if (ResultValue.Content.Type != BoolValueType) throw new System.Exception(MathType.ToString() + " unsuported type of result");

        ((BoolValue)ResultValue.Content).Value = compareFunction(firstValue, secondValue);
        return true;
    }

    protected abstract bool compareFunction(float firstValue, float secondValue);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class AndCommand : TwoArgumentsMathCommand
{
    static Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        typeMatrix = new Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        {
            {BoolValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {BoolValueType, BoolValueType} }
            },

            {IntValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {IntValueType, BoolValueType}, {FloatValueType, BoolValueType} }
            },

            {FloatValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {IntValueType, BoolValueType}, {FloatValueType, BoolValueType} }
            }
        };

    public AndCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
            base(MathCommandType.DivideCommandType, firstArgument, secondArgument)
    {
        ResultValue = Value.GetNewValueFromType(typeMatrix[firstArgument.Content.Type][secondArgument.Content.Type]);
        resultValue = ResultValue;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---AndCommand---");
        if (ResultValue.Content.Type == BoolValueType)
        {
            bool isCalculated = false;
            bool firstArgument = getFirstArgumentBool(out isCalculated);
            if (!isCalculated) return false;
            bool secondArgument = getSecondArgumentBool(out isCalculated);
            if (!isCalculated) return false;

            ((BoolValue)ResultValue.Content).Value = firstArgument && secondArgument;
            return true;
        }
        throw new System.Exception("and command unsuported type of arguments");
    }
}
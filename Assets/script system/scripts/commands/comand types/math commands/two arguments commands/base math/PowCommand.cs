using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class PowCommand : TwoArgumentsMathCommand
{
    static Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        typeMatrix = new Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        {
            {FloatValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {FloatValueType, FloatValueType}, { IntValueType, FloatValueType} }
            },

            {IntValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {FloatValueType, FloatValueType}, {IntValueType, IntValueType} }
            }
        };

    public PowCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
        base(MathCommandType.MultiplyCommandType, firstArgument, secondArgument)
    {
        ResultValue = Value.GetNewValueFromType(typeMatrix[firstArgument.Content.Type][secondArgument.Content.Type]);
        resultValue = ResultValue;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---PowCommand---");
        if (ResultValue.Content.Type == IntValueType)
        {
            bool isCalculated = false;
            int firstArgument = ((IntValue)FirstArgument.getResultContent(out isCalculated)).Value;
            if (!isCalculated) return false;
            int secondArgument = ((IntValue)SecondArgument.getResultContent(out isCalculated)).Value;
            if (!isCalculated) return false;

            ((IntValue)ResultValue.Content).Value = Mathf.RoundToInt(Mathf.Pow(firstArgument, secondArgument));
            return true;
        }
        if (ResultValue.Content.Type == FloatValueType)
        {
            bool isCalculated = false;
            float firstArgument = getFirstArgumentFloat(out isCalculated);
            if (!isCalculated) return false;
            float secondArgument = getSecondArgumentFloat(out isCalculated);
            if (!isCalculated) return false;

            ((FloatValue)ResultValue.Content).Value = Mathf.Pow(firstArgument, secondArgument);
            return true;
        }
        throw new System.Exception("pow command unsuported type of arguments");
    }
}

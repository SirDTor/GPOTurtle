using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class ModCommand : TwoArgumentsMathCommand
{
    static Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        typeMatrix = new Dictionary<ValueContent.ValueContentType,
        Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>>
        {
            {IntValueType, new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
            { {IntValueType, IntValueType} }
            }
        };

    public ModCommand(out Value resultValue, Value firstArgument, Value secondArgument) :
            base(MathCommandType.ModCommandType, firstArgument, secondArgument)
    {
        ResultValue = Value.GetNewValueFromType(typeMatrix[firstArgument.Content.Type][secondArgument.Content.Type]);
        resultValue = ResultValue;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---ModeCommand---");
        if (ResultValue.Content.Type == IntValueType)
        {
            bool isCalculated = false;
            int firstArgument = ((IntValue)FirstArgument.getResultContent(out isCalculated)).Value;
            if (!isCalculated) return false;
            int secondArgument = ((IntValue)SecondArgument.getResultContent(out isCalculated)).Value;
            if (!isCalculated) return false;

            ((IntValue)ResultValue.Content).Value = firstArgument % secondArgument;
            return true;
        }
        throw new System.Exception("add command unsuported type of arguments");
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class NotCommand : OneArgumentMathCommand
{
    static Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
        typeMatrix = new Dictionary<ValueContent.ValueContentType, ValueContent.ValueContentType>
        {
            {BoolValueType, BoolValueType}
        };

    public NotCommand(out Value resultValue, Value firstArgument) :
        base(MathCommandType.EqualizeCommandType, firstArgument)
    {
        ResultValue = Value.GetNewValueFromType(typeMatrix[firstArgument.Content.Type]);
        resultValue = ResultValue;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---NotCommand---");
        bool isCalculated = false;
        switch (ResultValue.Content.Type)
        {
            case BoolValueType:
                bool boolArgument = ((BoolValue)FirstArgument.getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                ((BoolValue)ResultValue.Content).Value = !boolArgument;
                return true;

            default:
                throw new System.Exception("not command unsuported type of arguments");
        }
    }
}

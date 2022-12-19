using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class EqualizeCommand : OneArgumentMathCommand
{
    public EqualizeCommand(Value resultValue, Value firstArgument) :
        base(MathCommandType.EqualizeCommandType, resultValue, firstArgument)
    { isEqualTypesOfArgumentAndResult(); }

    public override bool InvokeCommand()
    {
        Debug.Log("---EqualizeCommand---");
        bool isCalculated = false;
        switch (ResultValue.Content.Type)
        {
            case IntValueType:
                int intArgument = ((IntValue)FirstArgument.getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                ((IntValue)ResultValue.Content).Value = intArgument;
                return true;

            case FloatValueType:
                float floatArgument = ((FloatValue)FirstArgument.getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                ((FloatValue)ResultValue.Content).Value = floatArgument;
                return true;

            case BoolValueType:
                bool boolArgument = ((BoolValue)FirstArgument.getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                ((BoolValue)ResultValue.Content).Value = boolArgument;
                return true;

            default: 
                throw new System.Exception("equalize command unsuported type of arguments");
        }   
    }
}

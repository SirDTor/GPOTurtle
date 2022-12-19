using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintCommand : BaseObjectCommand
{
    public PrintCommand(List<Value> arguments = null)
        : base("Print", arguments) { }

    public PrintCommand(Value argument)
        : base("Print", argument) { }

    public override bool InvokeCommand()
    {
        bool isCalculated = false;
        switch (arguments[0].Content.Type)
        {
            case ValueContent.ValueContentType.IntValueType:
                int intRes = ((IntValue)arguments[0].getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                else
                {
                    Debug.Log($"Print output (IntType): {intRes}");
                    return true;
                }

            case ValueContent.ValueContentType.FloatValueType:
                float floatRes = ((FloatValue)arguments[0].getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                else
                {
                    Debug.Log($"Print output (FloatType): {floatRes}");
                    return true;
                }

            case ValueContent.ValueContentType.BoolValueType:
                bool boolRes = ((BoolValue)arguments[0].getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
                else
                {
                    Debug.Log($"Print output (BoolType): {boolRes}");
                    return true;
                }

            default:
                Debug.Log($"Print output (NoneType): None");
                break;

        }
        return true;
    }
}

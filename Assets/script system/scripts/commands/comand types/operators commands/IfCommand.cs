using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class IfCommand : BaseCommandType
{
    private CodePart trueCodePart;
    public CodePart TrueCodePart { set => trueCodePart = value; }

    private CodePart falseCodePart;
    public CodePart FalseCodePart { set => falseCodePart = value; }

    private Value value;
    public Value Value { set => this.value = value; }

    private bool boolValue;
    private bool isFirst = true;

    public IfCommand(CodePart trueCodePart = null, Value value = null)
    {
        this.type = CommandType.IfCommandType;
        this.trueCodePart = trueCodePart;
        falseCodePart = null;
        this.value = value;
    }

    public IfCommand(CodePart trueCodePart, CodePart falseCodePart, Value value)
    {
        this.type = CommandType.IfCommandType;
        this.trueCodePart = trueCodePart;
        this.falseCodePart = falseCodePart;
        this.value = value;
    }

    public override bool InvokeCommand()
    {
        if (isFirst)
        {
            Debug.Log("---IfCommand---");
            if (value.Content.Type == BoolValueType)
            {
                bool isCalculated = false;
                boolValue = ((BoolValue)value.getResultContent(out isCalculated)).Value;
                if (!isCalculated) return false;
            }
            else throw new System.Exception("if command argument must be <Bool> type");

            if (!boolValue && falseCodePart == null)
            {
                Debug.Log("-noElseCode-");
                return true;
            }
            isFirst = false;
            return false;
        }
        else
        {
            if (boolValue)
            {
                Debug.Log("-true-");
                bool res = trueCodePart.execute();
                if (res) isFirst = true;
                return res;
            }
            else
            {
                Debug.Log("-false-");
                bool res = falseCodePart.execute();
                if (res) isFirst = true;
                return res;
            }
        }
    }
}

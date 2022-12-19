using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public class WhileCommand : BaseCommandType
{
    private CodePart code;
    public CodePart Code
    {
        set => this.code = value;
        get => code;
    }

    private Value value;
    public Value Value
    {
        set => this.value = value;
        get => value;
    }

    private bool isFirst = true;
    private bool curentValue;
    private bool isCicle = false;

    public WhileCommand(CodePart code, Value value)
    {
        this.type = CommandType.WhileCommandType;
        this.code = code;
        this.value = value;
    }

    public WhileCommand()
    {
        this.type = CommandType.WhileCommandType;
        this.code = null;
        this.value = null;
    }

    public override bool InvokeCommand()
    {
        Debug.Log("---While---");
        if (isFirst)
        {
            isFirst = false;
            return false;
        }
        if (!isCicle && value.Content.Type == BoolValueType)
        {
            bool isCalculated = false;
            curentValue = ((BoolValue)value.getResultContent(out isCalculated)).Value;
            if (!isCalculated) return false;

            if (curentValue)
            {
                isCicle = true;
                Debug.Log("---restart while---");
            }
            else
            {
                Debug.Log("---end while---");
                isFirst = true;
                isCicle = false;
                return true;
            }
        }
        if (isCicle)
        {
            Debug.Log("---cicle while---");
            isCicle = !code.execute();
            return false;
        }
        else throw new System.Exception("WhileCommand not bool value error");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public abstract class OneArgumentMathCommand : BaseMathCommand
{
    private Value resultValue;
    public Value ResultValue
    {
        set => resultValue = value;
        get => resultValue;
    }

    private Value firstArgument;
    public Value FirstArgument
    {
        set => firstArgument = value;
        get => firstArgument;
    }

    public OneArgumentMathCommand(MathCommandType mathCommandType, Value resultValue, Value firstArgument) : base(mathCommandType)
    {
        this.resultValue = resultValue;
        this.firstArgument = firstArgument;
    }

    public OneArgumentMathCommand(MathCommandType mathCommandType, Value firstArgument) : base(mathCommandType)
    {
        this.resultValue = null;
        this.firstArgument = firstArgument;
    }

    protected bool isEqualTypesOfArgumentAndResult()
    {
        if (ResultValue.Content.Type == FirstArgument.Content.Type) return true;
        else throw new System.Exception("math command not equal type of argument and result");
    }

    public static BaseCommandType getNewPrefixOneArgumentCommand(out Value result, string operatorStr, Stack<Value> valuesStack)
    {
        Value firstArg = valuesStack.Pop();

        switch (operatorStr)
        {
            case "!":
                return new NotCommand(out result, firstArg);

            default: throw new System.Exception($"undefined math operator <{operatorStr}>");
        }
    }
}

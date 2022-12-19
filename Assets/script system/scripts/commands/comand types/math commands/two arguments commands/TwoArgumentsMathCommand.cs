using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ValueContent.ValueContentType;

public abstract class TwoArgumentsMathCommand : BaseMathCommand
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

    private Value secondArgument;
    public Value SecondArgument
    {
        set => secondArgument = value;
        get => secondArgument;
    }

    public TwoArgumentsMathCommand(MathCommandType mathCommandType, Value firstArgument, Value secondArgument) : base(mathCommandType)
    {
        this.resultValue = null;
        this.firstArgument = firstArgument;
        this.secondArgument = secondArgument;
    }

    public static BaseCommandType getNewTwoArgumentsCommand(out Value result, string operatorStr, Stack<Value> valuesStack)
    {
        Value secondArg = valuesStack.Pop();
        Value firstArg = valuesStack.Pop();

        switch (operatorStr)
        {
            case "+":
                return new AddCommand(out result, firstArg, secondArg);
            case "-":
                return new SubtractCommand(out result, firstArg, secondArg);
            case "*":
                return new MultiplyCommand(out result, firstArg, secondArg);
            case "/":
                return new DivideCommand(out result, firstArg, secondArg);
            case "^":
                return new PowCommand(out result, firstArg, secondArg);
            case "%":
                return new ModCommand(out result, firstArg, secondArg);
            case "==":
                return new EqualCommand(out result, firstArg, secondArg);
            case "<=":
                return new LessOrEqualCommand(out result, firstArg, secondArg);
            case ">=":
                return new MoreOrEqualCommand(out result, firstArg, secondArg);
            case "<":
                return new LessCommand(out result, firstArg, secondArg);
            case ">":
                return new MoreCommand(out result, firstArg, secondArg);
            case "&&":
                return new AndCommand(out result, firstArg, secondArg);
            case "||":
                return new OrCommand(out result, firstArg, secondArg);

            default: throw new System.Exception($"undefined math operator <{operatorStr}>");
        }
    }

    protected bool isEqualArguments()
    {
        if ((ResultValue.Content.Type == FirstArgument.Content.Type) &&
            (ResultValue.Content.Type == SecondArgument.Content.Type) &&
            (FirstArgument.Content.Type == SecondArgument.Content.Type)) return true;
        else throw new System.Exception("math command not equal type of arguments and result");
    }

    protected bool isEqualTypesOfArgumentsAndResult()
    {
        if ((ResultValue.Content.Type == FirstArgument.Content.Type) &&
            (ResultValue.Content.Type == SecondArgument.Content.Type) &&
            (FirstArgument.Content.Type == SecondArgument.Content.Type)) return true;
        else throw new System.Exception("math command not equal type of arguments and result");
    }

    protected bool isEqualTypesOfArguments()
    {
        if (FirstArgument.Content.Type == SecondArgument.Content.Type) return true;
        else throw new System.Exception("math command not equal type of arguments");
    }

    protected float getFirstArgumentFloat(out bool isCalculated)
    {
        float value = 0f;
        switch (firstArgument.Content.Type)
        {
            case FloatValueType:
                isCalculated = false;
                value = ((FloatValue)firstArgument.getResultContent(out isCalculated)).Value;
                return value;

            case IntValueType:
                isCalculated = false;
                value = ((IntValue)firstArgument.getResultContent(out isCalculated)).Value;
                return value;

            case BoolValueType:
                isCalculated = false;
                value = ((BoolValue)firstArgument.getResultContent(out isCalculated)).Value ? 1f : 0f;
                return value;

            default:
                throw new System.Exception("to float conversion error");
        }
    }

    protected float getSecondArgumentFloat(out bool isCalculated)
    {
        float value = 0f;
        switch (secondArgument.Content.Type)
        {
            case FloatValueType:
                isCalculated = false;
                value = ((FloatValue)secondArgument.getResultContent(out isCalculated)).Value;
                return value;

            case IntValueType:
                isCalculated = false;
                value = ((IntValue)secondArgument.getResultContent(out isCalculated)).Value;
                return value;

            case BoolValueType:
                isCalculated = false;
                value = ((BoolValue)secondArgument.getResultContent(out isCalculated)).Value ? 1f : 0f;
                return value;

            default:
                throw new System.Exception("to float conversion error");
        }
    }

    protected bool getFirstArgumentBool(out bool isCalculated)
    {
        bool value;
        switch (firstArgument.Content.Type)
        {
            case FloatValueType:
                isCalculated = false;
                value = ((FloatValue)firstArgument.getResultContent(out isCalculated)).Value > 0;
                return value;

            case IntValueType:
                isCalculated = false;
                value = ((IntValue)firstArgument.getResultContent(out isCalculated)).Value > 0;
                return value;

            case BoolValueType:
                isCalculated = false;
                value = ((BoolValue)firstArgument.getResultContent(out isCalculated)).Value;
                return value;

            default:
                throw new System.Exception("to bool conversion error");
        }
    }

    protected bool getSecondArgumentBool(out bool isCalculated)
    {
        bool value;
        switch (firstArgument.Content.Type)
        {
            case FloatValueType:
                isCalculated = false;
                value = ((FloatValue)secondArgument.getResultContent(out isCalculated)).Value > 0;
                return value;

            case IntValueType:
                isCalculated = false;
                value = ((IntValue)secondArgument.getResultContent(out isCalculated)).Value > 0;
                return value;

            case BoolValueType:
                isCalculated = false;
                value = ((BoolValue)secondArgument.getResultContent(out isCalculated)).Value;
                return value;

            default:
                throw new System.Exception("to bool conversion error");
        }
    }
}

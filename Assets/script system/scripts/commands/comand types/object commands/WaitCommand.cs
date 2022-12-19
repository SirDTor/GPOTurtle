using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ValueContent.ValueContentType;
//using static BaseObjectCommand.ObjectCommandType;

public class WaitCommand : BaseObjectCommand
{
    private int calls = 0;
    IntValue waitTime;
    public WaitCommand(List<Value> arguments = null)
        : base("Wait", arguments) { }

    public WaitCommand(Value argument)
        : base("Wait", argument) { }

    public override bool InvokeCommand()
    {
        if(calls == 0)
        {
            if(arguments[0].Content.Type == IntValueType)
            {
                bool isCalculated = false;
                waitTime = (IntValue)arguments[0].getResultContent(out isCalculated);
                if (!isCalculated) return false;
            }
        }
        BoolValue isFinished = new BoolValue(false);
        gameObjectMethod.Invoke(arguments, returnValue, isFinished);
        calls++;
        if (calls >= waitTime.Value)
        {
            calls = 0;
            return true;
        }
        else return false;
    }
}

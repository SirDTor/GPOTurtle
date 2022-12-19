using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Events;

public class BaseObjectCommand : BaseCommandType
{
    /*
    public enum ObjectCommandType {
        NULL,
        ColorCommandType, 
        MoveCommandType, 
        WaitCommandType,
        TurnCommandType};*/

    protected UnityEvent<List<Value>, Value, BoolValue> gameObjectMethod;
    protected List<Value> arguments;
    protected Value returnValue;
    public List<Value> Arguments 
    {
        get => arguments;
        set => arguments = value;
    }

    public Value ReturnValue
    {
        get => returnValue;
        set => returnValue = value;
    }

    protected string objectCommandName;
    public string Name => objectCommandName;

    public BaseObjectCommand(string objectCommandName, List<Value> arguments = null, Value returnValue = null)
    {
        type = CommandType.ObjectCommandType;
        this.objectCommandName = objectCommandName;
        this.arguments = arguments;
        this.returnValue = returnValue;
        gameObjectMethod = ScriptSystemSingleton.commandsList.GetCommandMethod(objectCommandName);
    }

    public BaseObjectCommand(string objectCommandName, Value argument, Value returnValue = null)
    {
        type = CommandType.ObjectCommandType;
        this.objectCommandName = objectCommandName;
        this.returnValue = returnValue;
        arguments = new List<Value>();
        arguments.Add(argument);
        gameObjectMethod = ScriptSystemSingleton.commandsList.GetCommandMethod(objectCommandName);
    }

    public override bool InvokeCommand()
    {
        BoolValue isFinished = new BoolValue(false);
        gameObjectMethod.Invoke(arguments, returnValue, isFinished);
        return isFinished.Value;
    }
}

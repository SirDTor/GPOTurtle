using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;


public class CommandsList : MonoBehaviour
{
    // команды внешних объектов --------------------------------------------------------
    [Serializable]
    private struct Command
    {
        public string name;
        public UnityEvent<List<Value>, Value, BoolValue> commandFunction;
        public List<ValueContent.ValueContentType> signature;
    }

    [SerializeField]
    private List<Command> ObjectCommands;

    public UnityEvent<List<Value>, Value, BoolValue> GetCommandMethod(string name)
    {
        foreach (Command command in ObjectCommands)
        {
            if (command.name == name)
            {
                if (command.commandFunction.GetPersistentEventCount() > 0) return command.commandFunction;
                else Debug.LogWarning($"Command {name} no commmand function");

                UnityEvent<List<Value>, Value, BoolValue> empty1 = new UnityEvent<List<Value>, Value, BoolValue>();
                empty1.AddListener(EmptyMethod);
                return empty1;
            }
        }

        UnityEvent<List<Value>, Value, BoolValue> empty = new UnityEvent<List<Value>, Value, BoolValue>();
        empty.AddListener(EmptyMethod);
        return empty;
    }

    public bool isObjectCommand(string name)
    {
        foreach (Command command in ObjectCommands)
        {
            if (command.name == name) return true;
        }
        return false;
    }

    public List<ValueContent.ValueContentType> GetSignature(BaseCommandType command) // получение сигнатур функций
    {
        List<ValueContent.ValueContentType> result;

        switch(command.Type)
        {
            // Для объектных комманд
            case BaseCommandType.CommandType.ObjectCommandType:
                result = GetObjectCommandSignature(((BaseObjectCommand)command).Name);
                if (result != null) return result;
                throw new System.Exception($"No signature for {command.Type} {((BaseObjectCommand)command).Name}");

            // для базовых комманд
            default:
                result = GetBaseCommandSignature(command.Type);
                if (result != null) return result;
                throw new System.Exception($"No signature for {command.Type}");
        }
    }

    public List<ValueContent.ValueContentType> GetObjectCommandSignature(string name)
    {
        foreach (Command command in ObjectCommands)
        {
            if (command.name == name) return command.signature;
        }
        return null;
    }

    public void EmptyMethod(List<Value> arguments, Value returnValue, BoolValue isFinished)
    {
        isFinished.Value = true;
    }



    // базовые команды -----------------------------------------------------------------
    [Serializable]
    private struct BaseCommand
    {
        public string nameInCode;
        public BaseCommandType.CommandType type;
        public List<ValueContent.ValueContentType> signature;
    }

    [SerializeField]
    private List<BaseCommand> BaseCommands;


    public BaseCommandType.CommandType GetBaseCommandType(string name)
    {
        foreach (BaseCommand command in BaseCommands)
        {
            if (command.nameInCode == name) return command.type;
        }
        return BaseCommandType.CommandType.NULL;
    }

    public List<ValueContent.ValueContentType> GetBaseCommandSignature(BaseCommandType.CommandType type)
    {
        foreach (BaseCommand command in BaseCommands)
        {
            if (command.type == type) return command.signature;
        }
        return null;
    }

    private void Awake()
    {
        ScriptSystemSingleton.commandsList = this;
    }
}

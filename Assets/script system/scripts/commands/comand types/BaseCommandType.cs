using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCommandType
{
    public enum CommandType {
        NULL,
        ObjectCommandType, 
        IfCommandType, 
        WhileCommandType,
        MathCommandType};
    protected CommandType type;
    public CommandType Type => type;

    public abstract bool InvokeCommand();

    // установка и проверка параметров команды
    static public void setReturnAndArgumentsToCommand(BaseCommandType command, List<Value> arguments, Value returnValue)
    {
        setReturnAndArgumentsToCommand(command, arguments, returnValue, null);
    }

    static public void setReturnAndArgumentsToCommand(BaseCommandType command, List<Value> arguments, Value returnValue, List<CodePart> codeParts)
    {
        var signature = ScriptSystemSingleton.commandsList.GetSignature(command);

        checkArguments(signature, arguments);
        checkReturnValue(signature, returnValue);

        switch (command.Type)
        {
            case CommandType.ObjectCommandType:  // Для команд объектов
                ((BaseObjectCommand)command).Arguments = arguments;
                ((BaseObjectCommand)command).ReturnValue = returnValue;
                break;

            case CommandType.IfCommandType:  // if
               ((IfCommand)command).Value = arguments[0];
                if (codeParts == null || codeParts[0] == null) throw new System.Exception("No code part in while command");
                ((IfCommand)command).TrueCodePart = codeParts[0];
                if(codeParts.Count > 1 && codeParts[1] != null) ((IfCommand)command).FalseCodePart = codeParts[1];
                break;

            case CommandType.WhileCommandType:  // while
                ((WhileCommand)command).Value = arguments[0];
                if (codeParts == null || codeParts[0] == null) throw new System.Exception("No code part in while command");
                ((WhileCommand)command).Code = codeParts[0];
                break;
        }
    }

    // получение объекта команды по имени
    static public BaseCommandType getCommandFromName(string name)
    {
        // команды сторонних объектов
        if (ScriptSystemSingleton.commandsList.isObjectCommand(name))
        {
            if (name == "Wait") return new WaitCommand();
            if (name == "Print") return new PrintCommand();
            return new BaseObjectCommand(name);
        }

        // базовые команды
        var baseCommand = ScriptSystemSingleton.commandsList.GetBaseCommandType(name);
        if (baseCommand != CommandType.NULL)
        {
            if (baseCommand == CommandType.IfCommandType) return new IfCommand();
            if (baseCommand == CommandType.WhileCommandType) return new WhileCommand();
        }
        throw new System.Exception($"command <{name}> not found");
    }

    // проверка возврата на сигнатуру
    static private void checkReturnValue(List<ValueContent.ValueContentType> signature, Value value)
    {
        if (value != null)
        {
            if (signature[0] != value.Content.Type) throw new System.Exception($"Wrong return value type get {value.Content.Type} need {signature[0]}");
        }
        else
        {
            if (signature[0] != ValueContent.ValueContentType.NoneValueType)
            {
                value = Value.GetNewValueFromType(signature[0]);
            }
        }
    }

    // проверка аргументов на сигнатуру
    static private void checkArguments(List<ValueContent.ValueContentType> signature, List<Value> arguments)
    {
        if (signature.Count - 1 != arguments.Count) throw new System.Exception("Wrong arguments count error");

        for (int i = 0; i < arguments.Count; i++)
        {
            if (signature[i + 1] != ValueContent.ValueContentType.AnyValueType && 
                signature[i + 1] != arguments[i].Content.Type) throw new System.Exception($"Wrong argument type error get {arguments[i].Content.Type} need {signature[i + 1]}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePart
{
    private CodePart parentCodePart;
    public CodePart ParentCodePart => parentCodePart;
    private Dictionary<string, Value> values = new Dictionary<string, Value>();
    private List<BaseCommandType> commands = new List<BaseCommandType>();
    private int commmand = 0;

    public int commnadsCount => commands.Count;

    public CodePart(CodePart parentCodePart)
    {
        this.parentCodePart = parentCodePart;
    }

    public CodePart()
    {
        this.parentCodePart = this;
    }

    public void addValue(string name, Value value)
    {
        Dictionary<string, Value> availableValues = getAvailableValues();
        if (availableValues.ContainsKey(name)) throw new System.Exception($"Duplicate name {name} error");
        if (value != null) values.Add(name, value);
    }

    public Value getValue(string name)
    {
        Dictionary<string, Value> availableValues = getAvailableValues();
        Value val;
        if (availableValues.TryGetValue(name, out val)) return val;
        else throw new System.Exception($"Undefined value <{name}>");
    }

    public void addCommand(BaseCommandType command)
    {
        commands.Add(command);
    }

    public Dictionary<string, Value> getAvailableValues()
    {
        if (parentCodePart == this) return new Dictionary<string, Value>(values);
        if (parentCodePart == null) throw new System.Exception("getAvailableValues NULL parent code part");
        Dictionary<string, Value> parentValues = parentCodePart.getAvailableValues();
        foreach (KeyValuePair<string, Value> entry in values)
        {
            parentValues.Add(entry.Key, entry.Value);
        }
        return parentValues;
    }

    public string getStringInfo()
    {
        return "commands: " + commands.Count.ToString() + "   values: " + values.Keys.Count;
    }

    public bool execute()
    {
        if (commands.Count == 0) return true;
        if(commands[commmand].InvokeCommand())
        {
            commmand++;
            if (commmand < commands.Count) return false;
            else
            {
                commmand = 0;
                return true;
            }
        }
        else return false;
    }
}

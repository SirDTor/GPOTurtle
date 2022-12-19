using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesList : MonoBehaviour
{
    private Dictionary<string, Value> valuesRegister = new Dictionary<string, Value>();

    void Awake()
    {
        //ScriptSystemSingleton.valuesList = this;
    }

    public void addValue(string name, Value val)
    {
        if (valuesRegister.ContainsKey(name)) throw new System.Exception($"Duplicate name {name} error");
        if (val != null) valuesRegister.Add(name, val);
    }

    public Value getValue(string name)
    {
        Value val;
        if (valuesRegister.TryGetValue(name, out val)) return val;
        else return null;
    }

    public bool isInRegister(string name)
    {
        return valuesRegister.ContainsKey(name);
    }

}

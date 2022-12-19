using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueContent
{
    public enum ValueContentType { NoneValueType, AnyValueType, IntValueType, FloatValueType, BoolValueType };

    static public Dictionary<string, ValueContentType> valuesContentTypesDict = new Dictionary<string, ValueContentType>
    {
        {"int", ValueContentType.IntValueType},
        {"float", ValueContentType.FloatValueType},
        {"bool", ValueContentType.BoolValueType},
        {"none", ValueContentType.NoneValueType}
    };

    private ValueContentType type = ValueContentType.NoneValueType;
    public ValueContentType Type => type;

    public ValueContent(ValueContentType type)
    {
        this.type = type;
    }

    static public ValueContent GetNewValueContentFromType(ValueContentType type)
    {
        switch (type)
        {
            case ValueContentType.IntValueType:
                return new IntValue(0);

            case ValueContentType.FloatValueType:
                return new FloatValue(0);

            case ValueContentType.BoolValueType:
                return new BoolValue(false);

            default:
                return new ValueContent(ValueContentType.NoneValueType);
        }
    }

    static public ValueContent getNewValueContentFromString(string typeName)
    {
        return GetNewValueContentFromType(getValueContentTypeFromString(typeName));
    }

    static public ValueContentType getValueContentTypeFromString(string typeName)
    {
        ValueContentType result;
        if (valuesContentTypesDict.TryGetValue(typeName, out result))
        {
            return result;
        }
        throw new System.Exception($"Undefined value type <{typeName}>");
    }

    static public ValueContent getConstantContentFromString(string input)
    {
        if (input == "true") return new BoolValue(true);
        if (input == "false") return new BoolValue(false);

        try
        {
            return new IntValue(int.Parse(input));
        }
        catch (System.Exception) { }

        try
        {
            return new FloatValue(float.Parse(input));
        }
        catch (System.Exception) { }

        throw new System.Exception($"Undefined value <{input}>");
    }
}

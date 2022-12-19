using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value
{
    private ValueContent content;
    public ValueContent Content => content;

    public static bool consoleInfo = false;

    private bool calculated = false;
    public ValueContent getResultContent(out bool isCalculated)
    {
        if(calculated == true)
        {
            calculated = false;
            isCalculated = true;
            return content;
        }
        if (simple) {
            if (consoleInfo) Debug.LogWarning("---Simple value extract---");
            isCalculated = true;
            return content;
        }
        else
        {  
            if (calculation.execute())
            {
                if (consoleInfo) Debug.LogWarning("---Value calculating end---");
                calculated = true;
                isCalculated = false;
                return content;
            }
            else
            {
                if (consoleInfo) Debug.LogError("---Value calculating---");
                isCalculated = false;
                return content;
            }
        }
    }

    private CodePart calculation = null;

    private bool simple;
    public bool Simple
    {
        get => simple;
    }

    public Value(ValueContent content)
    {
        this.content = content;
        simple = true;
    }

    public Value(ValueContent content, CodePart calculation)
    {
        this.content = content;
        this.calculation = calculation;
        simple = false;
    }

    static public Value GetNewValueFromType(ValueContent.ValueContentType type)
    {
        return new Value(ValueContent.GetNewValueContentFromType(type));
    }

    static public Value getNewValueFromString(string typeName)
    {
        return new Value(ValueContent.getNewValueContentFromString(typeName));
    }

    static public ValueContent.ValueContentType getValueTypeFromString(string typeName)
    {
        return ValueContent.getValueContentTypeFromString(typeName);
    }

    static public Value getConstantFromString(string input)
    {
        return new Value(ValueContent.getConstantContentFromString(input));
    }

    static public Value getValueFromString(string input, CodePart parentCodePart) 
    {
        if (input != string.Empty)
        {
            // переменная
            try { return parentCodePart.getValue(input); }
            catch (System.Exception)
            {
                // константа
                return getConstantFromString(input);
            }  
        }
        throw new System.Exception($"getValueFromString empty input string");
    }
}

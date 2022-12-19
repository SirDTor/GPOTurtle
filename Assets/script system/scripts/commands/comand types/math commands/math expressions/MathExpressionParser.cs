using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExpressionParser
{
    public static bool consoleInfo = true;

    public static Value parseMathExpression(string input, CodePart parentCodePart)
    {
        if (input == string.Empty) return null;

        var lexemes = parseStringToLexemes(input);
        if (consoleInfo) foreach(var lex in lexemes) Debug.LogError("<" + lex.lexeme + "> " + lex.type.ToString());
        
        Stack<Value> valuesStack = new Stack<Value>();
        Stack<MathLexeme> operatorsStack = new Stack<MathLexeme>();
        CodePart calculation = new CodePart(parentCodePart);

        // ������ ������ � ������������ � �������� �������� ��. ����� https://www.youtube.com/watch?v=Vk-tGND2bfc
        if (consoleInfo) Debug.Log("Start parsing: " + input);
        for (int i = 0; i < lexemes.Count; i++)
        {
            switch(lexemes[i].type)
            {
                case MathLexeme.MathLexemeType.Lexeme:
                    valuesStack.Push(Value.getValueFromString(lexemes[i].lexeme, parentCodePart));
                    if (consoleInfo) Debug.Log("Add value: " + lexemes[i].lexeme);
                    break;

                case MathLexeme.MathLexemeType.Function:
                    CodePart code = new CodePart(parentCodePart);

                    int nameEnd = lexemes[i].lexeme.IndexOf('(');
                    if (nameEnd == -1) throw new System.Exception($"in <{lexemes[i].lexeme}> function simbol <(> not found");
                    if (lexemes[i].lexeme[lexemes[i].lexeme.Length - 1] != ')') throw new System.Exception($"in <{lexemes[i].lexeme}> function don't end with simbol <)>");
                    string name = lexemes[i].lexeme.Substring(0, nameEnd);

                    BaseCommandType command = BaseCommandType.getCommandFromName(name);

                    string[] strArguments = lexemes[i].lexeme.Substring(nameEnd + 1, lexemes[i].lexeme.Length - nameEnd - 2).Split(',');
                    var arguments = CodeParser.getArgumentsFromString(strArguments, parentCodePart);
                    
                    Value returnVal = Value.GetNewValueFromType(ScriptSystemSingleton.commandsList.
                        GetSignature(command)[0]);

                    BaseCommandType.setReturnAndArgumentsToCommand(command, arguments, returnVal);

                    code.addCommand(command);
                    Value val = new Value(returnVal.Content, code);

                    valuesStack.Push(val);
                    if (consoleInfo) Debug.Log("Add function: " + lexemes[i].lexeme);
                    break;

                case MathLexeme.MathLexemeType.Operator:
                    if (operatorsStack.Count > 0 &&
                        operatorsStack.Peek().priority != -1 &&
                        operatorsStack.Peek().priority >= lexemes[i].priority)
                    {
                        while(operatorsStack.Count > 0 && operatorsStack.Peek().priority >= lexemes[i].priority)
                        {
                            calculation.addCommand(parseMathCommand(valuesStack, operatorsStack));
                        }
                    }
                    operatorsStack.Push(lexemes[i]);
                    break;

                case MathLexeme.MathLexemeType.ClosingBar:
                    while(operatorsStack.Count > 0 && operatorsStack.Peek().type != MathLexeme.MathLexemeType.OpeningBar)
                    {
                        calculation.addCommand(parseMathCommand(valuesStack, operatorsStack));
                    }
                    if(operatorsStack.Count > 0) operatorsStack.Pop();
                    break;

                default:
                    operatorsStack.Push(lexemes[i]);
                    break;

            }
        }
        if (consoleInfo) Debug.Log("Parsing remaining commands in commands stack");
        while (operatorsStack.Count > 0)
        {
            calculation.addCommand(parseMathCommand(valuesStack, operatorsStack));
        }

        if (consoleInfo) Debug.Log("end of parse. Result: " + calculation.getStringInfo() + " " + valuesStack.Count);
        if(calculation.commnadsCount > 0) return new Value(valuesStack.Pop().Content, calculation);
        else return valuesStack.Pop();
    } 

    // �������� ������ �� ������ ������
    public static List<MathLexeme> parseStringToLexemes(string input)
    {
        List<MathLexeme> result = new List<MathLexeme>();
        int lastLexemeEnd = 0;

        for(int i = 0; i < input.Length; i++)
        {
            int separatorLength = 0;
            if (isSeparator(input, i, out separatorLength))
            { 
                // �������� �� ������� "func(dhdhd)" � ���� �� ����������� ������
                if (input[i] == '(' && i >= 1 && char.IsLetterOrDigit(input[i - 1]))
                {
                    int barceCounter = 1;
                    while (barceCounter > 0 && i < input.Length - 1)
                    {
                        i++;
                        if (input[i] == '(') barceCounter++;
                        if (input[i] == ')') barceCounter--;
                    }
                    result.Add(
                    new MathLexeme(input.Substring(lastLexemeEnd, i + 1 - lastLexemeEnd),
                    MathLexeme.MathLexemeType.Function));
                    lastLexemeEnd = i + 1;
                    continue;
                }

                // ���������� ������� �� ����������/���������/������� ����� �������������
                if (i != lastLexemeEnd)
                {
                    // ������� �������
                    result.Add(
                    new MathLexeme(input.Substring(lastLexemeEnd, i - lastLexemeEnd),
                    MathLexeme.MathLexemeType.Lexeme));
                }

                // ��������� ������� �� �����������
                string separartor = input.Substring(i, separatorLength);
                result.Add(new MathLexeme(separartor, getSeparatorType(separartor),
                    getOperatorPriority(separartor)));

                i += separatorLength;
                lastLexemeEnd = i;
                i--;
            }
            if (i == input.Length - 1 && lastLexemeEnd != i + 1) result.Add(new MathLexeme(
                input.Substring(lastLexemeEnd, input.Length - lastLexemeEnd),
                MathLexeme.MathLexemeType.Lexeme));
        }

        return result;
    }

    // �������� �� ��������� �� ������� ������ ������� ����������
    private static bool isSeparator(string input, int start, out int separatorLengt)
    {
        // ������� ������������
        foreach (string separator in MathCommandsList.separatorsList)
        {
            bool isEqual = true;
            // ������� ������� ����������
            for(int i = 0; i < separator.Length; i++)
            {
                if(separator[i] != input[start + i])
                {
                    isEqual = false;
                    break;
                }
            }
            if (isEqual)
            {
                // ��� ����������
                separatorLengt = separator.Length;
                return true;
            }
        }
        // ��� �� ����������
        separatorLengt = 0;
        return false;
    }

    // �������������� ��������� �������� � ����� ���������� � ��������
    // � �������� ��������������� ��������� � ����� �������� � ��������� 
    // ������� �� ����� � ���������� ���������� (������ �� ����������) � ���� ����������
    private static BaseCommandType parseMathCommand(Stack<Value> valuesStack, Stack<MathLexeme> operatorsStack)
    {
        Value result;
        BaseCommandType resCommand;
        string lex;
        try{
            lex = operatorsStack.Pop().lexeme;
            MathCommandsList.mathTypes type;
            if(MathCommandsList.mathOperationsTypes.TryGetValue(lex, out type))
            {
                switch (type)
                {
                    case MathCommandsList.mathTypes.TwoArguments:
                        resCommand = TwoArgumentsMathCommand.getNewTwoArgumentsCommand(out result, lex, valuesStack);
                        valuesStack.Push(result);
                        return resCommand;

                    case MathCommandsList.mathTypes.PrefixOneArgument:
                        resCommand = OneArgumentMathCommand.getNewPrefixOneArgumentCommand(out result, lex, valuesStack);
                        valuesStack.Push(result);
                        return resCommand;

                    default: throw new System.Exception($"undefined math type <{type}>");
                }
            }
            else throw new System.Exception($"undefined math operator <{lex}>");
        }
        catch(System.InvalidOperationException)
        {
            throw new System.Exception("wrong math expression");
        }
        catch(System.Exception e)
        {
            throw new System.Exception(e.Message);
        }
    }

    // ��������� ���� ������� ���������� �� ������ � ���� �����������
    private static MathLexeme.MathLexemeType getSeparatorType(string input)
    {
        if (input == "(") return MathLexeme.MathLexemeType.OpeningBar;
        if (input == ")") return MathLexeme.MathLexemeType.ClosingBar;
        return MathLexeme.MathLexemeType.Operator;
    }

    private static int getOperatorPriority(string input)
    {
        int priority;
        if (MathCommandsList.mathOperationsPriority.TryGetValue(input, out priority))
        {
            return priority;
        }
        else return -1;
    }
}

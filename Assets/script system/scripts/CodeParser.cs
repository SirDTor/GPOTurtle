using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CodeParser
{
    static public int commandCounter = 0;
    static public CodePart parseCodePart(string inputString, CodePart parentCodePart)
    {
        inputString += ' ';

        CodePart result;
        if (parentCodePart == null) result = new CodePart();
        else result = new CodePart(parentCodePart);

        List<string> strings = splitToStrings(inputString);


        foreach(string str in strings)
        {
            //result.addCommand(parseLine(str));
            //try
            //{
                result.addCommand(parseLine(str, result));
            //}
            //catch(System.Exception e)
            //{
            //    if(e.Message.Contains("in command #"))
            //    throw new System.Exception(e.Message);
            //    else throw new System.Exception($"{e.Message} in command #{commandCounter + 1}");
            //}

            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] == '{')
                {
                    int braceCounter = 1;
                    while (braceCounter > 0 && i < str.Length - 1)
                    {
                        i++;
                        if (str[i] == '{') braceCounter++;
                        if (str[i] == '}') braceCounter--;
                    }
                }
            }
        }

        return result;
    }

    // парсинг строки (от токи с запятой до точки с запятой)
    static public BaseCommandType parseLine(string inputString, CodePart parentCodePart)
    {
        string normalisedString = delEndSemicolon(normilizeString(inputString));
        BaseCommandType command;

        // парсинг возвращаемого значения
        int returnEnd = normalisedString.IndexOf('=');
        int codePartStart = normalisedString.IndexOf('{');
        int nameEnd = normalisedString.IndexOf('(');
        if (returnEnd == -1 || (returnEnd > nameEnd && nameEnd > 0) 
            || (codePartStart != -1 && returnEnd > codePartStart)) returnEnd = 0;
        Value returnValue = getReturnValueFromString(normalisedString.Substring(0, returnEnd), parentCodePart);
        
        // парсинг комманды
        if(returnEnd == 0) command = parseCommand(normalisedString, returnValue, parentCodePart);
        else command = parseCommand(normalisedString.Substring(returnEnd + 1), returnValue, parentCodePart);

        return command;
    }


    // парсинг команды (все что после = )
    static private BaseCommandType parseCommand(string commandString, Value returnValue, CodePart parentCodePart)
    {
        int nameEnd = commandString.IndexOf('(');
        //Debug.Log($"nameStart = {nameStart}, nameEnd = {nameEnd}");

        BaseCommandType command = null;

        // если есть "буква или число"( (команда или функция) + нет математики за пределами ()
        int firstMath = commandString.IndexOfAny(MathCommandsList.mathSimbolsList);
        int lastMath = commandString.LastIndexOfAny(MathCommandsList.mathSimbolsList);
        if (nameEnd > 0 && char.IsLetterOrDigit(commandString[nameEnd - 1]) &&
            (firstMath == -1 || firstMath > nameEnd) && (lastMath == -1 || lastMath < commandString.LastIndexOf(')')))
        {
            // отделение названия команды
            string commandName = commandString.Substring(0, nameEnd);

            // отделение аргументов
            int codePartStart = commandString.IndexOf('{');
            int argumentsEnd = commandString.LastIndexOf(')', codePartStart > 0 ? codePartStart : commandString.Length - 1);
            if (argumentsEnd < nameEnd) throw new System.Exception($"Simbol \")\" after {commandName} not found");

            // отделение 1 вложенной области видимости
            List<CodePart> codeParts = null;
            int codeStart = commandString.IndexOf('{');
            if(argumentsEnd < codeStart)
            {
                codeParts = new List<CodePart>();
                for(int i = codeStart; i < commandString.Length; i++)
                {
                    int barceCounter = 1;
                    // скип до ответной }
                    while (barceCounter > 0 && i < commandString.Length - 1)
                    {
                        i++;
                        if (commandString[i] == '{') barceCounter++;
                        if (commandString[i] == '}') barceCounter--;
                    }
                    if(barceCounter != 0) throw new System.Exception("Simbol \"}\" not found");
                    codeParts.Add(parseCodePart(commandString.Substring(codeStart + 1, i - codeStart - 1), parentCodePart));
                    // проверка наличия следующей области видимости
                    codeStart = commandString.IndexOf('{', i);
                    if (codeStart == -1) break;
                    if (codeStart <= i && i != commandString.Length - 1) throw new System.Exception("Simbol \"{\" not found");
                    // проверка ключевого слова
                    string keyWord = commandString.Substring(i + 1, codeStart - i - 1);
                    i = codeStart - 1;
                    if (!isKeyWord(keyWord)) throw new System.Exception($"Wrond expression \"{keyWord}\"");
                }
            }

            // парсинг аргументов
            string[] strArguments = commandString.Substring(nameEnd + 1, argumentsEnd - nameEnd - 1).Split(',');

            var arguments = getArgumentsFromString(strArguments, parentCodePart);

            // получение команды и по
            command = BaseCommandType.getCommandFromName(commandName);

            try
            {
                // установка аргументов, областей видимости, и возвращаемого значения
                BaseCommandType.setReturnAndArgumentsToCommand(command, arguments, returnValue, codeParts);
            }
            catch(System.Exception e)
            {
                throw new System.Exception($"{e.Message} in {commandName}");
            }
        }

        // присвоение значения
        if (command == null && commandString.LastIndexOfAny(new char[] { '{', '}' }) == -1)
        {
            command = new EqualizeCommand(returnValue, MathExpressionParser.parseMathExpression(commandString, parentCodePart)); ;  
        }

        if (command == null) throw new System.Exception("Wrong command syntax");
        else commandCounter++;
        return command;
    }

    // проверка является ли ключевым словом
    static private bool isKeyWord(string input)
    {
        if (input == "else") return true;
        return false;
    }

    // удаление лишних знаков в строке
    static private string normilizeString(string input)
    {
        string result = string.Empty;
        foreach (char current in input)
        {
            if (current != ' ' && current != '\n') result += current;
        }
        return result;
    }


    // удаление последнего двоеточия в блоке кода
    static private string delEndSemicolon(string input)
    {
        int lastSemicolon = input.LastIndexOf(';');
        int lastBarce = input.LastIndexOf('}');
        if (lastSemicolon > lastBarce) return input.Substring(0, lastSemicolon);
        return input;
    }

    // разделение блока кода на строки
    static private List<string> splitToStrings(string input)
    {
        List<string> result = new List<string>();
        int lastEnd = -1;

        for (int i = 0; i < input.Length; i++)
        {
            if(input[i] == ';')
            {
                i++;
                while (i < input.Length - 1 && input[i] == ' ')
                {
                    i++;
                }
                if (input[i] != '\n') i--;
                if(i + 1 < input.Length)
                result.Add(input.Substring(lastEnd + 1, i - lastEnd));
                lastEnd = i;
            }

            if(input[i] == '{')
            {
                string keyword;
                do
                {
                    int braceCounter = 1;
                    while (braceCounter > 0 && i < input.Length - 1)
                    {
                        i++;
                        if (input[i] == '{') braceCounter++;
                        if (input[i] == '}') braceCounter--;
                    }
                    keyword = string.Empty;
                    int nextBar = input.IndexOf('{', i);
                    if (nextBar > i) keyword = input.Substring(i + 1, nextBar - i - 1);
                } while (isKeyWord(keyword));
                result.Add(input.Substring(lastEnd + 1, i - lastEnd));
                lastEnd = i;
            }
        }

        return result;
    }

    //парсинг аргументов из строки в сиписок ссылок
    static public List<Value> getArgumentsFromString(string[] input, CodePart parentCodePart) 
    {
        var arguments = new List<Value>();
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == string.Empty) continue;
            arguments.Add(MathExpressionParser.parseMathExpression(input[i], parentCodePart));
        }
        return arguments;
    }

    // получение ссылки на переменную возврата из строки
    static private Value getReturnValueFromString(string input, CodePart parentCodePart) 
    {
        Value result = null;

        if (input != string.Empty)
        {
            int typeEnd = input.IndexOf(':');
            if (typeEnd != -1)
            {
                // создание новой переменной
                result = Value.getNewValueFromString(input.Substring(0, typeEnd));
                string valName = input.Substring(typeEnd + 1);
                parentCodePart.addValue(valName, result);
            }
            else
            {
                // попытка получения переменной
                result = parentCodePart.getValue(input);
            }
        }
        return result;
    }
}



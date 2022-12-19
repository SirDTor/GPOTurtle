using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathCommandsList
{
    static MathCommandsList()
    {
        separatorsList.AddRange(mathOperationsPriority.Keys);
    }

    public static Dictionary<string, int> mathOperationsPriority = new Dictionary<string, int>
    {
        {"&&", 30}, // ������ ������� ����� � ������ ����� ���������!! ����� ����� ��� 
        {"||", 30}, // � ����������� �������� ����� �� 2 ���������
        {"==", 40},
        {"<=", 40},
        {">=", 40},
        {"<", 40},
        {">", 40},

        {"+", 50},
        {"-", 50},
        {"*", 60},
        {"/", 60},
        {"%", 70},
        {"^", 80},
        {"!", 100},
    };

    public enum mathTypes { PrefixOneArgument, TwoArguments};

    public static Dictionary<string, mathTypes> mathOperationsTypes = new Dictionary<string, mathTypes>
    {
        {"&&", mathTypes.TwoArguments}, // ������ ������� ����� � ������ ����� ���������!! ����� ����� ��� 
        {"||", mathTypes.TwoArguments}, // � ����������� �������� ����� �� 2 ���������
        {"==", mathTypes.TwoArguments},
        {"<=", mathTypes.TwoArguments},
        {">=", mathTypes.TwoArguments},
        {"<", mathTypes.TwoArguments},
        {">", mathTypes.TwoArguments},

        {"+", mathTypes.TwoArguments},
        {"-", mathTypes.TwoArguments},
        {"*", mathTypes.TwoArguments},
        {"/", mathTypes.TwoArguments},
        {"%", mathTypes.TwoArguments},
        {"^", mathTypes.TwoArguments},
        {"!", mathTypes.PrefixOneArgument}
    };

    public static List<string> separatorsList = new List<string>
    {
        "(", ")"
    };

    public static char[] mathSimbolsList = new char[]
    {
        '+', '-', '*', '/', '%', '=', '|', '&', '<', '>', '^'
    };
}

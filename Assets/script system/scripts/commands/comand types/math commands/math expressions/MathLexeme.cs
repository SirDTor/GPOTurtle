using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLexeme
{
    public string lexeme;
    public MathLexemeType type;
    public int priority;

    public enum MathLexemeType
    {
        Lexeme, Operator, OpeningBar, ClosingBar, Function
    }

    public MathLexeme(string lexeme, MathLexemeType type, int priority)
    {
        this.lexeme = lexeme;
        this.type = type;
        this.priority = priority;
    }

    public MathLexeme(string lexeme, MathLexemeType type)
    {
        this.lexeme = lexeme;
        this.type = type;
        priority = -1;
    }
}

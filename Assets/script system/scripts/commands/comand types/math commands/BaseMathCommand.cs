using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMathCommand : BaseCommandType
{
    public enum MathCommandType { 
        EqualizeCommandType,
        AddCommandType,
        SubtractCommandType,
        MultiplyCommandType,
        DivideCommandType,
        ModCommandType,
        EqualCommandType,
        MoreCommandType,
        LessCommandType,
        MoreOrEqualCommandType,
        LessOrEqualCommandType
    };

    private MathCommandType mathCommandType;
    public MathCommandType MathType => mathCommandType;

    public BaseMathCommand(MathCommandType mathCommandType)
    {
        this.mathCommandType = mathCommandType;
        type = CommandType.MathCommandType;
    }
}

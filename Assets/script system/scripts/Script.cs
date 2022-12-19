using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script
{
    private CodePart mainCodePart;
    public CodePart MainCodePart {
        get => mainCodePart;
        set => mainCodePart = value;
    }

    public bool execute()
    {
        return mainCodePart.execute();
    }
}

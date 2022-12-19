using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptExecutor : MonoBehaviour
{
    [SerializeField]
    public bool OnOffScriptExecutor = true;
    [SerializeField]
    public bool PauseScript = false;

    private float stepTime = 0.075f;
    public float StepTime => stepTime;

    private bool isRun = false;
    public bool IsRun => isRun;

    //private Value res;


    private Script script;
    public Script Script
    {
        set
        {
            if (!isRun) script = value;
        }
        get => script;
    }

    public void StartScript()
    {
        StartCoroutine(StepCorutine(stepTime));
        isRun = true;
    }

    private IEnumerator StepCorutine(float stepTime)
    {
        isRun = true;
        Debug.Log("$$$ ===================== start ===================== $$$");
        while (!script.execute())
        {
            Debug.Log("============ NextStep ============");
            yield return new WaitForSeconds(stepTime);
            while (PauseScript) yield return new WaitForSeconds(stepTime);
        }
        Debug.Log("$$$ ===================== finish ===================== $$$");
        isRun = false;
    }

    void test()
    {
        //throw new System.Exception("error");
    }

    private string error = string.Empty;
    void Start()
    {
        string test = 
            "while(InspectStep()){  \n" +
            "  Move(1);\n" +
            "  Print(GetFuel());\n" +
            "  if(!InspectStep())\n" +
            "  {\n" +
            "    Turn(1);\n" +
            "    SwitchColor() ;\n" +
            "  }\n" +
            "}\n" +
            "Wait(10) ;" +
            "SwitchColor() ;";

        script = new Script();
        if (OnOffScriptExecutor)
        {
            //try
            //{
                script.MainCodePart = CodeParser.parseCodePart(test, null);
                //CodePart baseCodePart = new CodePart();
                //baseCodePart.addValue("test1", new Value(new IntValue(200)));
                //baseCodePart.addValue("test2", new Value(new IntValue(5)));
                //baseCodePart.addValue("test3", new Value(new IntValue(10)));
                //res = MathExpressionParser.parseMathExpression("test3-test1/(2*(test2+15))", baseCodePart);
            //}
            //catch (System.Exception e)
            //{
            //    error = e.Message;
            //}

            if (error != string.Empty) Debug.LogError(error);
        }
        
        //bool isCalculated = false;
        //ValueContent result = res.getResultContent(out isCalculated);
        //while (!isCalculated)
        //{
        //    result = res.getResultContent(out isCalculated);
       // }
       // Debug.Log(((IntValue)result).Value);
    }

    private bool isFitst = true;

    private void FixedUpdate()
    {
        if(isFitst && OnOffScriptExecutor && error == string.Empty)
        {
            isFitst = false;
            StartScript();
            //bool isCalculated = false;
            //ValueContent result = res.getResultContent(out isCalculated);
            //if (isCalculated)
            //{
            //    Debug.Log(((IntValue)result).Value);
            //    isFitst = false;
            //}
        }
    }


}

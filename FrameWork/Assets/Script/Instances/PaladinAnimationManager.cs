using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PaladinAnimationManager : LEUnitAnimatorManager
{

    protected override void Start()
    {
        base.Start();
    }

    public override void UpdateAnimation()
    {
        
    }

    [Visin1_1.AMBCallback()]
    public void DoSomeThing()
    {
        Debug.Log("Do SomeThing");
    }

    [Visin1_1.AMBCallback()]
    public void DoWhatEverThing()
    {
        Debug.Log("Do DoWhatEverThing");
    }

    [Visin1_1.AMBCallback()]
    public void DoSomeThingElse()
    {
        Debug.Log("Do DoSomeThingElse");
    }

    [Visin1_1.AMBCallback()]
    public void PrintNumberOf(float number)
    {
        Debug.Log(number);
    }

    [Visin1_1.AMBCallback()]
    public void SetValue(bool vaule)
    {
        Debug.LogFormat("The Bool Value is : {0}", vaule);
    }

    [Visin1_1.AMBCallback()]
    public void SetVector3(Vector3 v)
    {
        Debug.LogFormat("The Vector3 Value is : {0}", v);
    }

    [Visin1_1.AMBCallback()]
    public void DoSomeShit(Vector3 v)
    {

    }

    public override void InvokeMethod(MethodInfo m,object[] obj)
    {
        m.Invoke(this,obj);
    }

}

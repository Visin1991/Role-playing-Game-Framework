using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class StateMachineEditorHelper{

	public static List<MethodInfo> GetMethodOfType<T>() where T: class
    {
        List<MethodInfo> methods = new List<MethodInfo>();
        MethodInfo[] ms = typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        foreach (MethodInfo m in ms)
        {
            SMCondition attr = Attribute.GetCustomAttribute(m, typeof(SMCondition)) as SMCondition;
            if (attr != null)
            {
                methods.Add(m);
            }
        }
        return methods;
    }

    public static List<string> GetAllMethodNameWithSMSCondition_In_T<T>() where T : class
    {
        List<MethodInfo> methods = GetMethodOfType<T>();
        List<string> methodNames = new List<string>();
        foreach (MethodInfo m in methods)
        {
            if (m.ReturnType == typeof(bool))
            {
                methodNames.Add(m.Name);
            }
        }
        return methodNames;
    }

}

[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class SMCondition : System.Attribute
{
    public SMCondition()
    {

    }
}



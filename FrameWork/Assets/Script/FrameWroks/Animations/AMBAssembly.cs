using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CreateAssetMenu(fileName = "Data", menuName = "AMB/Assembly", order = 1)]
public class AMBAssembly : ScriptableObject
{
    public System.Type animationManagerType;
    public List<MethodInfo> callbackFunctions;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[Node(false,"StateMachine/New State",new System.Type[] { typeof(StateMachineCanvas)})]
public class PatrolStateNode : BaseStateNode {

    private Vector2 scroll;

    private const int StartValue = 276;
    protected const int SizeValue = 24;

    [UnityEngine.SerializeField]
    List<Transition> _allTransition;

    [UnityEngine.SerializeField]
    List<StateEnter> allEnters;

    float EnterStartValue = 60;

    StateMachineCanvas canvas;


    public override Node Create(Vector2 pos)
    {
        PatrolStateNode node = CreateInstance<PatrolStateNode>();

        node.rect.position = pos;
        node.name = "Base State Node";

        //Previous Node Connections
        node.CreateInput("Enter Node", "StateEnter", NodeSide.Left, 30);

        //node.CreateOutput("Exit Node", "StateOut", NodeSide.Right, 30);

        node.stateName = "defualt State";

        node._allTransition = new List<Transition>();
        node.allEnters = new List<StateEnter>();
        //node.

        return node;
    }

    protected internal override void NodeGUI()
    {
        
#if UNITY_EDITOR
        EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(true));
        {
            EditorGUILayout.BeginVertical("Box");
            {
                GUILayout.BeginHorizontal();
                stateName = EditorGUILayout.TextField("", stateName);
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();


        GUILayout.BeginVertical("Box");
        {
            GUILayout.ExpandWidth(false);

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                AddNewInput();
                IssueEditorCallBacks_Input();
            }

            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                RemoveLastTransition_Input();
            }

            GUILayout.Label("Options", NodeEditorGUI.nodeLabelBoldCentered);
            if (GUILayout.Button("+", GUILayout.Width(20)))
            {
                if (canvas.conditionFuncs != null)
                {
                    if (canvas.conditionFuncs.Count > 0)
                    {
                        AddNewOption();
                        IssueEditorCallBacks_Output();
                    }
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            DrawOptions();

            GUILayout.ExpandWidth(false); 
        }
        GUILayout.EndVertical();
#endif
    }

    private void OnEnable()
    {
        canvas = GetCanvas() as StateMachineCanvas;
        if (canvas == null) { return; }
        if (canvas.conditionFuncs != null)
        {
            if (canvas.conditionFuncs.Count > 0)
            {
                if(_allTransition == null){ return; }
                for (int i = 0; i < _allTransition.Count; i++)
                {
                    Transition t = _allTransition[i];
                    t.conditionFuncIndex =  canvas.conditionFuncs.FindIndex(c => c == t.condition);
                    if (t.conditionFuncIndex < 0)
                    {
                        t.conditionFuncIndex = 0;
                    }
                }
            }
        }
    }

    private void DrawOptions()
    {
#if UNITY_EDITOR
        EditorGUILayout.BeginVertical();

        for (int i = 0; i < _allTransition.Count; i++)
        {
            Transition t = _allTransition[i];
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(t.NodeOutputIndex + ".", GUILayout.MaxWidth(15));

            if(canvas == null) canvas = GetCanvas() as StateMachineCanvas;

            if (canvas.conditionFuncs != null)
            {
                if (canvas.conditionFuncs.Count > 0)
                {
                    if (t.conditionFuncIndex == 0){ GUI.color = Color.red; }
                    t.conditionFuncIndex = EditorGUILayout.Popup(t.conditionFuncIndex, canvas.conditionFuncs.ToArray(),GUILayout.MaxWidth(160));
                    t.condition = canvas.conditionFuncs[t.conditionFuncIndex];
                    GUI.color = Color.white;
                }
            }

            OutputKnob(_allTransition[i].NodeOutputIndex);
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                _allTransition.RemoveAt(i);
                Outputs[t.NodeOutputIndex].Delete();
                rect = new Rect(rect.x, rect.y, rect.width, rect.height - SizeValue);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.Space(4);
        }

        EditorGUILayout.EndVertical();
#endif
    }

    //Define Color and width......
    private void AddNewOption()
    {
        Transition t = new Transition();
        float rPos = StartValue + _allTransition.Count * SizeValue;
        float lPos = rect.height;
        float height = lPos > rPos ? lPos : rPos;
        NodeOutput o =CreateOutput("Exit Node", "StateOut", NodeSide.Right, rPos);
        o.typeData.Color = Color.yellow;

        rect = new Rect(rect.x, rect.y, rect.width, height);

        t.NodeOutputIndex = Outputs.Count - 1;
        _allTransition.Add(t);
    }

    private void RemoveLastTransition_Output()
    {
        if (_allTransition.Count > 1)
        {
            Transition t = _allTransition.Last();
            _allTransition.Remove(t);
            Outputs[t.NodeOutputIndex].Delete();

            float rPos = StartValue + _allTransition.Count * SizeValue;
            float lPos = rect.height - SizeValue;
            float height = lPos > rPos ? lPos : rPos;

            rect = new Rect(rect.x, rect.y, rect.width, height);
        }
    }

    private void IssueEditorCallBacks_Output()
    {
        Transition t = _allTransition.Last();
        NodeEditorCallbacks.IssueOnAddNodeKnob(Outputs[t.NodeOutputIndex]);
    }

    private void AddNewInput()
    {
        StateEnter e = new StateEnter();
        float lPos = EnterStartValue + allEnters.Count * SizeValue;
        float rPos = rect.height;
        float height = lPos > rPos ? lPos : rPos;
        CreateInput("Enter Node", "StateEnter", NodeSide.Left, lPos);

        rect = new Rect(rect.x, rect.y, rect.width, height);

        e.NodeInputIndex = Inputs.Count - 1;
        allEnters.Add(e);
    }

    private void RemoveLastTransition_Input()
    {
        if (Inputs.Count > 0)
        {
            StateEnter e = allEnters.Last();
            allEnters.Remove(e);
            Inputs[e.NodeInputIndex].Delete();

            float lPos = EnterStartValue + allEnters.Count * SizeValue;
            float rPos = rect.height - SizeValue;
            float height = lPos > rPos ? lPos : rPos;

            rect = new Rect(rect.x, rect.y, rect.width, height);
        }
    }

    private void IssueEditorCallBacks_Input()
    {
        StateEnter e = allEnters.Last();
        NodeEditorCallbacks.IssueOnAddNodeKnob(Inputs[e.NodeInputIndex]);
    }

    [System.Serializable]
    public class Transition
    {
        public string condition = "null";
        public string additionalCall = "null";
        public int NodeOutputIndex;
        public int conditionFuncIndex;
    }

    [System.Serializable]
    public class StateEnter
    {
        public int NodeInputIndex;
    }


}

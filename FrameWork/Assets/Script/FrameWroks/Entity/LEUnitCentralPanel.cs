using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自动化计划目标：
///     
/// </summary>
//Living Entity Unity Central Panel
// Living Entity单位 的中心主板， 相当于电脑中连接 鼠标键盘显示器等等一系列东西的中介
// 在此处连接 动画UI 控制键输入等等。。。。
//
//把 Processor 从Central Panel 中抽出来的好处：
//     1.随时更换 Processor.
public abstract class LEUnitCentralPanel : MonoBehaviour{

    //主板的基层Class 绑定一个processor 基层接口。
    protected LEUnitProcessor currentProcessor;
    //========================================================
    //LivingEntity external Adapter
    //========================================================
    System.Action<LE_Animation_Event> adapter_LE_AnimationEvent;
    System.Action<LE_UI_Event> adapter_LE_UIEvent;

    [SerializeField] private Transform adapter_LE_mainBody;
    private Vector3 adapter_LE_MoveVeclocity3D;
    private Vector2 adapter_LE_InputVH;
    //========================================================
    protected virtual void Start() { GetAndDisableAllProcessor(); InitalProcessor(); }

    protected abstract void GetAndDisableAllProcessor();

    protected abstract void InitalProcessor();
    
    protected abstract void ChangeProcessor();

    public abstract void MailBox_LE_CentralPanel(LEEvent e);
  
    //========================================================
    //主板外部接口
    //========================================================

    public void Bind_LE_UI_Event_MailBox(System.Action<LE_UI_Event> func)
    {
        adapter_LE_UIEvent -= func;
        adapter_LE_UIEvent += func;
    }

    public void Bind_LE_Animation_Event_MailBox(System.Action<LE_Animation_Event> func)
    {
        adapter_LE_AnimationEvent -= func;
        adapter_LE_AnimationEvent += func;
    }

    public Transform Adapter_LE_mainBody { get { return adapter_LE_mainBody; } set { adapter_LE_mainBody = value; } } //主躯干。 一般是物体模型所在的那个物体

    public Vector2 Adapter_LE_InputVH { get { return currentProcessor.InputVH; }}

    public Vector3 Adapter_MoveVeclocity3D { get { return adapter_LE_MoveVeclocity3D; } set { adapter_LE_MoveVeclocity3D = value; } }//单位在三位空间坐标内移动向量。一般有移动控制器计算出来后赋值到 LEUnitCP。

    public Transform Adapter_playerCamera;

    //========================================================
    //Processor 用于直调用
    //========================================================
    public void Rise_LE_UI_Event(LE_UI_Event e)
    {
        if (adapter_LE_UIEvent != null)
        {
            adapter_LE_UIEvent(e);
        }
        else {
            Debug.LogError(" There is no Subscriber bind to 'adapter_LE_UIEvent'. ");
            return;
        }
    }

    public void Rise_LE_Animation_Event(LE_Animation_Event e)
    {
        if (adapter_LE_UIEvent != null)
        {
            adapter_LE_AnimationEvent(e);
        }
        else
        {
            Debug.LogError(" There is no Subscriber bind to 'adapter_LE_AnimationEvent' ");
            return;
        }
    }
   
}

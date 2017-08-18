using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class LEUnitCentralPanel : MonoBehaviour{


    protected LEUnitProcessor currentProcessor;
    //========================================================
    //LivingEntity external Adapter
    //========================================================
    System.Action<LE_Animation_Event> adapter_LE_AnimationEvent;
    System.Action<LE_UI_Event> adapter_LE_UIEvent;
    System.Action<LE_Camera_Event> adapter_LE_CameraManager_Event;
    System.Action<LE_BasicMovement_Event> adapter_LE_BasicMovement_Event;

    [SerializeField] private Transform adapter_LE_mainBody;
    private Vector3 adapter_LE_MoveVeclocity3D;
    private Vector2 adapter_LE_InputVH;
    //========================================================
    protected virtual void Start() {
        GetAndDisableAllProcessor(); InitalProcessor();
        GameCentalPr.Instance.Adapter_Pause -= Rise_SYS_PauseEvent;
        GameCentalPr.Instance.Adapter_Pause += Rise_SYS_PauseEvent;
    }

    protected abstract void GetAndDisableAllProcessor();

    protected abstract void InitalProcessor();
    
    protected abstract void ChangeProcessor();

    public abstract void MailBox_LE_CentralPanel(LEEvent e);

    public void Intial_All_Component()
    {
       
    }
  
    //========================================================
    //Central Panel External Interface
    //========================================================

    public void Bind_LE_UI_Event_MailBox(System.Action<LE_UI_Event> func)
    {
        adapter_LE_UIEvent -= func;
        adapter_LE_UIEvent += func;
    }

    public void Bind_LE_BasicMovement_Event_MailBox(System.Action<LE_BasicMovement_Event> func)
    {
        adapter_LE_BasicMovement_Event -= func;
        adapter_LE_BasicMovement_Event += func;
    }

    public void Bind_LE_Animation_Event_MailBox(System.Action<LE_Animation_Event> func)
    {
        adapter_LE_AnimationEvent -= func;
        adapter_LE_AnimationEvent += func;
    }

    public void Bind_lE_CameraManager_Event_MailBox(System.Action<LE_Camera_Event> func)
    {
        adapter_LE_CameraManager_Event -= func;
        adapter_LE_CameraManager_Event += func;
    }

    public Transform Adapter_LE_mainBody { get { return adapter_LE_mainBody; } set { adapter_LE_mainBody = value; } } //主躯干。 一般是物体模型所在的那个物体

    public Vector2 Adapter_LE_InputVH { get { return currentProcessor.InputVH; }}

    public Vector3 Adapter_MoveVeclocity3D { get { return adapter_LE_MoveVeclocity3D; } set { adapter_LE_MoveVeclocity3D = value; } }//单位在三位空间坐标内移动向量。一般有移动控制器计算出来后赋值到 LEUnitCP。

    public Transform Adapter_playerCamera;

    //========================================================
    //Central Rise Event Interface
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
        if (adapter_LE_AnimationEvent != null)
        {
            adapter_LE_AnimationEvent(e);
        }
        else
        {
            Debug.LogError(" There is no Subscriber bind to 'adapter_LE_AnimationEvent' ");
            return;
        }
    }

    public void Rise_LE_CameraManager_Event(LE_Camera_Event e)
    {
        if (adapter_LE_CameraManager_Event != null)
        {
            adapter_LE_CameraManager_Event(e);
        }
        else
        {
            Debug.LogError(" There is no Subscriber bind to 'adapter_LE_CameraManager_Event' ");
            return;
        }
    }

    public void Rise_LE_BasicMovement_Event(LE_BasicMovement_Event e)
    {
        if (adapter_LE_BasicMovement_Event != null)
        {
            adapter_LE_BasicMovement_Event(e);
        }
        else
        {
            Debug.LogError("There is no Subscriber bind to the BasicMovement Event");
            return;
        }
    }

    public void Rise_SYS_PauseEvent(bool p)
    {
        currentProcessor.Pause(p);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Linq;

#region LivingEntity Event Define

public enum LEEventType
{
    GetDamage,
    Die,
    Sleep,
    StartDrive,
    StartFlyModel,
    StartShootGunModel,
    StartMeleeModel
}

public interface LEEvent
{ 
    LEEventType Type { get; set; }
    void Init();

}

public struct LEEvent_GetDamage : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.GetDamage; }
    public Damage damage;
}

public struct LEEvent_Die : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.Die; }
}

public struct LEEvent_Sleep : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.Sleep; }
}

public struct LEEvent_StartDrive : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.StartDrive; }
}

public struct LEEvent_StartFlyModel : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.StartFlyModel; }
}

public struct LEEvent_StartShootGunModel : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.StartShootGunModel; }
}

public struct LEEvent_StartMeleeModel : LEEvent
{
    LEEventType type;
    public LEEventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LEEventType.StartMeleeModel; }
}

#endregion

#region Damage Event Define

public enum DamageType
{
    Non,
    SlowDown,
    Bleeding,
    Stun
}
//Struct can not implement OOP, So I use Interface
public interface Damage
{
    DamageType Type { get; set; }
    void Init(); //因为Struct 不能使用 explicit parameterless constructors 所有我们必须手动初始化。
}

 public struct Damage_Non : Damage
{
    DamageType type;
    public DamageType Type { get { return type; } set { type = value; } }
    public void Init() { type = DamageType.Non; }

    public float num;
}

public struct Damage_SlowDown : Damage
{
    DamageType type;
    public DamageType Type { get { return type; } set { type = value; } }
    public void Init() { type = DamageType.SlowDown; }

    public float num;
    public float slowTime;
    public float slowPercentage;
}

public struct Damage_Stun : Damage
{
    DamageType type;
    public DamageType Type { get { return type; } set { type = value; } }
    public void Init() { type = DamageType.Stun; }

    public float num;
    public float stunTime;
}


#endregion

#region Animation Event Define

public enum LE_Animation_EventType
{
    moveInfo,
    shootInfo,
    Stun,
    SlowDown,
    Die,
    ReloadBullet
}

public interface LE_Animation_Event
{
    LE_Animation_EventType Type { get; set; }
    void Init(); //Because Struct We can't use explicit parameterless constructors
}

public struct LE_Animation_Event_moveInfo : LE_Animation_Event
{
    LE_Animation_EventType type;
    public LE_Animation_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Animation_EventType.moveInfo; }
    public float moveSpeed;
}

public struct LE_Animation_Event_shootInfo : LE_Animation_Event
{
    LE_Animation_EventType type;
    public LE_Animation_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Animation_EventType.shootInfo; }
    public bool isShoot;
}


public struct LE_Animation_Event_Stun : LE_Animation_Event
{
    LE_Animation_EventType type;
    public LE_Animation_EventType Type { get { return type; } set {type = value; } }
    public void Init() { type = LE_Animation_EventType.Stun; }

    public float stunTime;
    public int stunEffectIndex;
}

public struct LE_Animation_Event_SlowDown : LE_Animation_Event
{
    LE_Animation_EventType type;
    public LE_Animation_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Animation_EventType.SlowDown; }

    public float slowTime;
    public int slowValue;
}

public struct LE_Animation_Event_ReloadBullet : LE_Animation_Event
{
    LE_Animation_EventType type;
    public LE_Animation_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Animation_EventType.ReloadBullet; }

}

#endregion

#region UI Event Define

//LE UI Event
public enum LE_UI_EventType
{
    UpdateHealthBar,
    UseSkill,
    GetStun,
    WhatEver
}

public interface LE_UI_Event
{
    LE_UI_EventType Type { get; set; }
    void Init(); 
}

public struct LE_UI_Event_UpdateHealthBar : LE_UI_Event
{
    LE_UI_EventType type;
    public LE_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_UI_EventType.UpdateHealthBar; }

    public int LEId;
    public float currentHealth;
    public float maxHealth;
}

public struct LE_UI_Event_UseSkill : LE_UI_Event
{
    LE_UI_EventType type;
    public LE_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_UI_EventType.UseSkill; }

    public int LEId;
    public string skillName;
}

public struct LE_UI_Event_GetStun : LE_UI_Event
{
    LE_UI_EventType type;
    public LE_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_UI_EventType.GetStun; }

    public int LEId;
    public float stunTime;
}

//Systen UI Event
public enum SYS_UI_EventType
{
    StartGame,
    ExitGame,
    PrintSomeInfo,
    PressSettingButton,
}

public interface SYS_UI_Event
{
    SYS_UI_EventType Type { get; set; }
    void Init(); 
}

public struct SYS_UI_Event_PrintSomeInfo : SYS_UI_Event
{
    SYS_UI_EventType type;
    public SYS_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = SYS_UI_EventType.PrintSomeInfo; }

    public Vector3 mousePosition;
    public string whatYouWantToSay;
}

public struct SYS_UI_Event_StartGame : SYS_UI_Event
{
    SYS_UI_EventType type;
    public SYS_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = SYS_UI_EventType.StartGame; }
}

public struct SYS_UI_Event_PressSettingButton : SYS_UI_Event
{
    SYS_UI_EventType type;
    public SYS_UI_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = SYS_UI_EventType.PressSettingButton; }
}

#endregion

#region Camera_Event Define
public enum LE_Camera_EventType
{
    UpdateValue,
    ChangeCameraType
}

public interface LE_Camera_Event
{
    LE_Camera_EventType Type { get; set; }
    void Init(); //Because Struct We can't use explicit parameterless constructors
}

public struct LE_Camera_Event_UpdateVlaue : LE_Camera_Event
{
    LE_Camera_EventType type;
    public LE_Camera_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Camera_EventType.UpdateValue; }
    public float delta_yaw;
    public float delta_pitch;
    public float delta_dstToTarget;
}

public struct LE_Camera_Event_ChangeCamera : LE_Camera_Event
{
    LE_Camera_EventType type;
    public LE_Camera_EventType Type { get { return type; } set { type = value; } }
    public void Init() { type = LE_Camera_EventType.ChangeCameraType; }
    public int cameraType;
}

#endregion
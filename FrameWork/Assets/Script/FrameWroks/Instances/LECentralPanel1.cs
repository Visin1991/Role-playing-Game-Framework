﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central Panel 1
/// </summary>
public class LECentralPanel1 : LEUnitCentralPanel
{
    TPSProcessor fpsProcessor;

    protected override void Start () {
        base.Start();
    }

    [Obsolete]
    protected override void GetAndDisableAllProcessor()
    {/// get all Processor, then disable. Function will be Auto call through base.Start()
       // fpsProcessor = transform.GetOrAddComponent<TPSProcessor>();
       // fpsProcessor.enabled = false;
    }
    [Obsolete]
    protected override void InitalProcessor()
    {///right now we only have one TPSProcessor。。。。。Function will be Auto call through base.Start()
       // currentProcessor = fpsProcessor;
       // fpsProcessor.enabled = true;    
    }
    [Obsolete]
    protected override void ChangeProcessor()
    {
        //processor = fpsProcessor;
        //processor = carProcessor;
        //processor = flyProcessor;
        //...................
    }
    [Obsolete]
    public override void MailBox_LE_CentralPanel(LEEvent e)
    {
       // currentProcessor.MailBox_LE_ProcessEvent(e);
    }
}

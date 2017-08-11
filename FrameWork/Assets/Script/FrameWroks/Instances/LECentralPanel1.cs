using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central Panel 1
/// </summary>
public class LECentralPanel1 : LEUnitCentralPanel
{
    LEUnitProcessor fpsProcessor;

    protected override void Start () {
        base.Start();
    }

    protected override void GetAndDisableAllProcessor()
    {/// get all Processor, then disable. Function will be Auto call through base.Start()
        fpsProcessor = transform.GetComponent<LEUnitProcessor>();
        if(fpsProcessor != null)
            fpsProcessor.enabled = false;
    }

    protected override void InitalProcessor()
    {///right now we only have one TPSProcessor。。。。。Function will be Auto call through base.Start()
        currentProcessor = fpsProcessor;
        fpsProcessor.enabled = true;    
    }

    protected override void ChangeProcessor()
    {
        //processor = fpsProcessor;
        //processor = carProcessor;
        //processor = flyProcessor;
        //...................
    }

    public override void MailBox_LE_CentralPanel(LEEvent e)
    {
        currentProcessor.MailBox_LE_ProcessEvent(e);
    }
}

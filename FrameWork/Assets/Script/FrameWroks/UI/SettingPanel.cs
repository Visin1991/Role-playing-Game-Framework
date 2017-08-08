using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : UIPanel {
    public override GameCentalPr.PanelType GetPanelType()
    {
        return GameCentalPr.PanelType.Setting;
    }
}

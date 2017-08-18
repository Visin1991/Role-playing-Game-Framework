using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSavePanel : UIPanel {
    public override GameCentalPr.PanelType GetPanelType()
    {
        return GameCentalPr.PanelType.LoadSave;
    }
}

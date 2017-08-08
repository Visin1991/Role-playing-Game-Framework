using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This is the Setting Button in the Main Panel
public class SwitchPanelButton : MonoBehaviour {

    //GameCentalPr.PanelType parentPanelType;

    public GameCentalPr.PanelType target;
    
    Button switchPanelButton;

    private void Start()
    {
       /* UIPanel uiPanel = GetComponentInParent<UIPanel>();
        if (uiPanel == null) { Debug.LogError("Parent Don't have an UIPanel"); }
        else { parentPanelType = uiPanel.GetPanelType(); }*/

        switchPanelButton = GetComponent<Button>();
        if (switchPanelButton != null)
        {
            switchPanelButton.onClick.AddListener(delegate { OnClick(); });
        }
    }

    public void OnClick()
    {
        GameCentalPr.Instance.SwitchActivePanel(target);
        transform.parent.gameObject.SetActive(false);
    }
}

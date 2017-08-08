using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

    GameCentalPr.PanelType parentPanelType;
    public GameCentalPr.PanelType target;

    Button startNewGameButton;

    private void Start()
    {
        startNewGameButton = GetComponent<Button>();
        if (startNewGameButton != null)
        {
            startNewGameButton.onClick.AddListener(delegate { OnClick(); });
        }
    }

    public void OnClick()
    {
        GameCentalPr.Instance.StartNewGame();
    }
}


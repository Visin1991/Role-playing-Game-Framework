using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour {

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
        GameUIPr.Instance.StartNewGame();
    }
}


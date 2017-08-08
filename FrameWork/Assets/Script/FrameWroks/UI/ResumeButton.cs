using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButton : MonoBehaviour {

    Button resumeButton;

    private void Start()
    {
        resumeButton = GetComponent<Button>();
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(delegate { OnClick(); });
        }
    }

    public void OnClick()
    {
        GameCentalPr.Instance.PauseResumeEvent();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEditorHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[ContextMenu("Alling Anchor")]
	void AlingTheAnchor()
	{
		RectTransform rt = GetComponent<RectTransform>();
		Vinsin1_1.UIHelper.MatchCornersToAnchors(ref rt);
	}
}

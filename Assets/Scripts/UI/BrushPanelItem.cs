using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BrushPanelItem : MonoBehaviour {
    public BrushInfo Info;
	// Use this for initialization
	void Start () {
        Button b = GetComponent<Button>();
        b.onClick.AddListener(SetBrush);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBrush()
    {
        GetComponentInParent<BrushPanelSelector>().SetBrushInfo(Info);
    }
    
}

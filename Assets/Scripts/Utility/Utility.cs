using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility :MonoBehaviour{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void DeleteChildObjects(Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }

    }
}

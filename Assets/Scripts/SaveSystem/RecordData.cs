using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordData  {

    public StrokeData[] strokeDatas;

    public RecordData(Stroke[] strokes)
    {
        List<StrokeData> tempStrokeDatas = new List<StrokeData>();
        for(int i = 0; i < strokes.Length; i++)
        {
            tempStrokeDatas.Add(new StrokeData(strokes[i]));

        }
        strokeDatas = tempStrokeDatas.ToArray();
    }
}

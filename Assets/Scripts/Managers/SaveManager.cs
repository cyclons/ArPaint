using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour {

    public AllBrushInfo Brushes;

    List<Stroke> savedStrokes=new List<Stroke>();


    public void Save()
    {
        List<PaintAction> actions = GameManager.Instance.actionManager.ActionList;
        savedStrokes.Clear();
        for(int i = 0; i < GameManager.Instance.actionManager.ActionIndex+1; i++)
        {
            if(actions[i] is DrawAction && actions[i].StrokeObject.activeSelf)
            {
                savedStrokes.Add(actions[i].StrokeInfo);
            }
        }
        Debug.Log(savedStrokes.Count);
        SaveIntoFile();
    }

    void SaveIntoFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/work.joker";
        FileStream stream = new FileStream(path, FileMode.Create);

        RecordData record = new RecordData(savedStrokes.ToArray());

        formatter.Serialize(stream, record);
        stream.Close();
    }

    public void Load()
    {
        StrokeData[] strokeDatas = LoadStrokesFromFile();
        savedStrokes.Clear();
        for(int i = 0; i < strokeDatas.Length; i++)
        {
            savedStrokes.Add(new Stroke(strokeDatas[i]));
        }
        Utility.DeleteChildObjects(GameManager.Instance.drawManager.StrokesContainer);
        GameManager.Instance.actionManager.ClearActions();
        StartCoroutine(LoadCoroutine());
    }

    StrokeData[] LoadStrokesFromFile()
    {
        string path = Application.persistentDataPath + "/work.joker";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            RecordData data = formatter.Deserialize(stream) as RecordData;
            stream.Close();
            return data.strokeDatas;
        }
        else
        {
            Debug.Log("file not found");
            return null;
        }
    }

    IEnumerator LoadCoroutine()
    {
        for(int i = 0; i < savedStrokes.Count; i++)
        {
            BrushInfo info = GetBrushInfoByName(savedStrokes[i].BrushInfoName);
            for(int j = 0; j < savedStrokes[i].Points.Length; j++)
            {
                if (j == 0)
                {
                    info.StartDraw(GameManager.Instance.drawManager.StrokesContainer, info, savedStrokes[i].Width, savedStrokes[i].StrokeColor);
                }
                info.Drawing(savedStrokes[i].Points[j]);
                if(j== savedStrokes[i].Points.Length - 1)
                {
                    GameManager.Instance.actionManager.AddAction(info.GetAction());

                    info.EndDraw();
                }
                yield return new WaitForFixedUpdate();

            }
        }
    }

    public BrushInfo GetBrushInfoByName(string infoName)
    {
        for (int i = 0; i < Brushes.AllBrushes.Length; i++)
        {
            if (Brushes.AllBrushes[i].BrushName.Equals(infoName))
            {
                return Brushes.AllBrushes[i];
            }
        }
        Debug.Log(infoName + "not found");
        return null;
    }
}

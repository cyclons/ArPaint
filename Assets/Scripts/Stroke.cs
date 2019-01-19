using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stroke  {
    public float Width;
    public Color StrokeColor;
    public string BrushInfoName;
    public Vector3[] Points;
    public Stroke()
    {

    }
    public Stroke(StrokeData data)
    {
        Width = data.width;
        StrokeColor = new Color(data.color[0], data.color[1], data.color[2], data.color[3]);
        BrushInfoName = data.brushInfoName;
        List<Vector3> pointsList = new List<Vector3>();
        for(int i = 0; i < data.points.Length; i += 3)
        {
            pointsList.Add(new Vector3(data.points[i], data.points[i + 1], data.points[i + 2]));

        }
        Points = pointsList.ToArray();
    }
    public void InitStroke(float width,Color strokeColor,string brushInfoName,Vector3[] points)
    {
        Width = width;
        StrokeColor = strokeColor;
        BrushInfoName = brushInfoName;
        Points = points;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

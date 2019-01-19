using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StrokeData {

    public float width;
    public float[] color = new float[4];
    public string brushInfoName;
    public float[] points;

    public StrokeData(Stroke s)
    {
        Init(s.Width, s.StrokeColor, s.BrushInfoName, s.Points);
    }

    public StrokeData(float width, Color color, string brushInfoName, Vector3[] linePoints)
    {
        this.width = width;
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
        this.color[3] = color.a;

        this.brushInfoName = brushInfoName;

        this.points = new float[linePoints.Length];
        for(int i = 0; i < linePoints.Length; i++)
        {
            points[i*3] = linePoints[i].x;
            points[i*3+1] = linePoints[i].y;
            points[i*3+2] = linePoints[i].z;
        }
    }

    void Init(float width, Color color, string brushInfoName, Vector3[] linePoints)
    {
        this.width = width;
        this.color[0] = color.r;
        this.color[1] = color.g;
        this.color[2] = color.b;
        this.color[3] = color.a;

        this.brushInfoName = brushInfoName;

        this.points = new float[linePoints.Length*3];
        for (int i = 0; i < linePoints.Length; i++)
        {
            points[i * 3] = linePoints[i].x;
            points[i * 3 + 1] = linePoints[i].y;
            points[i * 3 + 2] = linePoints[i].z;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BrushInfo", menuName = "My/BrushInfo", order = 1)]
public class BrushInfo : ScriptableObject {

    public string BrushName;
    public Material brushMat;
    public BrushPaintType BrushType;
    public int randomTexNum = 1;

    private BrushStyle brushStyle;
    private Vector3 lastDrawPoint;
    private Color brushColor;
    private float brushWidth;
    public void StartDraw(Transform StrokesContainer,BrushInfo BrushInf,float BrushWidth,Color color)
    {
        brushColor = color;
        brushWidth = BrushWidth;
        brushStyle = GetBrushStyle();
        brushStyle.StartPaint( StrokesContainer,  BrushInf,  BrushWidth, color);
    }

    public void Drawing(Vector3 DrawPoint)
    {
        //meshLineRenderer.AddPoint(DrawPoint.position);
        brushStyle.Painting(DrawPoint);
    }

    public void EndDraw()
    {
        //tempBrush.GetComponent<MeshCollider>().sharedMesh = tempBrush.GetComponent<MeshFilter>().mesh;
        brushStyle.EndPaint();
    }

    public  BrushStyle GetBrushStyle()
    {
        BrushStyle style=new BrushStyle();
        switch (BrushType)
        {
            case BrushPaintType.NormalPaint:
                style = new NormalMeshBrushStyle();
                break;
            case BrushPaintType.ParticlePaint:
                break;
        }
        return style;
    }

    public PaintAction GetAction()
    {
        PaintAction action;
        Stroke s = new Stroke();
        s.InitStroke(brushWidth, brushColor, BrushName, brushStyle.meshLineRenderer.LinePoints.ToArray());
        action = new DrawAction();
        action.Init(s, brushStyle.tempBrush);
        return action;
    }
}

public enum BrushPaintType
{
    NormalPaint,
    ParticlePaint
}

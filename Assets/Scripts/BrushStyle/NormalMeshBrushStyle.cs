using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeshBrushStyle : BrushStyle {

    private BrushStyle brushStyle;
    private Vector3 lastDrawPoint;


    public override void StartPaint(Transform StrokesContainer, BrushInfo BrushInf, float BrushWidth, Color color)
    {
        tempBrush = new GameObject();
        tempBrush.transform.SetParent(StrokesContainer);
        MeshFilter meshFilter = tempBrush.AddComponent<MeshFilter>();
        tempBrush.AddComponent<MeshRenderer>();
        MeshCollider meshCollider = tempBrush.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
        meshLineRenderer = tempBrush.AddComponent<MeshLineRenderer>();
        meshLineRenderer.InitBrush(BrushInf, Camera.main.transform, BrushWidth, color);
    }

    public override void Painting(Vector3 DrawPoint)
    {
        base.Painting();
        meshLineRenderer.AddPoint(DrawPoint);

    }

    public override void EndPaint()
    {
        base.EndPaint();
        tempBrush.GetComponent<MeshCollider>().sharedMesh = tempBrush.GetComponent<MeshFilter>().mesh;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BrushStyle  {

    public MeshLineRenderer meshLineRenderer;
    public GameObject tempBrush;


    public virtual void StartPaint()
    {

    }
    public virtual void StartPaint(Transform StrokesContainer, BrushInfo BrushInf, float BrushWidth, Color color)
    {

    }

    public virtual void Painting()
    {

    }

    public virtual void Painting(Vector3 DrawPoint)
    {

    }

    public virtual void EndPaint()
    {

    }


}

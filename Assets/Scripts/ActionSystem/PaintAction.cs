using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintAction  {

    public Stroke StrokeInfo;
    public GameObject StrokeObject;


    public void Init(Stroke stroke,GameObject StrokeObj)
    {
        StrokeInfo = stroke;
        StrokeObject = StrokeObj;
    }

    public virtual void Undo()
    {

    }

    public virtual void Redo()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAction : PaintAction {

    public override void Redo()
    {
        base.Redo();
        StrokeObject.SetActive(true);
    }

    public override void Undo()
    {
        base.Undo();
        StrokeObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAction : PaintAction {

    public override void Redo()
    {
        base.Redo();
        StrokeObject.SetActive(false);
    }
    public override void Undo()
    {
        base.Undo();
        StrokeObject.SetActive(true);
    }
}

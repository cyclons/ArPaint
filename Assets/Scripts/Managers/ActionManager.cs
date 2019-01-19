using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {
    public int ActionIndex=-1;
    public List<PaintAction> ActionList=new List<PaintAction>();

    private int totalIndex=-1;
    public void AddAction(PaintAction action)
    {
        for (int i = totalIndex; i > ActionIndex; i--)
        {
            ActionList.RemoveAt(i);
        }
        ActionList.Add(action);

        ActionIndex++;
        totalIndex = ActionIndex;
    }

    public void UndoAction()
    {
        if (ActionIndex <= -1) return;
        ActionList[ActionIndex].Undo();
        ActionIndex--;
    }

    public void RedoAction()
    {
        if (totalIndex > ActionIndex)
        {
            ActionIndex++;
            ActionList[ActionIndex].Redo();
        }
    }

    public void ClearActions()
    {
        ActionList.Clear();
        ActionIndex = totalIndex = -1;
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour {
    public Transform StrokesContainer;
    public Transform DrawPoint;
    public BrushInfo BrushInf;
    public ColorPickerTriangle CP;
    [Range(0,0.05f)]
    public float BrushWidth = 0.025f;
    public bool DeleteMode = false;

    private Vector3 lastDrawPoint;
    private bool startDraw = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DrawPoint.GetComponent<Renderer>().material.color = CP.TheColor;

        if (!DeleteMode)
        {
            DrawPoint.gameObject.SetActive(true);
            if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject())

#elif IPHONE || UNITY_ANDROID
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#else
            if (EventSystem.current.IsPointerOverGameObject())
#endif
                {
                    return;
                }

                //更新DrawPoint
                UpdateDrawPoint();
                startDraw = true;

                BrushInf.StartDraw(StrokesContainer, BrushInf, BrushWidth, CP.TheColor);
            }
            else if (Input.GetMouseButton(0) && (DrawPoint.position - lastDrawPoint).sqrMagnitude > (BrushWidth * BrushWidth / 64))
            {

                if (startDraw)
                {
                    BrushInf.Drawing(DrawPoint.position);
                }
                lastDrawPoint = DrawPoint.position;
            }
            else if (Input.GetMouseButtonUp(0) && startDraw)
            {

                //将操作加入到操作列表
                AddActionToManager(BrushInf.GetAction());
                startDraw = false;
                DrawPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Camera.main.transform.forward * .3f;
                BrushInf.EndDraw();

            }
            if (startDraw)
            {
                UpdateDrawPoint();
            }
        }
        else
        {
            DrawPoint.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit))
                {
                    if (hit.collider.transform.parent == StrokesContainer)
                    {
                        hit.collider.gameObject.SetActive(false);
                        PaintAction action = new DeleteAction();
                        action.Init(null, hit.collider.gameObject);
                        AddActionToManager(action);
                    }
                }
            }
        }

	}

    public void SetBrushWidth(float w)
    {
        BrushWidth = w*0.1f*0.5f;
        DrawPoint.localScale = new Vector3(BrushWidth, BrushWidth, BrushWidth);
    }

    void UpdateDrawPoint()
    {
        //Debug.Log(Input.mousePosition);
        DrawPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) +
            Camera.main.transform.forward * .3f +
            Camera.main.transform.up * (Input.mousePosition.y - Screen.height / 2) / (Screen.height / 2) * .3f * .583f +
            Camera.main.transform.right * (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2) * .3f * .333f;

    }

    public void AddActionToManager(PaintAction action)
    {
        GameManager.Instance.actionManager.AddAction(action);
    }

    public void SetDelete(bool b)
    {
        DeleteMode = b;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CamMove : MonoBehaviour {
    float movX, movY;
    public float Speed = .5f;
    Vector2 lastMousePosition;

    // Use this for initialization
    void Start () {
		
	}
	
	void FixedUpdate () {
        movX = Input.GetAxisRaw("Horizontal");
        movY= Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate( new Vector3(0, Time.deltaTime, 0)*Speed);
        }else if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(new Vector3(0, -Time.deltaTime, 0) * Speed);
        }else if (movX!=0||movY!=0)
        {
            transform.Translate(new Vector3(movX, 0, movY)*Time.deltaTime * Speed);

        }


        if (Input.GetMouseButton(1))
        {
            transform.localRotation = Quaternion.Euler(
                transform.localRotation.eulerAngles.x- Input.mousePosition.y + lastMousePosition.y,
                transform.localRotation.eulerAngles.y + Input.mousePosition.x-lastMousePosition.x,
                0);
            //transform.localRotation *= Quaternion.Euler(,0 , 0);
            
        }
        lastMousePosition = Input.mousePosition;
    }
}

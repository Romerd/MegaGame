using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offSet;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;

    // Start is called before the first frame update
    void Start()
    {

        if (!useOffsetValues)
        {
            offSet = target.position - transform.position;//transform.position -locate the GameObject in 3D world space
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

    }

    // Update is called once per frame
    void Update()
    {
        //Get the x position of the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0,horizontal, 0);

        //Get the y position of the mouse & rotate the Pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);
        
        //Move the camera based on the current rotation of the target & the original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = target.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation  * offSet);
        //transform.position = target.position - offSet;
        transform.LookAt(target);
    }
}

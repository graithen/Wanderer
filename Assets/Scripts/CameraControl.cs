using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 1.5f;
    private const float Y_ANGLE_MAX = 50.0f;

    public GameObject Anchor; //where the camera is focussed
    public float AnchorOffset = 1;

    private float distance = 7.5f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensitivityX = 1.0f; //sensitivity mouse X
    private float sensitivityY = 1.0f; //sensitivity mouse Y
    private Vector3 anchorOffsetPos;

    float horizontalMouse;
    float verticalMouse;

    // Use this for initialization
    void Start()
    {
        //Cursor.visible = false; //turn off cursor in game
        //rig = GameObject.Find("CameraRig");

        anchorOffsetPos = new Vector3(Anchor.transform.position.x, Anchor.transform.position.y + AnchorOffset, Anchor.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        currentX += horizontalMouse * sensitivityX;
        currentY += -verticalMouse * sensitivityY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX); //Clamp the camera Y value between the min and max Y values

        GetMouseInput();
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 1, -distance); //controls where the camera is located
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = Anchor.transform.position + rotation * direction;
        gameObject.transform.LookAt(Anchor.transform.position); //where the camera is pointing towards (in this case the rig's transform)
    }

    void GetMouseInput()
    {
        horizontalMouse = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("HorizontalLook"); //Input.GetAxis("HorizontalLook");
        verticalMouse = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("VerticalLook"); //Input.GetAxis("VerticalLook");
        //Debug.Log(horizontalMouse);
        //Debug.Log(verticalMouse);
    }
}
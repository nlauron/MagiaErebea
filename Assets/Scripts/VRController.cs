using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/**
 * VRController
 * 
 * Controls the movement of the player. Moves using the the controllers
 * analog sticks and can snap the rotation of the camera using the grip
 * buttons.
 */
public class VRController : MonoBehaviour
{
    // Gravity, keep the player grounded
    public float m_Gravity = 100.0f;
    // public float m_Sensitivity = 10f;
    // Speed at which the player may move
    public float m_MaxSpeed = 10.0f;
    // Angle player is rotated when using snap rotation
    public float m_RotateIncrement = 30;

    // Rotation Snap
    public SteamVR_Action_Boolean m_RotatePress = null;

    public SteamVR_Action_Vector2 m_MoveValue = null;

    // References to camera and player
    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    // Initializes character controller
    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Initializes the camera
    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }


    private void Update()
    {
        HandleHeight();
        CalculateMovement();
        SnapRotation();
    }

    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        m_CharacterController.center = newCenter;
    }

    private void CalculateMovement()
    {
        Vector3 orientationEuler = new Vector3(0, m_Head.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        movement += orientation * new Vector3(m_MoveValue.axis.x, 0.0f, m_MoveValue.axis.y) * m_MaxSpeed;
        Debug.Log("x=" + m_MoveValue.axis.x + ", " + "y=" + m_MoveValue.axis.y);

        // Gravity
        movement.y -= m_Gravity;

        m_CharacterController.Move(movement * Time.deltaTime);
    }

    private void SnapRotation()
    {
        float snapValue = 0.0f;

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
            snapValue = -Mathf.Abs(m_RotateIncrement);

        if (m_RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
            snapValue = Mathf.Abs(m_RotateIncrement);

        transform.RotateAround(m_Head.position, Vector3.up, snapValue);
    }
}

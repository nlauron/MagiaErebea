using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimator : MonoBehaviour
{
    // Controller input
    public SteamVR_Action_Single m_PointAction = null;

    // Reference to Animator
    private Animator m_Animator = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        m_PointAction[m_Pose.inputSource].onChange += Grab;
    }

    private void OnDestroy()
    {
        m_PointAction[m_Pose.inputSource].onChange -= Grab;
    }

    private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
    {
        m_Animator.SetFloat("PointBlend", axis);
    }
}

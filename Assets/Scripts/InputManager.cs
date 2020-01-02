using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/**
 * Input manager for the controller's touchpad. Used for the radial menu on
 * the players hands to select and change their elements
 */
public class InputManager : MonoBehaviour
{
    // When touching the right touchpad
    public SteamVR_Action_Boolean touchR = null;
    // When touching the left touchpad
    public SteamVR_Action_Boolean touchL = null;
    // When the right touchpad is pressed
    public SteamVR_Action_Boolean pressR = null;
    // When the left touchpad is pressed
    public SteamVR_Action_Boolean pressL = null;
    // Where the right touchpad is touched
    public SteamVR_Action_Vector2 touchPositionR = null;
    // Where the left touchpad is touched
    public SteamVR_Action_Vector2 touchPositionL = null;

    // Radial Menu for spells
    public RadialMenu radialMenuR = null;
    public RadialMenu radialMenuL = null;

    private void Awake()
    {
        touchR.onChange += TouchR;
        pressR.onStateUp += PressReleaseR;
        touchPositionR.onAxis += PositionR;

        touchL.onChange += TouchL;
        pressL.onStateUp += PressReleaseL;
        touchPositionL.onAxis += PositionL;
    }

    private void OnDestroy()
    {
        touchR.onChange -= TouchR;
        pressR.onStateUp -= PressReleaseR;
        touchPositionR.onAxis -= PositionR;

        touchL.onChange -= TouchL;
        pressL.onStateUp -= PressReleaseL;
        touchPositionL.onAxis -= PositionL;
    }

    // Radial Menu touch position
    private void PositionR(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        radialMenuR.SetTouchPosition(axis);
    }

    // Radial Menu reveal on touch
    private void TouchR(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        radialMenuR.Show(newState);
    }

    // Radial Menu highlight section
    private void PressReleaseR(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        radialMenuR.ActivateHighlightedSection();
    }

    // Radial Menu touch position
    private void PositionL(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        radialMenuL.SetTouchPosition(axis);
    }

    // Radial Menu reveal on touch
    private void TouchL(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState)
    {
        radialMenuL.Show(newState);
    }

    // Radial Menu highlight section
    private void PressReleaseL(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        radialMenuL.ActivateHighlightedSection();
    }
}

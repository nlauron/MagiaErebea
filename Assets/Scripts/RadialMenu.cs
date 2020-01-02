using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Radial Menu
 * 
 * Used by the player to select and change elements. Accessable via the
 * controllers touchpads, opens on touch and is activated on press.
 */
public class RadialMenu : MonoBehaviour
{
    [Header("Scene")]
    // Element position in menu
    public Transform selectionTransform = null;
    // Cursor position
    public Transform cursorTransform = null;

    [Header("Events")]
    // Sections of the radial menu, 6 in total
    public RadialSection north = null;
    public RadialSection northEast = null;
    public RadialSection southEast = null;
    public RadialSection south = null;
    public RadialSection southWest = null;
    public RadialSection northWest = null;

    // Touch position on controller
    private Vector2 touchPosition = Vector2.zero;
    // List of sections
    private List<RadialSection> radialSections = null;
    // Current selected section
    private RadialSection highlightSecion = null;

    // Section placement around the menu
    private readonly float degreeIncrement = 60.0f;

    // Set up menu when game starts
    private void Awake()
    {
        CreateAndSetupSections();
    }

    // Sets up menu and its sections
    private void CreateAndSetupSections()
    {
        radialSections = new List<RadialSection>()
        {
            north,
            northEast,
            southEast,
            south,
            southWest,
            northWest
        };

        foreach (RadialSection section in radialSections)
            section.iconRenderer.sprite = section.icon;
    }

    // Hides menu until interacted with
    private void Start()
    {
        Show(false);
    }

    // Deactivates menu to hide it
    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }

    // Checks for touch position to set cursor on menu
    private void Update()
    {
        Vector2 direction = Vector2.zero + touchPosition;
        float rotation = GetDegree(direction);

        SetCursorPosition();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }

    // Calculate degrees for menu
    private float GetDegree(Vector2 direction)
    {
        float value = Mathf.Atan2(direction.x, direction.y);
        value *= Mathf.Rad2Deg;

        if (value < 0)
            value += 360.0f;

        return value;
    }

    // Sets cursor position according to touch position
    private void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition;
    }

    // Sets the selected section
    private void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation);
    }

    // Gets the nearest section
    private float SnapRotation(float rotation)
    {
        return GetNearestIncrement(rotation) * degreeIncrement;
    }

    // Gets the nearest section through rounding
    private int GetNearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation / degreeIncrement);
    }

    // Sets the selected section and highlights it
    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetNearestIncrement(currentRotation);

        if (index == 6)
            index = 0;

        highlightSecion = radialSections[index];
    }

    // Updates the touch position
    public void SetTouchPosition(Vector2 newValue)
    {
        touchPosition = newValue;
    }

    // Calls the function set to the selected section
    public void ActivateHighlightedSection()
    {
        highlightSecion.onPress.Invoke();
    }
}

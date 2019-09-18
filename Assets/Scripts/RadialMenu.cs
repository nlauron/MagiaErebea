using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{

    [Header("Scene")]
    public Transform selectionTransform = null;
    public Transform cursorTransform = null;

    [Header("Events")]
    public RadialSection north = null;
    public RadialSection northEast = null;
    public RadialSection southEast = null;
    public RadialSection south = null;
    public RadialSection southWest = null;
    public RadialSection northWest = null;

    private Vector2 touchPosition = Vector2.zero;
    private List<RadialSection> radialSections = null;
    private RadialSection highlightSecion = null;

    private readonly float degreeIncrement = 60.0f;

    private void Awake()
    {
        CreateAndSetupSections();
    }

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

    private void Start()
    {
        Show(false);
    }

    public void Show(bool value)
    {
        gameObject.SetActive(value);
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero + touchPosition;
        float rotation = GetDegree(direction);

        SetCursorPosition();
        SetSelectionRotation(rotation);
        SetSelectedEvent(rotation);
    }

    private float GetDegree(Vector2 direction)
    {
        float value = Mathf.Atan2(direction.x, direction.y);
        value *= Mathf.Rad2Deg;

        if (value < 0)
            value += 360.0f;

        return value;
    }

    private void SetCursorPosition()
    {
        cursorTransform.localPosition = touchPosition;
    }

    private void SetSelectionRotation(float newRotation)
    {
        float snappedRotation = SnapRotation(newRotation);
        selectionTransform.localEulerAngles = new Vector3(0, 0, -snappedRotation);
    }

    private float SnapRotation(float rotation)
    {
        return GetNearestIncrement(rotation) * degreeIncrement;
    }

    private int GetNearestIncrement(float rotation)
    {
        return Mathf.RoundToInt(rotation / degreeIncrement);
    }

    private void SetSelectedEvent(float currentRotation)
    {
        int index = GetNearestIncrement(currentRotation);

        if (index == 6)
            index = 0;

        highlightSecion = radialSections[index];
    }

    public void SetTouchPosition(Vector2 newValue)
    {
        touchPosition = newValue;
    }

    public void ActivateHighlightedSection()
    {
        highlightSecion.onPress.Invoke();
    }
}

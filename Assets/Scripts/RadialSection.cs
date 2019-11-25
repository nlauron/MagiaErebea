using System;
using UnityEngine;
using UnityEngine.Events;

/**
 * RadialSection
 * 
 * A section on the radial menu. Consists of its icon and its function
 * when pressed.
 */
[Serializable]
public class RadialSection
{
    public Sprite icon = null;
    public SpriteRenderer iconRenderer = null;
    public UnityEvent onPress = new UnityEvent();
}

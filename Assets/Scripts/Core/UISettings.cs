using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UISettings
{
    [Tooltip("Character indication text color.")]
    public Color characterTextColor = Color.grey;
    [Tooltip("Basic text color.")]
    public Color basicTextColor = Color.white;
    [Tooltip("Choice text color.")]
    public Color choiceTextColor = Color.green;
    [Tooltip("Binary river scrolling speed")]
    public float scrollingSpeed = 0;
    public float clockRotationSpeed = 0;
    public float armSpeed = 0;
    public float desiredIntroTimer = 0;
    public float volumeRadio = 0;

}

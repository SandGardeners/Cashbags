using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhuestEvent
{
    CHECK_IN,
    PHONECALL,
    MAIL,
    CHECK_OUT,
    SPECIAL,
    NONE
}

[CreateAssetMenu(fileName = "Ghuest", menuName = "Ghuest", order = 1)]
public class Ghuest : ScriptableObject
{
    public string name;
    public string mainKnot;
    public List<AudioClip> voice;
    public Sprite headSprite;
    [HideInInspector]
    public int indexCheckin;

    [HideInInspector]
    public List<GhuestEvent> events;

    public Ghuest(string n, string knot)
    {
        name = n;
        mainKnot = knot;
        events = NarrativeUI.ParseAvailableEventsForGhuest(knot);
        
    }
}
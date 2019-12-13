﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DEBUG_TABS
{
    PERFORMANCES,
    STORY,
    SCENE
}

public class MultiDebug : MonoBehaviour
{
    [SerializeField]
    private bool displayDebugInfo = true;
    bool uiActive = true;
    
    
    static void EasyCreate()
    {
        if(Debug.isDebugBuild)
        {
            MultiDebug md = FindObjectOfType<MultiDebug>();
            if(md == null)
            {
                GameObject go = new GameObject("[MultiDebug]", typeof(MultiDebug));
                DontDestroyOnLoad(go);
            }
        }
    }
    
    void OnGUI()
    {
        if (displayDebugInfo = GUI.Toggle(new Rect(10, 10, 200, 30), displayDebugInfo, "Display debug info"))
        {
            GUI.Label(new Rect(20, 50, 100, 20), "Niceness: " + ((int)NarrativeUI.ui._inkStory.variablesState["NICENESS"]).ToString());
            if(GUI.Button(new Rect(150, 50, 150, 20), "NEXT GHUEST"))
            {
                GhuestManager.gm.timer = GhuestManager.gm.triggerTimer;
            }
            if (GUI.Button(new Rect(300, 50, 150, 20), "NEXT PHONECALL"))
            {
                Desk.d.phone.timer = Desk.d.phone.triggerTimer;//.gm.triggerTimer;
            }
            if(GUI.Button(new Rect(450, 50, 150, 20), "NEXT MESSAGE"))
            {
                BinaryRiver.br.timer = BinaryRiver.br.triggerTimer;
            }
   

            //   bool uiActive = GUI.Toggle(new Rect(10, 70, 200, 30), NarrativeUI.ui.uiActive, "Display dialogue");
            //    NarrativeUI.ui.SwitchActive(uiActive);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) displayDebugInfo = !displayDebugInfo;
    }

    void LateUpdate()
    {
       
    }
}

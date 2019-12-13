using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ClickableChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Text text = null; //  or make public and drag

    private string refText;
    public int index;


    void Start()
    {
        Text text = GetComponent<Text>();
        text.color = NarrativeUI.ui.settings.basicTextColor;
        GetComponent<Button>().transition = Selectable.Transition.None;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (NarrativeUI.ui.waitingForInput)
        {
            if (text == null)
                text = GetComponent<Text>();
            text.color = NarrativeUI.ui.settings.choiceTextColor;
            refText = text.text;
          //  text.text = "> " + refText;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (NarrativeUI.ui.waitingForInput)
        {
            text.color = NarrativeUI.ui.settings.basicTextColor;
         //   text.text = refText;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (NarrativeUI.ui.waitingForInput)
        {
           if(text != null)
                text.color = Color.grey;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (NarrativeUI.ui.waitingForInput)
        {
            SendMessageUpwards("ChoiceInput", index);
        }
    }
}

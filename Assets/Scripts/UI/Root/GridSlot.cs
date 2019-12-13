using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class GridSlot : MonoBehaviour, IDropHandler,IPointerEnterHandler
{
    Grid grid;
    [SerializeField]public  string localisation = null;
    void Start()
    {
        grid = transform.parent.GetComponent<Grid>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Item2d dragItem = eventData.pointerDrag.GetComponent<Item2d>();
        if (dragItem != null)
        {
        /*    int maskValue = dragItem.item.toMask();
            if (!((maskValue & grid.autorisedItemType) > 0))
            {
                Debug.Log("The Item is not on the good Grid");
                return;
            }
            else if (localisation != string.Empty && dragItem.item is Equipement)
            {
                if (((Equipement)dragItem.item).localisation != localisation)
                {
                    Debug.Log("The Item should be on " + ((Equipement)dragItem.item).localisation + " not :" + localisation);
                    return;
                }
            }*/

            if (transform.childCount == 0)
            {
                Debug.Log(dragItem.name + "was put in " + this.transform.parent.name);
                dragItem.transform.SetParent(this.transform);
                //if (localisation != string.Empty)
                //    dragItem.item.applyStats();
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
             /*       Item2d slotItem = transform.GetChild(0).GetComponent<Item2d>();
                    if (slotItem != null && dragItem.item.ID == slotItem.item.ID)
                    {
                        if (slotItem.nb + dragItem.nb <= dragItem.item.maxStack)
                        {
                            slotItem.nb += dragItem.nb;
                            slotItem.nbtext.GetComponent<Text>().text = slotItem.nb.ToString();
                            Destroy(dragItem.nbtext);
                            Destroy(dragItem.gameObject);
                            break;
                        }
                    }
               */ }
            }
        }
    }
            //else if (transform.childCount == 1)
            //{
            //    Debug.Log(dragItem.name + " and " + transform.GetChild(0).name + "switch places");
            //    transform.GetChild(0).SetParent(dragItem.lastSlot.transform);
            //    dragItem.transform.SetParent(this.transform);
            //}

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawers : MonoBehaviour
{

    public GameObject keyObject;
    public GameObject moneyObject;
    public Bounds boundsDrawers;

    GameObject currentObject = null;

    RectTransform currentDrawer = null;
    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowItem(string n)
    {
        for(int i = 2; i < 4; i++)
        {
            foreach(Transform t in transform.GetChild(i))
            {
                if(t.gameObject.name == n)
                {
                    t.gameObject.SetActive(true);
                }
            }
        }
    }

    public void Activate(int index)
    {
        if (index == 1 || index == 2 || index == 3 || index == 4)
        {
            AudioManager.am.PlayRandom(AudioManager.am.drawerOpen);
            if (currentDrawer != null)
                currentDrawer.gameObject.SetActive(false);
            currentDrawer = transform.GetChild(index - 1).GetComponent<RectTransform>();
            currentDrawer.gameObject.SetActive(true);
        }
        else if (index == 0)
        {
            if (currentDrawer != null)
            {
                AudioManager.am.PlayRandom(AudioManager.am.drawerClose);
                currentDrawer.gameObject.SetActive(false);
                currentDrawer = null;
                gameObject.SetActive(false);
            }
        }
    }

    public void AddCollectibleItem(GameObject reference)
    {
        reference.transform.SetParent(currentDrawer.transform, true);
        Destroy(reference.GetComponent<Item2d>());
        reference.AddComponent<DraggableObject>();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentDrawer != null)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                if (!boundsDrawers.Contains(Input.mousePosition-transform.position))
                {
                    Activate(0);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + boundsDrawers.center, boundsDrawers.size);
    }
}

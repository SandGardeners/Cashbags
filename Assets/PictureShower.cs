using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureShower : MonoBehaviour {

    bool isActive = false;

    private Image picture;

    static private PictureShower _ps;

    public GameObject target;
    static public PictureShower ps
    {
        get
        {
            return _ps;
        }
    }

    public void Close()
    {
        picture.sprite = null;
        target = null;
        gameObject.SetActive(false);
    }

    void Awake()
    {
        _ps = this;
    }

    public void Display(GameObject go)
    {
        target = go;
        picture.sprite = go.GetComponent<DraggableObject>().bigPicture;
        picture.SetNativeSize();
        gameObject.SetActive(true);
    }

    void Start()
    {
        picture = transform.GetChild(1).GetComponent<Image>();
        picture.SetNativeSize();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(2))
            Close();
    }
}

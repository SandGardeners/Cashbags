using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;


[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(GridLayoutGroup))]
public class Grid : MonoBehaviour {

   public GameObject tileModel;
   public  Vector2 gridSize;
   public  Vector2 tileSize;
   public int autorisedItemType;
    
}





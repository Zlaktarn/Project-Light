 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 
 public class GameObjectSearcher : MonoBehaviour
 {
    [HideInInspector]
     public string searchTag;
 
     void Start()
     {
         if (searchTag != null)
         {
             FindObjectwithTag(searchTag);
         }
     }
 
     public GameObject FindObjectwithTag(string tag)
     {
         Transform parent = transform;
         return GetChildObject(parent, tag);
     }
 
     public GameObject GetChildObject(Transform parent, string tag)
     {
         for (int i = 0; i < parent.childCount; i++)
         {
             Transform child = parent.GetChild(i);
             if (child.tag == tag)
             {
                return child.gameObject;
             }
             if (child.childCount > 0)
             {
                 GetChildObject(child, tag);
             }
         }

         return null;
     }
 }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBoard : MonoBehaviour
{
   public GameObject personUI;
   public bool isOpen=false;

   public void Open()
   {
       isOpen=true;
       personUI.SetActive(isOpen);
   }

   public void Close()
   {
       isOpen=false;
       personUI.SetActive(false);
   }
}

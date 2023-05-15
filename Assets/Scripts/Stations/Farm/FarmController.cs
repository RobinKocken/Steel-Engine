using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : MonoBehaviour
{
    public void OpenFarmUI()
    {
        //open a menu where you can choose crops to grow

        StartCoroutine(GrowCrop());
    }

   public IEnumerator GrowCrop()
    {
        return;
    }

}

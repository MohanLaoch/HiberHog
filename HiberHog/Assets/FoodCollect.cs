using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollect : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") && this.gameObject.layer == 9)
        {
            Debug.Log("works");
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    public Transform playertransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("webackbaby");
            playertransform.transform.position = new Vector3(127f, 120f, -59.78f);
        }
    }
}

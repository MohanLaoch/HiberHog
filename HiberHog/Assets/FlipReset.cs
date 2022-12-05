using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipReset : MonoBehaviour
{
    public Transform PlayerTransform;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            PlayerTransform.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}

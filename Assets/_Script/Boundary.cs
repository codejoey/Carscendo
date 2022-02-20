using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CARCUBE")
        {
            other.gameObject.GetComponentInParent<AudioSource>().volume = 0.2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "CARCUBE")
        {
            other.gameObject.GetComponentInParent<AudioSource>().volume = 1f;
        }
    }
}

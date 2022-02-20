using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThing : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.name == "OVRControllerPrefab")
        //{
            // do something
            Debug.Log("Button Pressed");
        //}

        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Button Released");
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}

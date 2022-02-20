using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThing : MonoBehaviour
{
    public CarManager state;
    public bool isConfirm = false;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.name == "OVRControllerPrefab")
        //{
            // do something
            Debug.Log("Button Pressed");
        //}
        if (isConfirm)
        {
            state.confirm();
        }
        else state.next();
        GetComponent<MeshRenderer>().material.color = Color.blue;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Button Released");
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}

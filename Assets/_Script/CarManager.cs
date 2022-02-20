using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public Display display;

    public ButtonThing b1, b2;
    //b1 left, b2, right, b3 confirm

    // car stuff
    public GameObject[] Carbody;
    public GameObject[] CarFrame;
    
    public Material Primary_EDM;
    public Material Secondary_EDM;

    public Material Primary_Jazz;
    public Material Secondary_Jazz;


    bool isConfirmed = false;
    public void confirm()
    {
        if (isConfirmed)
        {
            foreach (GameObject mesh in Carbody)
            {
                mesh.GetComponent<MeshRenderer>().material = Primary_EDM; 
            }
            foreach (GameObject mesh in CarFrame)
            {
                mesh.GetComponent<MeshRenderer>().material = Secondary_EDM;
            }
            isConfirmed = false;

        }
        else
        {
            foreach (GameObject mesh in Carbody)
            {
                mesh.GetComponent<MeshRenderer>().material = Primary_Jazz;
            }
            foreach (GameObject mesh in CarFrame)
            {
                mesh.GetComponent<MeshRenderer>().material = Secondary_Jazz;
            }
            isConfirmed = true;

        }
    }

    public void next()
    {
        if (display.currSlide+1 < 5)//hardcoded...
        {
            display.setSlide(display.currSlide+1);
        }
        else
        {
            display.setSlide(2);
        }
    }

}

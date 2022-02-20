using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStats : MonoBehaviour
{
    [Header("Realtime Stats")]
    public Vector3 velocity;
    public float speed = 0;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(0);
    }

    private void Update()
    {
        velocity = GetComponent<Rigidbody>().velocity; // to get a Vector3 representation of the velocity
        speed = velocity.magnitude; // to get magnitude
        //rotationDegree = GetComponent<Rigidbody>().rotation.eulerAngles.

        // pitch = speed
    }
}

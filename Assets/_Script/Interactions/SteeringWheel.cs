using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    //Right Hand 
    public GameObject rightHand;
    private Transform rightHandOriginalParent;
    private bool rightHandOnWheel = false;

    public Transform directionalObject;

    //Left Hand
    public GameObject leftHand;
    private Transform leftHandOriginalParent;
    private bool leftHandOnWheel = false;

    public Transform[] snapPositions;

    private int numberOfHandsOnWheel = 0;

    //wheels or objects to control with wheel 
    public GameObject Vehicle;
    private Rigidbody VehicleRigidBody;

    public float currentWheelRotation = 0.0f;

    //turn dampening, lower number makes vehicle take longer time to reach turn rotation 
    // for vehicle to just copy steering wheel movement 1 to 1 use high number like 9999.
    public float turnDampening = 250;

    // Start is called before the first frame update
    void Start()
    {
        //VehicleRigidBody = Vehicle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseHandsFromWheel();

        ConvertHandRotationToSteeringWheelRotation();

        ////TurnVehicle();

        currentWheelRotation = -transform.rotation.eulerAngles.z;
    }

    private void ReleaseHandsFromWheel()
    {
        if (rightHandOnWheel == true /*&& somewhay to see if it's being gripped*/)
        {
            rightHand.transform.parent = rightHandOriginalParent;
            rightHand.transform.position = rightHandOriginalParent.position;
            rightHandOnWheel = false;
            numberOfHandsOnWheel--;
        }
        if (leftHandOnWheel == true /*&& somewhay to see if it's being gripped*/)
        {
            leftHand.transform.parent = leftHandOriginalParent;
            leftHand.transform.position = leftHandOriginalParent.position;
            leftHandOnWheel = false;
            numberOfHandsOnWheel--;
        }

        if (leftHandOnWheel == false && rightHandOnWheel == false)
        {
            //reset steering wheel to not be parent of directional object if wheel is released.
            //TODO: lerp wheel back to center. 
            transform.parent = null;
        }
    }

    private void ConvertHandRotationToSteeringWheelRotation()
    {
        if (rightHandOnWheel == true && leftHand == false)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else if (rightHandOnWheel == false && leftHandOnWheel == true)
        {
            Quaternion newRot = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        }
        else if (rightHandOnWheel == true && leftHandOnWheel == true)
        {
            Quaternion newRotLeft = Quaternion.Euler(0, 0, leftHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion newRotRight = Quaternion.Euler(0, 0, rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(newRotLeft, newRotRight, 1.0f / 2.0f);
            directionalObject.rotation = finalRot;
            transform.parent = directionalObject;

        }
    }
    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel)
    {
        var shortestDistance = Vector3.Distance(snapPositions[0].position, hand.transform.position);
        var bestSnapp = snapPositions[0];

        foreach (var snapPosition in snapPositions)
        {
            if (snapPosition.childCount == 0)
            {
                var distance = Vector3.Distance(snapPosition.position, hand.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    bestSnapp = snapPosition;

                }
            }
        }
        // we need XHandOriginalParent to be able to reset hand after release
        originalParent = hand.transform.parent;

        //set best snap as parent and hand position to snap position
        hand.transform.parent = bestSnapp.transform;
        hand.transform.position = bestSnapp.transform.position;

        handOnWheel = true;
        numberOfHandsOnWheel++;
    }

    private void OnTriggerStay(Collider other)
    {
        CapsuleCollider overlapped = other.GetComponent<CapsuleCollider>();
        if (overlapped)
        {
            HandAnim hand = other.GetComponentInParent<HandAnim>();
            if (hand)
            {
                if (hand.isLeft == false)
                {
                    //Debug.Log("right hand");
                    //place right hand
                    if (rightHandOnWheel == false )//*&& someway to validated grip is pressed rn*//*)
                    {  
                        PlaceHandOnWheel(ref rightHand, ref rightHandOriginalParent, ref rightHandOnWheel);
                    }

                }
                else
                {
                    //Debug.Log("left hand");
                    //place left hand
                    if (leftHandOnWheel == false) //*&& someway to validated grip is pressed rn*//*)
                    {
                        PlaceHandOnWheel(ref leftHand, ref leftHandOriginalParent, ref leftHandOnWheel);
                    }

                }
            }
        }
        
    }
}

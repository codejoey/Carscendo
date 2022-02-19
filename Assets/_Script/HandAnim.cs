using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.
using System.Linq;

public class HandAnim : MonoBehaviour
{
    private PlayerControls controller;
    public Animator m_animator = null;
    public bool isLeft = true;

    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;
    private Collider[] m_colliders = null;

    public float anim_frames = 4f;
    private float grip_state = 0f;
    private float trigger_state = 0f;
    private float triggerCap_state = 0f;
    private float thumbCap_state = 0f;

    private void Awake()
    {
        controller = new PlayerControls();
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLeft)
        {
            //Grip, Oculus LGrip Trigger or keyboard W
            controller.VR.Grip.performed += Grip_performed;
            controller.VR.Grip.canceled += Grip_performed;

            //Gas, Oculus LTrigger or keyboward S
            controller.VR.Gas.performed += Gas_performed;
            controller.VR.Gas.canceled += Gas_performed;
        }
        




        m_colliders = this.GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        for (int i = 0; i < m_colliders.Length; ++i)
        {
            Collider collider = m_colliders[i];
            // collider.transform.localScale = new Vector3(COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN, COLLIDER_SCALE_MIN);
            collider.enabled = true;
        }
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
    }

    private void Gas_performed(InputAction.CallbackContext obj)
    {
        float triggerTarget = obj.ReadValue<float>();
        float trigger_state_delta = triggerTarget - trigger_state;
        if (trigger_state_delta > 0f)
        {
            trigger_state = Mathf.Clamp(trigger_state + 1 / anim_frames, 0f, triggerTarget);
        }
        else if (trigger_state_delta < 0f)
        {
            trigger_state = Mathf.Clamp(trigger_state - 1 / anim_frames, triggerTarget, 1f);
        }
        else { trigger_state = triggerTarget; }

        m_animator.SetFloat("Pinch", trigger_state);
    }

    private void Grip_performed(InputAction.CallbackContext obj)
    {
        float gripTarget = obj.ReadValue<float>();
        // solutionText.SetText(gripValue.ToString());
        float grip_state_delta = gripTarget - grip_state;
        if (grip_state_delta > 0f)
        {
            grip_state = Mathf.Clamp(grip_state + 1 / anim_frames, 0f, gripTarget);
        }
        else if (grip_state_delta < 0f)
        {
            grip_state = Mathf.Clamp(grip_state - 1 / anim_frames, gripTarget, 1f);
        }
        else { grip_state = gripTarget; }

        m_animator.SetFloat(m_animParamIndexFlex, grip_state);
    }


    // Update is called once per frame
    void Update()
    {
    
    }
}
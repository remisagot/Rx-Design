using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class scaleChange : MonoBehaviour
{
    public float scaleFactor;

    private bool handGrabLeft;
    private bool handGrabRight;

    public float distHand;

    public float distMin;

    public float sensibility;

    private float distHandPreview;

    public GameObject leftHand;

    public GameObject rightHand;

    private Vector3 objectOriginalScale;

    public SteamVR_Action_Boolean TriggerLeftOnOff;
    // a reference to the hand
    public SteamVR_Input_Sources handTypeLeft;

    public SteamVR_Action_Boolean TriggerRightOnOff;
    // a reference to the hand
    public SteamVR_Input_Sources handTypeRight;


    // Start is called before the first frame update
    void Start()
    {
        handGrabRight = false;
        handGrabLeft = false;

        objectOriginalScale = transform.localScale;


        TriggerLeftOnOff.AddOnStateDownListener(TriggerDownLeft, handTypeLeft);
        TriggerLeftOnOff.AddOnStateUpListener(TriggerUpLeft, handTypeLeft);

        TriggerRightOnOff.AddOnStateDownListener(TriggerDownRight, handTypeRight);
        TriggerRightOnOff.AddOnStateUpListener(TriggerUpRight, handTypeRight);

       
    }

    // Update is called once per frame
    void Update()
    {
        distHand = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
        
        if(handGrabRight && handGrabLeft && distHand > distMin)
        {
            if (distHand > distHandPreview + sensibility)
            {
                transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }
            if (distHand < distHandPreview - sensibility && transform.localScale.x > objectOriginalScale.x/2)
            {
                transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
            }
        }
        distHandPreview = distHand;
    }

    public void TriggerUpLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("trigger is up Left");
        handGrabLeft = false;
    }

    public void TriggerDownLeft(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down Left");
        handGrabLeft = true;
    }

    public void TriggerUpRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("trigger is up Right");
        handGrabRight = false;
    }

    public void TriggerDownRight(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down Right");
        handGrabRight = true;
    }

    
}

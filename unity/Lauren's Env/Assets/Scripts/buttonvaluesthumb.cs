using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{
    public class buttonvaluesthumb : MonoBehaviour
    {   
        // initializes the vive controller touchpad and the trigger as inputs
        NVRHand hand;
        NVRButtonInputs touchpadInput;
        NVRButtonInputs trig;

        //creates a game object for the thumb joint
        public GameObject didget2;
        public float orig = 156.902f;
        // Use this for initialization
        void Start()
        {
            hand = GetComponent<NVRHand>();
            didget2 = GameObject.Find("hands:b_l_thumb1");

        }

        // Update is called once per frame
        void Update()
        {
            var joystickThumb = Input.GetAxis("Horizontal");
            float roundedThumb = (float)System.Math.Round(joystickThumb, 2);
            touchpadInput = hand.Inputs[NVRButtons.Touchpad];
            print("current value: " + roundedThumb);
            if (roundedThumb <= -0.1f || roundedThumb >= 0.1f)
            {
                print("hi " + roundedThumb);
                //prints out the x and y position of the trackpad
                //print("y- axis is" + touchpadInput.Axis.y);
                // print("x- axis is" + touchpadInput.Axis.x);
                var rot = didget2.transform.localRotation.eulerAngles;
                rot.Set(didget2.transform.localEulerAngles.x, orig - (roundedThumb * 360.0f), didget2.transform.localEulerAngles.z);
                didget2.transform.localEulerAngles = rot;
            }
            /*
            else if (roundedThumb >= -0.1f || roundedThumb <= 0.1f)
            {
                print("bye ");
                //prints out the x and y position of the trackpad
                //print("y- axis is" + touchpadInput.Axis.y);
                //print("x- axis is" + touchpadInput.Axis.x);
                var rot = didget2.transform.localRotation.eulerAngles;
                rot.Set(didget2.transform.localEulerAngles.x, orig, didget2.transform.localEulerAngles.z);
                didget2.transform.localEulerAngles = rot;
            }*/

            /* old values from when the vive controller was used instead of a game controller
            if (touchpadInput.IsTouched)
            {

                //prints out the x and y position of the trackpad
                //print("y- axis is" + touchpadInput.Axis.y);
               // print("x- axis is" + touchpadInput.Axis.x);
               // print("orig" + orig);
                var rot = didget2.transform.localRotation.eulerAngles;
                rot.Set(didget2.transform.localEulerAngles.x, orig - (touchpadInput.Axis.x * 360.0f), didget2.transform.localEulerAngles.z);
                didget2.transform.localEulerAngles = rot;
            }

            else if (touchpadInput.IsTouched== false)
            {

                //prints out the x and y position of the trackpad
                //print("y- axis is" + touchpadInput.Axis.y);
                //print("x- axis is" + touchpadInput.Axis.x);
                //print("orig" + orig);
                var rot = didget2.transform.localRotation.eulerAngles;
                rot.Set(didget2.transform.localEulerAngles.x, orig, didget2.transform.localEulerAngles.z);
                didget2.transform.localEulerAngles = rot;
            }
            */
        }
    }
}

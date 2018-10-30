using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{
    public class buttonvalueIndex : MonoBehaviour
    {
        // initializes the vive controller touchpad and the trigger as inputs
        NVRHand hand;
        NVRButtonInputs touchpadInput;
        NVRButtonInputs trig;

        //creates a game object for the thumb joint
        public GameObject didget;
        public float orig = 61.113f;
        // Use this for initialization
        void Start()
        {
            //initializes the Newton vr hand (must have the NVR had script active) and obtains all of the transform info from the hand model
            hand = GetComponent<NVRHand>();
            didget = GameObject.Find("hands:b_l_index1");
        }

        // Update is called once per frame
        void Update()
        {
            //gets the value of the joystick's position once per frame
            var joystickIndex = Input.GetAxis("Vertical");
            float roundedIndex = (float)System.Math.Round(joystickIndex, 2);

            print("index "+joystickIndex);
            /* old values from when the vive controllers were used
             * if (touchpadInput.IsTouched)
             {
                    touchpadInput = hand.Inputs[NVRButtons.Touchpad];
                    trig = hand.Inputs[NVRButtons.Trigger];
                 //prints out the x and y position of the trackpad
                 print("y- axis is" + touchpadInput.Axis.y);


                 print("x- axis is" + touchpadInput.Axis.x);
             }*/

            // this part allows independant control of the index fingers knuckle joint  based on how far the joystick is pushed
            if (roundedIndex <= -0.1f || roundedIndex >= 0.1f)
            {   //prints out how far the trigger is depressed
                //print("hi" + (trig.Axis.x - 0.2f));
                var rot = didget.transform.localRotation.eulerAngles;


                rot.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig - ((roundedIndex - 0.2f) * 360.0f));
                didget.transform.localEulerAngles = rot;
            }
            // snaps the finger back to its original position when the joystick is released
            else if (roundedIndex >= -0.1f || roundedIndex <= 0.1f)
            {
                var rot2 = didget.transform.localRotation.eulerAngles;


                rot2.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig);
                didget.transform.localEulerAngles = rot2;


            }

        }
    }
}

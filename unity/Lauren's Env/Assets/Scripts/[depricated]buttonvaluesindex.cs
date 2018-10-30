using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{
    public class buttonvalues : MonoBehaviour
    {

        NVRHand hand;
        NVRButtonInputs touchpadInput;
        NVRButtonInputs trig;
        public GameObject didget;
        public float orig = 61.113f;
        // Use this for initialization
        void Start()
        {
            hand = GetComponent<NVRHand>();
            didget = GameObject.Find("hands:b_l_index1");
               
        }
        
        // Update is called once per frame
        void Update()
        {
            touchpadInput = hand.Inputs[NVRButtons.Touchpad];
            trig = hand.Inputs[NVRButtons.Trigger];
            //var orig = didget.transform.localRotation.eulerAngles.z;
            //print("stu"+didget.transform.localRotation);
            //print("rot" + didget.transform.localRotation.eulerAngles);
            if (touchpadInput.IsTouched)
            {   

                //prints out the x and y position of the trackpad
                print("y- axis is" + touchpadInput.Axis.y);
                
                
                print("x- axis is" + touchpadInput.Axis.x );
            }
            
            // this part allows independant control of the index fingers knuckle joint  based on how far the trigger is depressed
            if (trig.IsTouched)
            {   //prints out how far the trigger is depressed
                print("hi" + (trig.Axis.x - 0.2f));
                print(trig.Axis.x * 360.0f);
                //print(didget.transform.localRotation.x);
                //print(didget.transform.localRotation.y);
                //p/rint(didget.transform.localRotation.z);
                //print(didget.transform.localRotation);
                //print("b4" + didget.transform.localRotation);
                //print(didget.transform.eulerAngles);
                var rot = didget.transform.localRotation.eulerAngles;
                
                
                rot.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig-((trig.Axis.x-0.2f) * 360.0f));
                didget.transform.localEulerAngles = rot;
                print(didget.transform.localEulerAngles);
                print("orig"+orig);
                //new Quaternion( didget.transform.localRotation.x, didget.transform.localRotation.y, trig.Axis.x, didget.transform.localRotation.w);
                //didget.transform.localRotation.z = trig.Axis.x;
                //print("after"+didget.transform.localRotation);
            }
            // snaps the finger back to its original position when the trigger is released
            else if (trig.IsTouched == false)
            {
                var rot2 = didget.transform.localRotation.eulerAngles;


                rot2.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig);
                didget.transform.localEulerAngles = rot2;
                

            }

        }
    }
}

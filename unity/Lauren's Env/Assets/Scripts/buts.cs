using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewtonVR
{
    public class buts : MonoBehaviour
    {
        /*
         * 
         *= object has been completed but not all sub objectives are done 
         $= objective is finished and all sub objectives are done too 
         @= abandoned objective

        To do list: 
        1.) convert the inputs for the trackpad/ trigger into degrees that can be used as transforms for the collision detector
            - angles that are between -5 and 92 degrees look the most realistic
        
        2.) attach the collision detector boxes to the base of the finger joints 

        *3.) have all of the collision detector boxes daisy chained together
            -Mybe change the colliders to two colliders? so that its easier to isolate the fingers
        
        4.) write a script that will change the angle of the hand graphic to the equivalent angle that
        is being sent from the vive controller
            - since the graphic is tied to the collider box then the collider box should simultaniousley move
        
        5.) Start working with the arduino zero to start sending data from a sensor through the tracker to unity
        
        6.) learn how to use the 3d printer
        
        7.) learn how to use 3d modeling software 
        
        8.) draw up a sketch of the device i want to build 
        
        9.) brainstorm a few Zero one breaking systems 
            - tooth lock, like on zipties or bikes 
                - there is a catch that locks it
            - just putting a wedge in the gears 
            - maybe drum break?
        
        10.) pick motors 
        
        11.) prototype break systems with electrical circuit
        
        12.) draw schematic  of motor circuit 
        
        13.) look into batteries that will be able to power both the arduino and the motors 
            
        14.) make pcb
            - Make sure that the grounds are isolated 
            - get a current regulator 
            - if using through hole pins make sure that the hole sizes are big enough for the pins 
        15.) make housing for the battery and arduino 
        
        16.) final assemble 


        stretch goals: 
        1.) instead of using the tracker use our own tracking solution that involves a raspberry pie or another microcontroller
        plus all of the sensors that I have gotten from Nick. 
        
        2.) extend to multiple fingers 
        
        3.) make it able to grasp soft objects 

        */

        /* NVRHand hand;
         NVRButtonInputs touchpadInput;
         NVRButtonInputs trig;*/
        //public GameObject didget;

        public GameObject didget;
        // Use this for initialization
        void Start()
        {
            // hand = GetComponent<NVRHand>();
            didget = GameObject.Find("hands:b_l_index1");
        }

        // Update is called once per frame
        void Update()
        {
           /* touchpadInput = hand.Inputs[NVRButtons.Touchpad];
            trig = hand.Inputs[NVRButtons.Trigger];*/
            //print("rot"+didget.transform.localRotation.eulerAngles);
           /* if (touchpadInput.IsTouched)
            {
                //prints out the x and y position of the trackpad
                print("y- axis is" +touchpadInput.Axis.y);
                print("x- axis is" + touchpadInput.Axis.x);
                (285.1,106.6,61.1)
            }
            if (trig.IsTouched)
            {   //prints out how far the trigger is depressed
                print("hi" + trig.Axis.x);
            }*/
        }
    }
}

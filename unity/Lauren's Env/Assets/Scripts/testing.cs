using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{
    public class testing : MonoBehaviour
    {
        NVRHand hand;
        NVRButtonInputs touchpadInput;
        NVRButtonInputs trig;


        // Use this for initialization
        void Start()
        {
            hand = GetComponent<NVRHand>();
        }

        // Update is called once per frame
        void Update()
        {
            touchpadInput = hand.Inputs[NVRButtons.Touchpad];
            trig = hand.Inputs[NVRButtons.Trigger];
            //print("trig" + trig.Axis.x);

            

        }
    }
}
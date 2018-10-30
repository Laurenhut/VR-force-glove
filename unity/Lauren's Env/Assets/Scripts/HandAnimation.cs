using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewtonVR
{
    public class HandAnimation : MonoBehaviour
    {
        Controller controller;
        private Animator _anim;

        private NVRInputDevice InputDevice;
        public NVRButtons HoldButton = NVRButtons.Grip;
        public bool HoldButtonDown { get { return Inputs[HoldButton].PressDown; } }
        //private NVRInputDevice InputDevice;
        public Dictionary<NVRButtons, NVRButtonInputs> Inputs;
        private void Start()
        {
            Inputs = new Dictionary<NVRButtons, NVRButtonInputs>(new NVRButtonsComparer());
            for (int buttonIndex = 0; buttonIndex < NVRButtonsHelper.Array.Length; buttonIndex++)
            {
                if (Inputs.ContainsKey(NVRButtonsHelper.Array[buttonIndex]) == false)
                {
                    Inputs.Add(NVRButtonsHelper.Array[buttonIndex], new NVRButtonInputs());
                }
            }
       
}
        // Use this for initialization
        /* void Start()
         {
             _anim = GetComponentInChildren<Animator>();
             controller = GetComponent<Controller>();
         }

         // Update is called once per frame
         void Update()
         {
             if (controller.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
             {
                 //Plays grab animation
                 if (!_anim.GetBool("IsGrabbing"))
                 {
                     _anim.SetBool("IsGrabbing", true);
                 }



             }
             else
             {
                 if (controller.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
                 {
                     if (_anim.GetBool("IsGrabbing"))
                     {
                         _anim.SetBool("IsGrabbing", false);
                     }
                 }
             }

         }*/
    }
}
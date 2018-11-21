using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        //sets the values for the capslock key and keyevents
        private const byte VK_CAPSLOCK = 0x14;
        private const uint KEYEVENTF_EXTENDEDKEY = 1;
        private const int KEYEVENTF_KEYUP = 0x2;
        private const int KEYEVENTF_KEYDOWN = 0x0;

        // gets the current state of the capslock key and returns it as a boolean
        public static bool GetCapsLock()
        {
            return (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        }

        // sets the current state of the capslock key
        public static void SetCapsLock(bool bState)
        {
            if (GetCapsLock() != bState)
            {
                keybd_event(VK_CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN, 0);
                keybd_event(VK_CAPSLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            }
        }
        // Use this for initialization
        void Start()
        {
            //initializes the Newton vr hand (must have the NVR had script active) and obtains all of the transform info from the hand model
            hand = GetComponent<NVRHand>();
            didget = GameObject.Find("hands:b_l_index1");
        }
        public float roundedIndex_prev;
        // Update is called once per frame
        void Update()
        {
            //gets the value of the joystick's position once per frame
            var joystickIndex = Input.GetAxis("Vertical");
            float roundedIndex = (float)System.Math.Round(joystickIndex, 2);

            if (GetCapsLock())
            {
                if (roundedIndex_prev < roundedIndex )
                {
                    print("y- axis is" + roundedIndex);
                    // this part allows independant control of the index fingers knuckle joint  based on how far the joystick is pushed
                    if (roundedIndex <= -0.01f || roundedIndex >= 0.01f)
                    {   //prints out how far the trigger is depressed
                        //print("hi" + (trig.Axis.x - 0.2f));
                        var rot = didget.transform.localRotation.eulerAngles;

                        //print("rot " + rot);
                        rot.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig - ((roundedIndex - 0.2f) * 360.0f));
                        didget.transform.localEulerAngles = rot;
                    }
                }
                else {
                    var rot = didget.transform.localRotation.eulerAngles;
                    rot.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig - ((roundedIndex_prev - 0.2f) * 360.0f));
                    didget.transform.localEulerAngles = rot;
                }
            }
            else {

                print("y- axis is" + roundedIndex);
                // this part allows independant control of the index fingers knuckle joint  based on how far the joystick is pushed
                if (roundedIndex <= -0.01f || roundedIndex >= 0.01f)
                {   //prints out how far the trigger is depressed
                    //print("hi" + (trig.Axis.x - 0.2f));
                    var rot = didget.transform.localRotation.eulerAngles;

                    //print("rot " + rot);
                    rot.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig - ((roundedIndex - 0.2f) * 360.0f));
                    didget.transform.localEulerAngles = rot;
                }
                /*** snaps the finger back to its original position when the joystick is released
                else if (roundedIndex >= -0.01f || roundedIndex <= 0.01f)
                {
                    var rot2 = didget.transform.localRotation.eulerAngles;


                    rot2.Set(didget.transform.localEulerAngles.x, didget.transform.localEulerAngles.y, orig);
                    didget.transform.localEulerAngles = rot2;

    
                }*/
            }
            roundedIndex_prev = roundedIndex;
        }
    }
}

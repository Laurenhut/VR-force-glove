using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class pickup : MonoBehaviour {

    public bool[] index;
    public bool[] thumb;
    GameObject heldObject;
    GameObject stickto;
    GameObject stuck;
    Rigidbody simulator;

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
    void Start()
    {
        //makes sure capslock is off at the start of the program
        if (GetCapsLock()) {
            pickup.SetCapsLock(false);
        }
    }

    void Update()
    {   

        thumb=new bool[10] ;
        index = new bool[10];


        //looks at the boolean of the thumb colider upcoltip upcolback
        thumb[0] = GameObject.Find("thumb tip1").GetComponent<handcollide_thumb>().upcol;
        thumb[1] = GameObject.Find("thumb tip2").GetComponent<handcollide_thumb>().upcol;
        thumb[2] = GameObject.Find("thumb tip3").GetComponent<handcollide_thumb>().upcol;

        thumb[3] = GameObject.Find("thumb back1").GetComponent<handcollide_thumb>().upcol;
        thumb[4] = GameObject.Find("thumb back2").GetComponent<handcollide_thumb>().upcol;
        thumb[5] = GameObject.Find("thumb back3").GetComponent<handcollide_thumb>().upcol;

        thumb[6] = GameObject.Find("thumb tip1 (1)").GetComponent<handcollide_thumb>().upcol;
        thumb[7] = GameObject.Find("thumb tip1 (2)").GetComponent<handcollide_thumb>().upcol;
        thumb[8] = GameObject.Find("thumb tip1 (3)").GetComponent<handcollide_thumb>().upcol;
        thumb[9] = GameObject.Find("thumb tip1 (4)").GetComponent<handcollide_thumb>().upcol;
        // looks at the boolean from index collider

        index[0] = GameObject.Find("just the tip1").GetComponent<Handcolide>().downcol;
        index[1] = GameObject.Find("just the tip2").GetComponent<Handcolide>().downcol;
        index[2] = GameObject.Find("just the tip3").GetComponent<Handcolide>().downcol;

        index[3] = GameObject.Find("mid1").GetComponent<Handcolide>().downcol;
        index[4] = GameObject.Find("mid2").GetComponent<Handcolide>().downcol;
        index[5] = GameObject.Find("mid3").GetComponent<Handcolide>().downcol;

        index[6] = GameObject.Find("back1").GetComponent<Handcolide>().downcol;
        index[7] = GameObject.Find("back2").GetComponent<Handcolide>().downcol;
        index[8] = GameObject.Find("back3").GetComponent<Handcolide>().downcol;

        index[9] = GameObject.Find("knuckle").GetComponent<Handcolide>().downcol;
       

        foreach (bool indhit in index) {
                if (indhit == true )
                {print("hit index");

                foreach (bool thumbhit in thumb){
                        if (thumbhit==true)
                        {
                            print("hit thumb");
                            Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);

                            stickto = GameObject.Find("placeholder");
                            // finds the object that has been colided with
                            //stuck = GameObject.Find(hit.collider.gameObject.name);

                            // checks to see if each of those objects are already a held object

                            // looks through all of the coliders in the script
                            foreach (Collider col in cols)
                            {
                                if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null)

                                {
                                    print("object grasped");
                                    // assignes the desired one to be a gameobject   
                                    heldObject = col.gameObject;
                                    // assignes the object to be a child of my placeholder aka a child of the hand collider
                                    heldObject.transform.parent = stickto.transform;
                                    // gets rid of gravity etc on the object
                                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                                    // sets capslock to be true
                                    // the state of capslock will determine if the device is locked or not
                                    pickup.SetCapsLock(true);
                                    break;
                                }

                            }// end foreach

                            break;
                        } // end if
            
                        else if (thumbhit ==false)
                        {
                            if (heldObject != null)
                            {                             
                                heldObject.transform.parent = null;
                                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                                heldObject = null;
                                print("no thumb hit");
                            }
                        }// end else if
                    }
                    break;
                }

        }
 
        int total_index_false = 0;
        int total_thumb_false = 0;
        // checks to see how many of the index colliders are false
        for (int x = 0; x < index.Length; x++) {
            if (index[x] == false)
            {
                total_index_false++;
            }
        }

        // checks how many of the thumb colliders are false 
        for (int x = 0; x < thumb.Length; x++)
        {
            if (thumb[x] == false)
            {
                total_thumb_false++;
            }
        }

        if (total_index_false == index.Length || total_thumb_false == thumb.Length) {
            if (heldObject != null)
            {
                // turns capslock off 
                if (GetCapsLock())
                {
                    pickup.SetCapsLock(false);
                }
                // sets the parent of the object to be none and turns on gravity etc
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject = null;
                print("no index hit");
            }
        }



        // checking if my current transform is within the bounds of the box
        // if (Vector3.Distance(transform.position, foundedge) < .5f)
        //{
        //  print("hi "+"pos "+ transform.position+"found "+ foundedge);

        //}
        // else if (Vector3.Distance(transform.position, foundedge) > .5f )
        // {
        //     print("bye");
        // }

    }

}

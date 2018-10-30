using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour {
    public bool downcoltip;
    public bool downcolmid;
    public bool downcolback;
    public bool downcolknuckle;

    //public bool upcoltip;
    //public bool upcolback;
    public bool[] index;
    public bool[] thumb;
    GameObject heldObject;
    GameObject stickto;
    GameObject stuck;
    Rigidbody simulator;

    void Update()
    {
        RaycastHit hit;
        float distance;
        thumb=new bool[6] ;
        index = new bool[10];


        //looks at the boolean of the thumb colider upcoltip upcolback
        thumb[0] = GameObject.Find("thumb tip1").GetComponent<handcollide_thumb>().upcol;
        thumb[1] = GameObject.Find("thumb tip2").GetComponent<handcollide_thumb>().upcol;
        thumb[2] = GameObject.Find("thumb tip3").GetComponent<handcollide_thumb>().upcol;

        thumb[3] = GameObject.Find("thumb back1").GetComponent<handcollide_thumb>().upcol;
        thumb[4] = GameObject.Find("thumb back2").GetComponent<handcollide_thumb>().upcol;
        thumb[5] = GameObject.Find("thumb back3").GetComponent<handcollide_thumb>().upcol;

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
       

        //print("stuff " + "pos " + transform.position + "found " + foundedge);
        // saves the transform at which i ran into the colider for the box
        /* if ((upcoltip == true || upcolback == true))
         {
             print("stuf");
         }*/

        foreach (bool indhit in index) { 
        
                if (indhit == true )
                {

                    foreach (bool thumbhit in thumb){
                        if (thumbhit==true)
                        {
                                print("hit");
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
                                    print("stuck");
                                    // assignes the desired one to be a gameobject   
                                    heldObject = col.gameObject;
                                    // assignes the object to be a child of my placeholder aka a child of the hand collider
                                    heldObject.transform.parent = stickto.transform;
                                    // gets rid of gravity etc on the object
                                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                                    break;
                                }

                            }// end foreach


                            //print("i am at" + transform.position);
                            //print("index");
                            //print( hit.collider.gameObject.name);
                            //print("found endge at a distance of : "+ distance);
                            break;
                        } // end if
            

                        else if (thumbhit ==false)
                        {
                            if (heldObject != null)
                            {
                                heldObject.transform.parent = null;
                                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                                heldObject = null;
                                print("hi");
                            }
                        }// end else if
                    
                    }

                    break;
                }

            else if (indhit == false )
            {
                if (heldObject != null)
                {
                    heldObject.transform.parent = null;
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    heldObject = null;
                    print("hi");
                }
                // assignes the object to be a child of my placeholder aka a child of the hand collider
                //heldObject.transform.parent = stuck.transform;
                // gets rid of gravity etc on the object
                //heldObject.GetComponent<Rigidbody>().isKinematic = false;

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

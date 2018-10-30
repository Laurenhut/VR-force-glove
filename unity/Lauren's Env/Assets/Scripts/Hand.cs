using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// makes sure you have a controller object 
[RequireComponent(typeof(Controller))]
public class Hand : MonoBehaviour {
    GameObject heldObject;
    Controller controller;

    Rigidbody simulator;
   
    // Use this for initialization
    void Start () {
     
        simulator = new GameObject().AddComponent<Rigidbody>();
        simulator.name = "simulator";
        simulator.transform.parent = transform.parent;
        controller = GetComponent<Controller>();
    }
	
	// Update is called once per frame
	void Update () {

        // check if there is a held object already 
        // if not check if the trigger is pressed
        if (heldObject)
        {
            simulator.velocity = (transform.position - simulator.position) * 50f;
            if (controller.controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
            
                //DROPS AN OBJECT IF THE putton is let go
                //sets held object variable so that it can be grabbed again
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Rigidbody>().velocity = simulator.velocity;
                heldObject.GetComponent<HeldObject>().parent = null;
                heldObject = null;


            }

        }

        else
        {   //checks if triger is pressed
          
            if (controller.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                

                // creates an array of colliders of all the things in a small radious of the controls
                // all objects in a o.1 radious of object
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.1f);
                // checks to see if each of those objects are already a held object
                foreach (Collider col in cols)
                {
                    if (heldObject == null && col.GetComponent<HeldObject>() && col.GetComponent<HeldObject>().parent == null)
                    {
                        heldObject = col.gameObject;
                        heldObject.transform.parent = transform;
                        heldObject.transform.localPosition = Vector3.zero;
                        heldObject.transform.localRotation = Quaternion.identity;
                        //keeps the object from being affected by gravity if helf on to
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        //labels the controller as the thing holding the object
                        heldObject.GetComponent<HeldObject>().parent = controller;
                       


                    }
                }
            }
        }

    }




 
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class HeldObject : MonoBehaviour {
    [HideInInspector]
    public Controller parent;

    void Update()
    {
       
        //print("Position of " +this.gameObject.name +"is" + transform.position + "Rotation "+transform.eulerAngles);
     

    }
}

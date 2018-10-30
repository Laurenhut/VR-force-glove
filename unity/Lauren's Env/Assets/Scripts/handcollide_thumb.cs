using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handcollide_thumb : MonoBehaviour {
    public bool upcol;
    
    
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float distance;
        Vector3 up = transform.TransformDirection(Vector3.right) * .012f;
        Debug.DrawRay(transform.position, up, Color.red);
        upcol = Physics.Raycast(transform.position, (up), out hit, .012f);

        if (upcol == true)
        {
            distance = hit.distance;

            print("thumb");
            print(hit.collider.gameObject.name);
            //print("found endge at a distance of : "+ distance);


        }

    }
}

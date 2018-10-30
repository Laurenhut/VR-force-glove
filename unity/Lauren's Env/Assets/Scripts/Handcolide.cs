using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handcolide : MonoBehaviour {

    
    public bool downcol;

   
    void Update()
    {
        RaycastHit hit;
        float distance;
        //
        //frontcol = Physics.Raycast(transform.position, Vector3.forward, 1.5f);
        Vector3 forward = transform.TransformDirection(Vector3.down) * .01f;
        Debug.DrawRay(transform.position, forward, Color.green);
        downcol = Physics.Raycast(transform.position,(forward), out hit, .01f);
        

        if (downcol == true )
        {
            distance = hit.distance;
            print("index");
            print(hit.collider.gameObject.name);

        }
   

    }
   
}

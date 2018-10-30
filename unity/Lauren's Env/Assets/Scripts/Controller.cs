using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    // Use this for initialization
    public SteamVR_Controller.Device controller
    {
        // Returns all of the tracked data of the controllers 
        get
        {
            return SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index);
        }
    }
}

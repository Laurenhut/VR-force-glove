using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class object_force : MonoBehaviour
{
    public Transform forceBar;
    public Slider forceFill;


    public float maxForce = 1.0f;
    public float currentForce = 0.0f;
    public float forceBarofset = 2;

    // change the behavior to check if the other knob is being turned so i can change value of forcebar

    // Update is called once per frame
    void Update()
    {
        var joystickForce = Input.GetAxis("Horizontal");
        float roundedForce = (float)System.Math.Round(joystickForce, 2);
        PositionForceBar();
        changeForce(roundedForce);
    }

    public void changeForce(float newForceJostick)
    {
        if (currentForce != newForceJostick)
        {
            currentForce = newForceJostick;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);
            forceFill.value = currentForce;
        }

    }
    private void PositionForceBar()
    {
        Vector3 currentPosition = transform.position;
        forceBar.position = new Vector3(currentPosition.x, currentPosition.y + forceBarofset, currentPosition.z);
        forceBar.LookAt(Camera.main.transform);

    }


}

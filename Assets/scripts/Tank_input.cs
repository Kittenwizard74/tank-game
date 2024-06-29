using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tank_input : MonoBehaviour
{
    #region gets
    private float forwardinput;
    public float Forwardinput
    {
        get { return forwardinput; }
    }

    private float rotationinput;
    public float Rotationinput
    {
        get { return rotationinput; }
    }
    #endregion

    private void Update()
    {
        HandleInputs();
    }

    protected virtual void HandleInputs()
    {
        forwardinput = Input.GetAxis("Vertical");
        rotationinput = Input.GetAxis("Horizontal");
    }
}

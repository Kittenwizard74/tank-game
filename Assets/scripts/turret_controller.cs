using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class turret_controller : MonoBehaviour
{
    public Transform thirdPersonCamera;

    void FixedUpdate()
    {
        HandleTurret();
    }

    protected virtual void HandleTurret()
    {        
        Vector3 lookDirection = thirdPersonCamera.forward;
        lookDirection.y = 0;

        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = rotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Bullet_controller : MonoBehaviour
{
    #region variables
    private float lifetime;
    public float maxlifetime;
    #endregion
    #region base methods
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collider>() != null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
         lifetime += Time.deltaTime;

        if (lifetime > maxlifetime)
        {
            Destroy(gameObject);
        }
    }
    #endregion
}

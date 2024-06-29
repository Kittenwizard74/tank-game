using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Tank_input))]
public class Tank_controller : MonoBehaviour
{
    #region variables
    private Rigidbody rb;
    private Tank_input input;

    [Header("movement")]
    public float TankSpeed = 20f;
    public float TankRotationSpeed = 20f;

    [Header("shooting")]
    public GameObject shell;
    public Transform pewpewPoint;
    public float force;
    public float upforce;
    public float fireRate;
    private float nextShot;

    [Header("health")]
    public int MaxHealth;
    private int health;

    #endregion
    #region  base methods
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<Tank_input>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if(rb && input)
        {
            HandleMovement();
            HandleShooting();
        }
        if (health >= MaxHealth)
        {
            SceneManager.LoadScene(0);
        }

        //time for next shot in seconds
        nextShot += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AIBullet"))
        {
            health++;
        }
    }
    #endregion
    #region custom methods
    protected virtual void HandleMovement()
    {
        //movement
        Vector3 wantedPosition = transform.position + (transform.forward * input.Forwardinput * TankSpeed * Time.deltaTime);
        rb.MovePosition(wantedPosition);

        //rotation
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (TankRotationSpeed * input.Rotationinput * Time.deltaTime));
        rb.MoveRotation(wantedRotation);
    }

    protected virtual void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && fireRate < nextShot)
        {
            //shoot bullet
            var bullet = Instantiate(shell,pewpewPoint.position, shell.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(pewpewPoint.transform.up * -force ,ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(pewpewPoint.transform.forward * upforce, ForceMode.Impulse);

            //reset bullet coolddown
            nextShot = 0f;
        }
    }
    #endregion
}

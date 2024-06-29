using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent (typeof(Rigidbody))]
public class Enemy_controller : MonoBehaviour
{
    private Rigidbody rb; // PHYSICS BABYYYYYYYYYYYY
    public Transform Player; // where player

    [Header("shooting")]
    public GameObject AIbullet; // what bullet
    public Transform shootingPoint; // from where bullet
    public float bulletForce; // force of bullet forward
    public float upbulletForce; // force of bullet upward
    public float shootingRange; // allowed range to shoot
    public float AIFireRate; // shots per time
    private float AINextShot; // when next shot
    private bool allowShoot; // allows AI to shoot

    [Header("movement")]
    public float movementSpeed; // speed of AI
    private bool allowMove;

    [Header("HP")]
    private int HP; // current HP
    public int MaxHP; // max amount of HP
    private bool AIalive; // if its allowed to continue to exist

    #region base methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        HP = MaxHP; // Initialize HP to MaxHP or a starting value

        AIalive = true;
        allowMove = true;
        allowShoot = false;
    }

    private void Update()
    {
        HandleAIHP();

        // Distance between AI and player
        float distance = Vector3.Distance(transform.position, Player.position);

        // Enable or disable shooting/moving based on distance
        if (distance <= shootingRange)
        {
            allowShoot = true;
            allowMove = false;
        }
        else
        {
            allowShoot = false;
            allowMove = true;
        }

        if (AIalive)
        {
            if (allowMove)
            {
                HandleAIMovement();
            }

            if (allowShoot)
            {
                HandleAIShooting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Compare tag to see if it should damage, then damages
        if (other.gameObject.CompareTag("Bullet"))
        {
            HP--; // Typically HP should decrease when taking damage
        }
    }
    #endregion

    #region custom methods
    protected virtual void HandleAIShooting()
    {
        if (Player != null && Time.time > AINextShot)
        {
            // Creates variable to store info, instantiates bullet, then adds force
            var aibullet = Instantiate(AIbullet, shootingPoint.position, AIbullet.transform.rotation);
            aibullet.GetComponent<Rigidbody>().AddForce(shootingPoint.transform.forward * bulletForce, ForceMode.Impulse); // Forward force
            aibullet.GetComponent<Rigidbody>().AddForce(shootingPoint.transform.up * upbulletForce, ForceMode.Impulse); // Upward force

            // Time between shots
            AINextShot = Time.time + AIFireRate;
        }
    }

    protected virtual void HandleAIMovement()
    {
        // Move toward player
        Vector3 direction = (Player.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;
    }

    protected virtual void HandleAIHP()
    {
        if (HP <= 0) // Check if HP is below or equal to 0
        {
            AIalive = false;
            Destroy(gameObject);
        }
    }
    #endregion
}

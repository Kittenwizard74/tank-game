using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITurret_controller : MonoBehaviour
{
    #region variables
    private Transform playerTransform;
    #endregion

    #region base methods
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        HandleAITurret();
    }
    #endregion

    #region custom methdos
    protected virtual void HandleAITurret()
    {
        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
            }
        }
    }
    #endregion
}

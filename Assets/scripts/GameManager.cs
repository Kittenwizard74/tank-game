using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> objectsToTrack; 

    #region base methods
    private void Start()
    {
        // list for enemys
        objectsToTrack = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        // verefies if all destroyed
        if (HandleEnemyAmount())
        {            
            HandleNextScene();
        }
    }
    #endregion
    #region custom methods
    bool HandleEnemyAmount()
    {
        // sees if all game objects are destroyd
        foreach (GameObject obj in objectsToTrack)
        {
            if (obj != null)
            {
                return false; // at least one left
            }
        }
        return true; // none left
    }

    void HandleNextScene()
    {
        SceneManager.LoadScene(1);
    }
    #endregion
}

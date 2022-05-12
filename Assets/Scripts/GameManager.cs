using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();

                }
            }
            return instance;

        }

    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //also do pool manager and prefab manager.
}

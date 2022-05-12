using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region SINGLETON
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PoolManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<PoolManager>();

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

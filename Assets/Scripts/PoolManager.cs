using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    #region SINGLETON
    private static PrefabManager instance;
    public static PrefabManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PrefabManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<PrefabManager>();

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

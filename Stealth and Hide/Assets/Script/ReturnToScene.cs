using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToScene : MonoBehaviour
{
     private int nextSceneToLoad;
    
    
    // Start is called before the first frame update
    void Start()
    {
       nextSceneToLoad = SceneManager.GetActiveScene().buildIndex - 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

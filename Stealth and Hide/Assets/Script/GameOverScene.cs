using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
   
    [SerializeField] private Button retryButton;

   
     private void Start()
    {
        retryButton.onClick.AddListener(OnClickRetryButton);       
    }

    internal void BringGameOver()
    {
        gameObject.SetActive(true);
    }

    private void OnClickRetryButton()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}    
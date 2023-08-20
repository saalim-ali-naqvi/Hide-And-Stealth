using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{

    [SerializeField] private GameOverScene gameOverScene;
    public static Player Instance;
    public float speed = 5f;
    private Vector3 movement;
    public Text scoreText;



    private int score = 0;
    private Rigidbody2D myRigidbody;
    private Vector2 _initialPosition;

    ///////////
    public Image[] lives;
    public int livesRemaining = 3;

    private bool bGodMode = false;
    //private bool bNearBox = false;
    //private int NumberOfTimesBoxHasBeenHit = 3;
    //private GameObject _refBox;

    private bool isGoldCoinCollected = false;

    public MovementJoystick movementJoystick;




    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        _initialPosition = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerMovement();

        if (movementJoystick.joystickVec.y != 0)
        {
            myRigidbody.velocity = new Vector2(movementJoystick.joystickVec.x * speed, movementJoystick.joystickVec.y * speed);
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
        }
        
        // if (bNearBox)
        // {
        //     if (Input.GetKeyUp(KeyCode.Space))
        //     {
        //         NumberOfTimesBoxHasBeenHit++;
        //         if (NumberOfTimesBoxHasBeenHit == 3)
        //         {
        //             if (_refBox)
        //                 Destroy(_refBox);
        //         }
        //     }
        // }

    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.gameObject);
            UpdateScore(++score);
            //score++; //score = score + 1;
            //scoreText.text = score.ToString();
        }

        else if (collision.collider.tag == "Enemy")
        {
            Player.Instance.ResetPlayerToInitialPosition();
            if (bGodMode == false)
                Loselife();
        }

        else if (collision.collider.tag == "Box")
        {
           Destroy(collision.gameObject);
        }
        else if(collision.collider.tag == "Gold")
        {
            Destroy(collision.gameObject);
            UpdateScore(++score);
            isGoldCoinCollected = true;
        }
        else if (collision.collider.CompareTag("Door"))
        {
            Debug.Log("Special Coin Was not collected, The Door is locked, collect the Special coin to Unlock the door");
            if (isGoldCoinCollected)
            {
               var nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }

    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }



    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (String.Equals(other.gameObject.tag, "Box"))
    //     {
    //         bNearBox = true;
    //         _refBox = other.gameObject;

    //     }
    // }



    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (String.Equals(collision.gameObject.tag, "Box"))
    //     {
    //         bNearBox = false;
    //     }
    // }


    internal void Loselife()
    {
        bGodMode = true;
        livesRemaining--;
        if (livesRemaining < 0) livesRemaining = 0;
        lives[livesRemaining ].enabled = false;
       

        Invoke(nameof(ResetGodMode), 1);

        if (livesRemaining == 0)
        {
            gameOverScene.BringGameOver();
          Debug.Log("You Lost the Game");
        }
    }

    private void ResetGodMode()
    {
        bGodMode = false;
    }

    private void HandlePlayerMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;
        //myRigidbody.velocity = pos;
    }

    internal void ResetPlayerToInitialPosition()
    {
        transform.position = _initialPosition;
        myRigidbody.velocity = Vector2.zero;
        //score = 0;
        //scoreText.text = score.ToString();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    
    private Transform player;

    private Vector3 tempPos;


    [SerializeField]
    private float minX, maxX;

    [SerializeField]
    private float minY, maxY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        tempPos = transform.position;
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;




        if (tempPos.x < minX)
        {
            tempPos.x = minX;
        }
        if (tempPos.x > maxX)
        {
            tempPos.x = maxX;
        }

        if (tempPos.y < minY)
        {
            tempPos.y = minY;
        }
        if (tempPos.y > maxY)
        {
            tempPos.y = maxY;
        }

        transform.position = tempPos;
    }
}
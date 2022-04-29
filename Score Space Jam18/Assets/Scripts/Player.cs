using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    //speed to move the player
    public float playerSpeed;
    //can the player move
    public bool isMove;

    [Header("Throw Things")]
    public GameObject magnetThing;
    //time taken to destory gameobject
    public float timeToDestory;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        PlayerThrow();
    }

    void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(inputX * playerSpeed, inputY * playerSpeed);
    }

    void PlayerThrow()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            GameObject gameobject = Instantiate(magnetThing, transform.position, Quaternion.identity);

            //destroy the metal thing after a duration of some time in this case when it does not collide with any boss
            Destroy(gameobject, timeToDestory);
        }
    }
}

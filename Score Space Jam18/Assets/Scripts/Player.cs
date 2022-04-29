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

    public static Vector3 MousePos;
    [SerializeField] Camera cam;

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
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(inputX * playerSpeed, 0 * playerSpeed);
    }
}

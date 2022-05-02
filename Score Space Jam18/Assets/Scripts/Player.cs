using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    //speed to move the player
    public float playerSpeed;
    //can the player move
    public bool isMove;

    public static float Health = 1f;
    [SerializeField] GameObject healthbar;

    //varibles for getting world position
    public static Vector3 MousePos;
    [SerializeField] Camera cam;


    //used to setup leaderboard
    public LeaderBoard leaderboard;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement();

        if (Metal.StaticHeldObject != null)
        {
            Health -= 0.001f;
        }
    }

    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.x);

        healthbar.transform.localScale = new Vector2(Health * 17f, 0.4f);
    }

    void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(inputX * playerSpeed, 0 * playerSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //StartCoroutine(DeathCoroutine());
        }
    }
    
    IEnumerator DeathCoroutine()
    {
        yield return leaderboard.SubmitScoreRoutine(GameManeger.score);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(3);
    }

}

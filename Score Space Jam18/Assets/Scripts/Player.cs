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
    [SerializeField] LineRenderer lineRenderer;

    //varibles for getting world position
    public static Vector3 MousePos;
    [SerializeField] Camera cam;


    //used to setup leaderboard
    public LeaderBoard leaderboard;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WaveManeger.Score = 0;
    }

    private void FixedUpdate()
    {
        Movement();

        if (Health < 0.01)
        {
            Time.timeScale -= 0.05f;
            Health = 0f;
            if (Time.timeScale <= 0.05)
            {
                SceneManager.LoadScene(3);
                StartCoroutine(DeathCoroutine());
            }

        }
    }

    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.x);

        healthbar.transform.localScale = new Vector2(Health * 17f, 0.4f);



        if (Input.GetMouseButton(0) && Metal.StaticHeldObject != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, Metal.StaticHeldObject.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
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
        if (collision.gameObject.tag == "Boss")
        {
            Health = 0;
        }
    }
    
    IEnumerator DeathCoroutine()
    {
        yield return leaderboard.SubmitScoreRoutine(GameManeger.score);
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(3);
    }

}

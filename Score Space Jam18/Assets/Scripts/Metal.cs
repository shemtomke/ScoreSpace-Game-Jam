using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    public float metalSpeed;

    Rigidbody2D rb;

    [SerializeField] GameObject HeldObject;
    [SerializeField] ParticleSystem Destroyeffect;
    public static GameObject StaticHeldObject;
    [SerializeField] bool move;
    [SerializeField] float speed;
    Collider2D collider;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (HeldObject != null)
        {
            if (HeldObject = gameObject)
            {
                //this moves the object with force so you can throw the object when you releace the mouse button
                rb.AddForce(new Vector2(Player.MousePos.x - transform.position.x, Player.MousePos.y - transform.position.y));
                //rotates to make it look cool
                transform.Rotate(0, 0, 4);
            }
        }
        else
        {
            //rotates back to 0 rotation when not held
            transform.Rotate(0, 0, (Mathf.DeltaAngle(transform.eulerAngles.z, 0) / 10));
        }

        StaticHeldObject = HeldObject;

    }

    private void Update()
    {
        if (move && transform.position.y < -2.9)
        {
            rb.AddForce(Vector2.left * Time.deltaTime * speed);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), 0);
        }
    }

    private void OnMouseDown()
    {
        //when the mouse is pressed the item is grabbed
        HeldObject = gameObject;
        rb.drag = 10;
    }

    private void OnMouseUp()
    {
        //when the mouse is releced the item is droped
        HeldObject = null;
        rb.drag = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }

        if (collision.gameObject.tag == "Boss")
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        collider.enabled = false;
        Destroyeffect.Play();
        spriteRenderer.enabled = false;
        WaveManeger.Score += 100;
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}

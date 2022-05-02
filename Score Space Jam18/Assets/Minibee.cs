using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minibee : MonoBehaviour
{
    [SerializeField] float targetX;
    Rigidbody2D rigidbody;
    [SerializeField] ParticleSystem deatheffect;
    Collider2D collider;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetX = Random.Range(-8, 5);
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector2((targetX - transform.position.x) / 10, 0));

        if (Mathf.Abs(targetX - transform.position.x) < 0.8f)
        {
            transform.Translate(Vector2.down / 5);
            rigidbody.gravityScale = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.Health -= 0.1f;
        }
        StartCoroutine(Deathcoroutine());
    }

    IEnumerator Deathcoroutine()
    {
        deatheffect.Play();
        collider.enabled = false;
        spriteRenderer.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        WaveManeger.Score += 50;
        Destroy(gameObject);
    }
}

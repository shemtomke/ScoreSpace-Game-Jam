using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] BossPrefabs;
    [SerializeField] List<GameObject> EnemyQue;
    [SerializeField] int SpawnXEnemys;
    [SerializeField] List<GameObject> EnemysInScene;

    SpriteRenderer spriteRenderer;
    Collider2D collider;
    [SerializeField] ParticleSystem Destroyeffect;

    [SerializeField] bool DoFinalAttack;
    float FinalAttackTarget;
    GameObject FinalAttackTargetObject;
    [SerializeField] bool attackduringRun;
    [SerializeField] bool moveduringrun;
    [SerializeField] float moveduringrunTarget;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();


        StartCoroutine(SpawnEnemys());
        moveduringrunTarget = Random.Range(-5.0f, 10.0f);
        FinalAttackTargetObject = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator SpawnEnemys()
    {

        for (int i = 0; i < SpawnXEnemys; i++)
        {
            EnemyQue.Add(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)]);
        }


        yield return new WaitForSecondsRealtime(Random.Range(1, 3));

        while (EnemyQue.Count > 0)
        {
            GameObject instanciated = Instantiate(EnemyQue[0]);
            EnemysInScene.Add(instanciated);
            EnemyQue.RemoveAt(0);

            yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        }
    }

    private void Update()
    {
        for (int i = 0; i < EnemysInScene.Count; i++)
        {
            if (EnemysInScene[i] == null)
            {
                EnemysInScene.Remove(EnemysInScene[i]);
            }
        }

        if (EnemyQue.Count == 0 && EnemysInScene.Count == 0 && DoFinalAttack)
        {
            DoFinalAttack = false;
            StartCoroutine(FinalAttack());

        }
    }

    IEnumerator FinalAttack()
    {
        FinalAttackTarget = FinalAttackTargetObject.transform.position.x;

        while (Mathf.Abs(moveduringrunTarget - transform.position.x) > .1f)
        {
            transform.Translate(new Vector2((FinalAttackTarget - transform.position.x) / 30, .02f));
            yield return new WaitForEndOfFrame();
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(moveduringrunTarget - transform.position.x) < .1f)
        {
            moveduringrunTarget = Random.Range(-5.0f, 10.0f);
            Debug.Log("move");
        }

        if (moveduringrun)
        {
            transform.Translate(new Vector2((moveduringrunTarget - transform.position.x) / 50, .02f));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        collider.enabled = false;
        Destroyeffect.Play();
        spriteRenderer.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }
}

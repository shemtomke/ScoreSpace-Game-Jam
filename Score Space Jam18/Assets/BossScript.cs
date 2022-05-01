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
    [SerializeField] GameObject[] Destroyondeath;

    SpriteRenderer spriteRenderer;
    Collider2D collider;
    Metal metal;
    [SerializeField] SpringJoint2D springJoint2D;
    [SerializeField] ParticleSystem Destroyeffect;

    [SerializeField] bool DoFinalAttack;
    float FinalAttackTarget;
    GameObject FinalAttackTargetObject;
    [SerializeField] bool attackduringRun;
    [SerializeField] bool moveduringrun;
    [SerializeField] bool isBee;
    [SerializeField] bool DieOnGround;
    [SerializeField] bool MoveOnGround;
    [SerializeField] bool move;
    [SerializeField] float moveduringrunTarget;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        metal = GetComponent<Metal>();


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

        if (move)
        {
            transform.Translate(Vector2.left * Time.deltaTime);
        }

        if (isBee && transform.position.x > 15)
        {
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator FinalAttack()
    {
        yield return new WaitForSecondsRealtime(1);
        springJoint2D.enabled = false;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (DieOnGround)
            {
                StartCoroutine(DeathCoroutine());
            }
            else if(MoveOnGround)
            {
                move = true;
                metal.enabled = true;
            }
        }
    }

    IEnumerator DeathCoroutine()
    {
        collider.enabled = false;
        Destroyeffect.Play();
        spriteRenderer.enabled = false;
        foreach (GameObject kill in Destroyondeath)
        {
            Destroy(kill);
        }
        yield return new WaitForSecondsRealtime(1);
        Instantiate(BossPrefabs[Random.Range(0, BossPrefabs.Length - 1)]);
        Destroy(gameObject);
    }
}

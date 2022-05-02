using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] BossPrefabs;
    [SerializeField] List<GameObject> EnemyQue;
    [SerializeField] int SpawnXEnemys;
    [SerializeField] List<GameObject> EnemysInScene;
    [SerializeField] GameObject[] Destroyondeath;
    public static bool BossInWorld;

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

    [SerializeField] TextMeshProUGUI HintText;
    [SerializeField] TextMeshProUGUI JokeText;
    [SerializeField] string[] JokeQuestions;
    [SerializeField] string[] JokeAnsewrs;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        metal = GetComponent<Metal>();
        BossInWorld = true;


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
            //Instantiate(BossPrefabs[Random.Range(0, BossPrefabs.Length - 1)]);
            WaveManeger.Score += 1000;
            WaveManeger.Runningevent = false;
            JokeText.text = "  ";
            Destroy(gameObject);
        }
    }

    IEnumerator FinalAttack()
    {
        StartCoroutine(PlayJoke());
        Time.timeScale = 0.8f;
        if (!isBee)
        {
            yield return new WaitForSecondsRealtime(8);
        }
        else
        {
            yield return new WaitForSecondsRealtime(1.5f);
        }

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

    IEnumerator PlayJoke()
    {
        int i = Random.Range(0, 9);
        JokeText.text = JokeQuestions[i];
        yield return new WaitForSecondsRealtime(4);
        JokeText.text = JokeAnsewrs[i];
        yield return new WaitForSecondsRealtime(4);
        JokeText.text = "  ";
        Time.timeScale = 1;
        if (isBee)
        {
            HintText.text = "Throw the bee Off the right side of the screen to kill it!!";
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
        //Instantiate(BossPrefabs[Random.Range(0, BossPrefabs.Length - 1)]);
        WaveManeger.Score += 1000;
        BossInWorld = false;
        HintText.text = "  ";
        WaveManeger.Runningevent = false;
        Destroy(gameObject);
    }
}

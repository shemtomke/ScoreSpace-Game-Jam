using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManeger : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] BossPrefabs;
    [SerializeField] GameObject Minibee;
    [SerializeField] List<GameObject> EnemyQue;
    [SerializeField] int SpawnXEnemys;
    [SerializeField] List<GameObject> EnemysInScene;
    [SerializeField] int Dificalty;
    public static bool Runningevent;

    int i;

    public static float IncreasedEnemySpeed = 0;
 

    public static float Score;
    [SerializeField] TextMeshProUGUI ScoreText;
    private void Start()
    {
        StartCoroutine(SpawnEnemys());
    }

    private void Update()
    {
        ScoreText.text = Score.ToString();
        if (EnemyQue.Count == 0 && !Runningevent && EnemysInScene.Count == 0)
        {
            i = Random.Range(1, 4);
            if (i == 1)
            {
                Runningevent = true;
                StartCoroutine(BeeRain());
            }
            else if (i == 2)
            {
                Runningevent = true;
                StartCoroutine(SpawnEnemys());
            }
            else if (i == 3)
            {
                GameObject instancated = Instantiate(BossPrefabs[Random.Range(0, 2)]);
                instancated.SetActive(true);
                Runningevent = true;
            }

        }

        for (int i = 0; i < EnemysInScene.Count; i++)
        {
            if (EnemysInScene[i] == null)
            {
                EnemysInScene.Remove(EnemysInScene[i]);
            }
        }
    }

    IEnumerator SpawnEnemys()
    {

        for (int i = 0; i < SpawnXEnemys; i++)
        {
            EnemyQue.Add(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)]);
        }


        yield return new WaitForSecondsRealtime(Random.Range(1, 2));

        while (EnemyQue.Count > 0)
        {
            for (int i = 0; i < Mathf.RoundToInt(IncreasedEnemySpeed / 5) + 1; i++)
            {
                GameObject instanciated = Instantiate(EnemyQue[0]);
                EnemysInScene.Add(instanciated);
                EnemyQue.RemoveAt(0);
            }

            yield return new WaitForSecondsRealtime(Random.Range(1, 3) - IncreasedEnemySpeed);
        }

        Runningevent = false;
    }

    IEnumerator BeeRain()
    {
        yield return new WaitForSecondsRealtime(2);

        for (int i = 0; i < Random.Range(5, 15); i++)
        {
            Instantiate(Minibee);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        Runningevent = false;

    }

    private void FixedUpdate()
    {
        
    }
}

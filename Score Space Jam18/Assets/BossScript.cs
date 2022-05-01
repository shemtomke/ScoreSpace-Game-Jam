using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] BossPrefabs;
    [SerializeField] List<GameObject> EnemyQue;
    [SerializeField] int SpawnXEnemys;

    void Start()
    {
        StartCoroutine(SpawnEnemys());
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
            Instantiate(EnemyQue[0]);
            EnemyQue.RemoveAt(0);

            yield return new WaitForSecondsRealtime(Random.Range(1, 3));
        }


    }
}

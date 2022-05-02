using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManeger : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] BossPrefabs;
    [SerializeField] List<GameObject> EnemyQue;
    [SerializeField] int SpawnXEnemys;
    [SerializeField] List<GameObject> EnemysInScene;
    [SerializeField] int Dificalty;
 

    public static float Score;
    [SerializeField] TextMeshProUGUI ScoreText;

    private void Update()
    {
        ScoreText.text = Score.ToString();
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

    private void FixedUpdate()
    {
        
    }
}

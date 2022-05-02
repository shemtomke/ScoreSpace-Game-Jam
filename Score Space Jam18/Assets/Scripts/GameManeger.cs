using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public static int score;
    [SerializeField] int ForceChangeScore;

    private void Update()
    {
        score = (int)WaveManeger.Score;
    }
}

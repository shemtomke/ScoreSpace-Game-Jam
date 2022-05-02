using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        textMeshProUGUI.text = WaveManeger.Score.ToString();
    }

    private void Update()
    {
        textMeshProUGUI.text = WaveManeger.Score.ToString();
    }
}

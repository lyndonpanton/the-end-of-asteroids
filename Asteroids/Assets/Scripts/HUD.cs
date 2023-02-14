using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;

/// <summary>
/// A canvas which displays the current score of the canvas
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;

    float timeElapsed;

    bool isTimerRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = "0";
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning) {
            timeElapsed += Time.deltaTime;
            textMeshProUGUI.text = ((int)timeElapsed).ToString(); 
        }
    }

    public void StopGameTimer()
    {
        isTimerRunning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public static float timer;
    private string _niceTime;
    public Text _timerText;

    void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        _niceTime = string.Format("TIME: {0:0}:{1:00}", minutes, seconds);
        _timerText.text = _niceTime;
    }
}

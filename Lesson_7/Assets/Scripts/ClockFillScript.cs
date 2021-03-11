using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockFillScript : MonoBehaviour
{
    [HideInInspector] public float MaxTime;
    public Image img;
    private float _curentTime;
    [SerializeField] private bool play;
    [SerializeField] private bool loop;
    public enum States
    {
        Waiting,
        Playing,
        Finished
    }
    public States State;
    private void Start()
    {
        State = States.Waiting;
    }

    void Update()
    {
        if (play)
        {
            PlayCircle();
            loop = false;
        }
        if (loop)
        {
            PlayCircle();
        }
    }

    public void PlayCircle()
    {
        State = States.Playing;
        _curentTime += Time.deltaTime;
        img.fillAmount = _curentTime / MaxTime;
        if (_curentTime >= MaxTime)
        {
            State = States.Finished;
            _curentTime = 0;
            play = false;
            img.fillAmount = 1;
        }
    }
    /// <summary>
    /// запустить постоянное повторение
    /// </summary>
    public void StartLoop()
    {
        loop = true;
    }
    /// <summary>
    /// единожды выполнить
    /// </summary>
    public void PlayOnce()
    {
        play = true;
    }

    public void FillAmmountToZero()
    {
        img.fillAmount = 0;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianRaid : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    public GameObject FightPosition;
    public AnimationCurve Easing;
    public GameControllerScript gameControllerScript;
    private float _time;
    [SerializeField] private GameObject FightPlace;
    public enum States
    {
        Moving,
        Stand,
        Finished
    }
    public States State;
    void Start()
    {
        State = States.Moving;
        _startPosition = transform.position;
        _endPosition = FightPosition.transform.position;
    }

    
    void Update()
    {
        if(State == States.Moving)
        {
            Move();
        }
        
    }

    public void Move()
    {
        _time += Time.deltaTime;
        transform.position = LerpMoveTo(_startPosition, _endPosition, gameControllerScript.SecondsTillRaid);
        if(_time >= gameControllerScript.SecondsTillRaid)
        {
            FightPlace.SetActive(true);
            _time = 0;
            State = States.Finished;
            StartCoroutine(DelayForFight());
        }
    }

    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }

    IEnumerator DelayForFight()
    {
        yield return new WaitForSeconds(gameControllerScript.TimeToFight);
        transform.position = _startPosition;
        FightPlace.SetActive(false);
        State = States.Moving;
    }
}

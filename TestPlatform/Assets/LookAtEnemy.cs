using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public static LookAtEnemy singleton;
    private Transform tr;
    private GameObject[] enemies;

    public delegate void Delegate();
    public static event Delegate myDelegate;
    private int rand;
    void Start()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        singleton = this;
        tr = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindNewClosestEnemy();
        }
        
        LootAtEnemy();
    }
    

    public void FindNewClosestEnemy()
    {
        myDelegate?.Invoke();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        rand = Random.Range(0, enemies.Length);

    }
    public void LootAtEnemy()
    {
        
        Vector3 direction = enemies[rand].transform.position - tr.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        tr.rotation = Quaternion.Lerp(tr.rotation, rotation, 5f * Time.deltaTime);
    }

}

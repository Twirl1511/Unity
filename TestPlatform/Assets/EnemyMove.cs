using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform tr;
    private GameObject player;
    private Transform playerTransform;
    public float speed;
    private void Start()
    {
        tr = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }
    private void FixedUpdate()
    {
        Vector3 playerPos = playerTransform.position;
        tr.position = Vector3.MoveTowards(tr.position, Vector3.Lerp(tr.position, playerPos, 0.1f), speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }



    private void OnDisable()
    {

    }
}

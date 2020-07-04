using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBehavior : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public bool IsAlive;

    private Rigidbody2D rb;

    private Animator animator;

    public GameObject shootPrefab;

    public int enemyType;

    public float lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rb.gravityScale = 0.0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        IsAlive = true;
        lastShoot = 0L;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsAlive", IsAlive);
        animator.SetInteger("EnemyType", enemyType);

        if (Time.time - lastShoot > 3.0f)
        {
            Vector2 position = transform.GetChild(0).transform.position;

            RaycastHit2D hit = Physics2D.Linecast(position, position + new Vector2(0, -1000));
            float randomValue = UnityEngine.Random.value;
            bool shouldShoot = randomValue > 0.99f;
            if (hit.collider != null && hit.collider.gameObject.tag == "Player" && shouldShoot)
            {
                Shoot();
                lastShoot = Time.time;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerFire")
        {
            IsAlive = false;
        }
    }

    void Shoot()
    {
        GameObject fireIbject = Instantiate<GameObject>(shootPrefab);
        fireIbject.transform.position = transform.GetChild(0).transform.position;
        Rigidbody2D rigidBody = fireIbject.GetComponent<Rigidbody2D>();
        Vector3 direction = Vector3.down;
        rigidBody.AddForce(direction * 500.0f);
    }

    void OnDestroyAnimationFinish()
    {
        Destroy(gameObject);
    }
}

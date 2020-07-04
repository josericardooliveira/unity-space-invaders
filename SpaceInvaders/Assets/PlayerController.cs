using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject shootPrefab;

    private Vector3 moveDirection = Vector3.zero;

    private Rigidbody2D rb;

    public bool IsAlive;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rb.gravityScale = 0.0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        IsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAlive)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * 10.0f, 0.0f, 0.0f);
            rb.velocity = moveDirection;
            animator.SetBool("IsAlive", IsAlive);
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void FixedUpdate()
    {
        animator.SetBool("IsAlive", IsAlive);
    }

    void Shoot()
    {
        GameObject fireIbject = Instantiate<GameObject>(shootPrefab);
        fireIbject.transform.position = transform.GetChild(0).transform.position;
        Rigidbody2D rigidBody = fireIbject.GetComponent<Rigidbody2D>();
        Vector3 direction = Vector3.up;
        rigidBody.AddForce(direction * 1000.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyFire")
        {
            IsAlive = false;
        }
    }
}

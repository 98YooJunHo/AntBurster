using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D arrowRigidbody;
    private int damage;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        damage = 1;
        arrowRigidbody = GetComponent<Rigidbody2D>();
        arrowRigidbody.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<MonsterBehavior>().Hitted(damage);
            Destroy(gameObject);
        }
    }
}

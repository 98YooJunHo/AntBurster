using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTowerBehavior : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Animator bowAnimator;
    private Vector3 haveToShoot;
    private Vector3 direction;
    private bool targeted;
    private float timeAfterShoot;

    public int attackDamage;
    public float attackRate;
    public float attackRange;
    // Start is called before the first frame update
    void Start()
    {
        bowAnimator = transform.GetChild(0).GetComponent<Animator>();
        timeAfterShoot = 0;
        targeted = false;
        attackRate = 1f;
        attackRange = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterShoot += Time.deltaTime;
        if (timeAfterShoot > 1f / attackRate)
        {
            if(targeted == true)
            {
                timeAfterShoot = 0;
                GameObject arrow = 
                    Instantiate(arrowPrefab, transform.position, transform.rotation);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            targeted = true;
            bowAnimator.SetBool("Targeted", true);
            haveToShoot = other.gameObject.GetComponent<Transform>().position;
            direction = haveToShoot - transform.position;
            transform.up = direction;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            targeted = false;
            bowAnimator.SetBool("Targeted", false);
        }
    }

    public Vector3 Get_Dir()
    {
        return direction;
    }
}

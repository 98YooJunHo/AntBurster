using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class MonsterBehavior : MonoBehaviour
{
    private int enterConerCount;
    private Vector3 haveToGo;
    private Vector3 direction;
    private float speed = 1f;
    private GameObject coners;
    private GameManager gameManager;
    private int monsterLvl;
    private int monsterMaxHp;
    private int monsterHp;
    private TMP_Text hp;
    private TMP_Text lvl;
    // Start is called before the first frame update
    void Start()
    {
        enterConerCount = 0;
        hp = transform.GetChild(0).GetComponent<TMP_Text>();
        lvl = transform.GetChild(1).GetComponent<TMP_Text>();
        coners = GameObject.Find("Coners").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        monsterLvl = gameManager.Get_KillCount() / 10 + 1;
        for (int i = 0; i < monsterLvl; i++)
        {
            monsterMaxHp += ((monsterLvl * 4) / 3) + (i / 2);
        }
        monsterMaxHp += 4;
        monsterHp = monsterMaxHp;

        haveToGo = coners.transform.GetChild(0).GetComponent<Transform>().position;
        direction = haveToGo - transform.position;
        hp.text = "Hp: " + monsterHp.ToString() + "/ " + monsterMaxHp.ToString();
        lvl.text = "Lvl: " + monsterLvl.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.Get_IsPaused() == true)
        //{
        //    Time.timeScale = 0;
        //}

        //if (gameManager.Get_IsPaused() == false)
        //{
        //    Time.timeScale = 1;
        //}

        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    public int Get_Hp()
    {
        return monsterHp;
    }

    public void Hitted(int damamge)
    {
        monsterHp = monsterHp - damamge;
        if (monsterHp < 0)
        {
            monsterHp = 0;
        }
        hp.text = "Hp: "+ monsterHp.ToString() + "/ " + monsterMaxHp.ToString();
        if (monsterHp == 0)
        {
            Die();
            gameManager.Set_Gold(gameManager.Get_Gold() + monsterLvl);
            gameManager.Set_Score(gameManager.Get_Score() + monsterMaxHp);
            gameManager.Set_KillCount(gameManager.Get_KillCount() + 1);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Move()
    {
        haveToGo = coners.transform.GetChild(enterConerCount).GetComponent<Transform>().position;
        direction = haveToGo - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "End")
        {
            Die();
            gameManager.Set_Life(gameManager.Get_Life() - 1);
        }

        if (other.gameObject.tag == "Coner")
        {
            enterConerCount++;
            Move();
        }
    }
}

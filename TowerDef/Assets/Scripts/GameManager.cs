using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject turretArrow;
    public GameObject turretArrow_Img;
    public GameObject turretArrow_Img_Cant;
    public GameObject turretGun;
    public GameObject turretGun_Img;

    private RaycastHit2D hit;
    private int life;
    private bool isDead;
    private bool isWaitEnd;
    private bool isPaused;
    private bool isHoldTurret;
    private bool isShowBuild;
    private bool isShowCantBuild;
    private bool isTurretUi;
    private int gold;
    private int killCount;
    private float time;
    private int score;
    private int timeM;
    private int timeS;
    private TMP_Text timeText;
    private TMP_Text goldText;
    private TMP_Text scoreText;
    private TMP_Text lifeText;
    private TMP_Text scoreText_GameOver;
    private GameObject pauseBotton;
    private GameObject pauseScene;
    private GameObject gameOverScene;
    // Start is called before the first frame update
    void Start()
    {
        life = 8;
        isDead = false;
        isWaitEnd = false;
        isPaused = false;
        isHoldTurret = false;
        isShowBuild = false;
        isShowCantBuild = false;
        isTurretUi = false;
        pauseBotton = GameObject.Find("Button_Pause").gameObject;
        pauseScene = GameObject.Find("Pause").gameObject;
        gameOverScene = GameObject.Find("GameOver").gameObject;
        scoreText_GameOver = GameObject.Find("Score_GameOver").GetComponent<TMP_Text>();
        gameOverScene.SetActive(false);
        pauseScene.SetActive(false);
        time = 3.5f;
        score = 0;
        killCount = 0;
        gold = 150;
        timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
        goldText = GameObject.Find("Gold").GetComponent<TMP_Text>();
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        lifeText = GameObject.Find("Life").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGameEnd();
        CurrentTime();
        NowTime();
        CurrentGold();
        CurrentScore();
        CurrentLife();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isDead == false)
            {
                if (isPaused == false)
                {
                    Pause();
                }

                if (isPaused == true)
                {
                    Resume();
                }

                if (isHoldTurret == true)
                {
                    gold += 30;
                    isHoldTurret = false;
                    isShowBuild = false;
                    isShowCantBuild = false;
                    Destroy(GameObject.FindGameObjectWithTag("TurretImg"));
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTurretUi == true && isHoldTurret == false)
            {
                GameObject[] ranges = GameObject.FindGameObjectsWithTag("Range");
                foreach (GameObject range in ranges)
                {
                    range.GetComponent<SpriteRenderer>().enabled = false;
                }
                isTurretUi = false;
            }

            if (isTurretUi == false && isHoldTurret == false)
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(worldPosition, Vector2.zero, LayerMask.GetMask("Tile"));

                if (hit.collider.gameObject.tag == "Turret")
                {
                    SpriteRenderer range = hit.collider.gameObject.GetComponentInParent<SpriteRenderer>();
                    range.enabled = true;
                    isTurretUi = true;
                }
            }

            if (isShowBuild == true)
            {
                isHoldTurret = false;
                isShowBuild = false;
                Destroy(GameObject.FindGameObjectWithTag("TurretImg"));
                GameObject turret =
                    Instantiate(turretArrow, hit.transform.position, hit.transform.rotation);
                Collider2D turretBuilt = hit.collider;
                turretBuilt.enabled = false;
            }
        }

        if (isHoldTurret == true)
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPosition, Vector2.zero, LayerMask.GetMask("Tile"));

            if (isShowBuild == false && isShowCantBuild == false)
            {
                GameObject turretImg =
                    Instantiate(turretArrow_Img_Cant, worldPosition, Quaternion.identity);
                isShowCantBuild = true;
            }


            //if (hit.collider.gameObject.tag == "TurretSpot")
            //{
            //    Debug.Log("터렛짓는곳의 범위에 있다");
            //}
            //else
            //{
            //    Debug.Log(hit.collider.gameObject.tag + "\n" + hit.collider.gameObject.name);
            //}

            //if (isShowBuild == false)
            //{
            //    Debug.Log("설치가능 타워를 보여주지 않고 있다");
            //}

            if (hit.collider.gameObject.tag == "TurretSpot" && isShowBuild == false)
            {
                Destroy(GameObject.FindGameObjectWithTag("TurretImg"));

                Vector3 hitPosition = hit.transform.position;
                Quaternion hitRotation = hit.transform.rotation;
                GameObject turretImg =
                    Instantiate(turretArrow_Img, hitPosition, hitRotation);
                isShowBuild = true;
                isShowCantBuild = false;
            }

            if (hit.collider.gameObject.tag != "TurretSpot" && isShowBuild == true)
            {
                Debug.Log(hit.collider.ToString());
                Destroy(GameObject.FindGameObjectWithTag("TurretImg"));
                isShowBuild = false;
            }
        }
    }

    void CurrentTime()
    {
        if (time <= 0)
        {
            isWaitEnd = true;
        }

        if (isWaitEnd == false)
        {
            time -= Time.deltaTime;
        }

        if (isWaitEnd == true)
        {
            time += Time.deltaTime;
        }
    }

    void IsGameEnd()
    {
        if (life == 0)
        {
            isDead = true;
            Time.timeScale = 0;
            gameOverScene.SetActive(true);
            scoreText_GameOver.text = "Score : " + score.ToString();
        }
    }

    void NowTime()
    {
        timeM = Convert.ToInt32(Math.Floor(time / 60f));
        timeS = Convert.ToInt32(Math.Floor(time % 60f));
        timeText.text = "Time " + timeM.ToString() + ":" + timeS.ToString();
    }

    void CurrentScore()
    {
        scoreText.text = "Score : " + score.ToString();
    }

    void CurrentGold()
    {
        goldText.text = "Gold : " + gold.ToString();
    }

    void CurrentLife()
    {
        lifeText.text = "Life : " + life.ToString();
    }

    public int Get_KillCount()
    {
        return killCount;
    }

    public void Set_KillCount(int nowKillCount)
    {
        killCount = nowKillCount;
    }

    public bool Get_IsPaused()
    {
        return isPaused;
    }

    public void Buy_Turret()
    {
        if (gold < 30)
        {
            return;
        }

        if (isHoldTurret == false && isTurretUi == false)
        {
            isHoldTurret = true;
            gold -= 30;
        }
    }

    public void Pause()
    {
        if (isHoldTurret == true)
        {
            gold += 30;
            isHoldTurret = false;
            isShowBuild = false;
            isShowCantBuild = false;
            Destroy(GameObject.FindGameObjectWithTag("TurretImg"));
        }

        isPaused = true;
        pauseBotton.SetActive(false);
        Time.timeScale = 0;
        pauseScene.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        pauseBotton.SetActive(true);
        Time.timeScale = 1;
        pauseScene.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public int Get_Life()
    {
        return life;
    }

    public void Set_Life(int nowLife)
    {
        life = nowLife;
    }

    public int Get_Gold()
    {
        return gold;
    }

    public void Set_Gold(int nowGold)
    {
        gold = nowGold;
    }

    public int Get_Score()
    {
        return score;
    }

    public void Set_Score(int nowScore)
    {
        score = nowScore;
    }
}

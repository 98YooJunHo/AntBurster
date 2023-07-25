using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpotMaking : MonoBehaviour
{
    private GameObject[] turretSpot;

    private void Awake()
    {
        turretSpot = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            turretSpot[i] = transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            turretSpot[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DelayTime()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            yield return null;
            turretSpot[i].SetActive(true);
        }
    }
}

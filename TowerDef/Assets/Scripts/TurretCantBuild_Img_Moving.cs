using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCantBuild_Img_Moving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = worldPosition;
    }
}

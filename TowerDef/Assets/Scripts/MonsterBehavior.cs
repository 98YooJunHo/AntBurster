using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{


    private int monsterLvl;
    private int monsterMaxHp;
    private int monsterHp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public int Get_Hp()
    {
        return monsterHp;
    }

    void Hitted(int damamge)
    {
        monsterHp = monsterHp - damamge;
        if(monsterHp < 0) 
        {
            monsterHp = 0;
        }
    }

    void Die()
    {
        if(monsterHp == 0) 
        {
            Destroy(gameObject);
        }
    }

}

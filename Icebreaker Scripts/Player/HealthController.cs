using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject[] hpSprite;
    public int maxHp;
    int currHp;

    // Start is called before the first frame update
    void Start()
    {
        currHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Damaged(1);
        }
    }

    public void Damaged(int dmg)
    {
        currHp -= dmg;
        UpdateHealthSprite();
    }

    void UpdateHealthSprite()
    {
        if (currHp < 0)
        {
            Debug.Log("You died!");
            return;
        }
        for (int i = hpSprite.Length; i> currHp; --i)
        {
            hpSprite[i-1].SetActive(false);
        }

       
    }
}

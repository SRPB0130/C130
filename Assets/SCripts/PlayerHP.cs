using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    public Scrollbar HPBar;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPUI();
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0 , maxHP);
        UpdateHPUI();
    }

    public void Headl(int amount) 
    { 
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHPUI();
    }
    void UpdateHPUI()
    {
        if(HPBar != null)
        {
            HPBar.size = (float)currentHP / maxHP;
        }
    }

}

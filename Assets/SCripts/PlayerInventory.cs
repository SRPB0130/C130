using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int itemCount = 0;
    public Text itemCountText; // UI ¿¬°á

    private void Start()
    {
        UpdateItemUI();
    }

    public void AddItem(int amount = 1)
    {
        itemCount += amount;
        UpdateItemUI();
    }

    void UpdateItemUI()
    {
        if (itemCountText != null)
            itemCountText.text = "x " + itemCount.ToString();
    }
}

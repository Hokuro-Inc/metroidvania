using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory;
    public GameObject blankInventorySlot;
    public GameObject inventoryPanel;
    //public Text description;
    //public GameObject useButton;
    public Item currentItem;

    void OnEnable()
    {
        SetTextAndButton("", false);
        ClearInventorySlots();
        MakeInventorySlots();
    }

    public void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void SetTextAndButton(string description, bool activeButton)
    {
        //description.Text = description;
        //useButtton.SetActive(activeButton);
    }

    public void SetDescriptionAndButton(string newDescription, bool isUsable, Item newItem)
    {
        //description.Text = description;
        //useButtton.SetActive(activeButton);
        currentItem = newItem;
    }

    public void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);

                InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                if (newSlot)
                {
                    newSlot.Setup(playerInventory.items[i], this);
                }
            }
        }
    }

    public void ButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
        }
    }

    public void UpdateInventory()
    {
        ClearInventorySlots();
        MakeInventorySlots();
    }
}

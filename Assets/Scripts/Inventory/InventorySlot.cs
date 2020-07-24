using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage;
    public Sprite itemSprite;
    public InventoryManager manager;
    public Item item;

    public void Setup(Item newItem, InventoryManager newManager)
    {
        item = newItem;
        manager = newManager;

        if (item)
        {
            itemImage.sprite = item.itemSprite;
        }
    }

    public void OnClick()
    {
        if (item)
        {
            Debug.Log("showing");
            manager.SetDescriptionAndButton(item.itemDesciption, item.usable, item);
        }
    }
}

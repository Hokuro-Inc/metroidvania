using System.Collections.Generic;
using UnityEngine;

// Scriptable Object del inventario del jugador
[CreateAssetMenu(menuName = "Inventory/Inventory")]
[System.Serializable]
public class Inventory : GenericScriptableObject
{
    // En proceso de estudio
    //public Item currentItem;
    [Tooltip("Vida del jugador")]
    [SerializeField] private ExtendedFloatValue playerHealth;
    [Tooltip("Cantidad de dinero actual")]
    [SerializeField] private FloatValue currency;
    [Tooltip("Cantidad de pociones")]
    [SerializeField] private ExtendedFloatValue potions;
    [Tooltip("Número máximo de objetos equipables actualmente")]
    [SerializeField] private FloatValue maxEquipItems;
    [Tooltip("Comprueba si se ha desbloqueado o no la magia")]
    [SerializeField] private BoolValue manaUnlocked;
    [Tooltip("Cantidad actual de maná")]
    [SerializeField] private ExtendedFloatValue mana;
    [Tooltip("Posición del jugador")]
    [SerializeField] private VectorValue playerPosition;
    [Tooltip("Objetos en el inventario")]
    [SerializeField] public List<Item> items = new List<Item>();
    [Tooltip("Objetos equipados")]
    [SerializeField] public List<Item> equipment = new List<Item>();

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    /// <summary>
    /// Deletes the item form the inventory
    /// </summary>
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    /// <summary>
    /// Equips the item
    /// </summary>
    public void Equip(Item item)
    {
        equipment.Add(item);
    }

    /// <summary>
    /// Unequips the item
    /// </summary>
    public void Unequip(Item item)
    { 
        equipment.Remove(item);
    }

    /// <summary>
    /// Resets values to their default value to begin a new game
    /// </summary>
    public override void Reset()
    {
        //currentItem = null;
        playerHealth.Reset();
        currency.Reset();
        potions.Reset();
        maxEquipItems.Reset();
        manaUnlocked.Reset();
        mana.Reset();
        playerPosition.Reset();
        items.Clear();
        equipment.Clear();
    }
}
using System.Collections.Generic;
using UnityEngine;

// Scriptable Object del inventario del jugador
[CreateAssetMenu(menuName = "Inventory/Inventory")]
[System.Serializable]
public class Inventory : GenericScriptableObject
{
    // En proceso de estudio
    public Item currentItem;
    [Tooltip("Cantidad de dinero actual")]
    public int currency;
    [Tooltip("Cantidad máxima de pociones")]
    public int maxPotions;
    [Tooltip("Número máximo de objetos equipables actualmente")]
    public int maxEquipItems;
    [Tooltip("Comprueba si se ha desbloqueado o no la magia")]
    public BoolValue manaUnlocked;
    [Tooltip("Cantidad actual de maná")]
    public ExtendedFloatValue mana;
    [Tooltip("Objetos en el inventario")]
    public List<Item> items = new List<Item>();
    [Tooltip("Objetos equipados")]
    public List<Item> equipment = new List<Item>();

    // Añade el objeto al inventario del jugador
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    // Borra del inventario
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }


    // Equipar objeto
    public void Equip(Item item)
    {
        equipment.Add(item);
    }

    // Desequipa el objeto
    public void Unequip(Item item)
    { 
        equipment.Remove(item);
    }

    // Reseta al valor por defecto para empezar nueva partida
    public override void Reset()
    {
        currentItem = null;
        currency = 0;
        maxPotions = 3;
        maxEquipItems = 0;
        manaUnlocked.Reset();
        mana.Reset();
        items.Clear();
        equipment.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [Tooltip("Cantidad de dinero actual")]
    [SerializeField] private FloatValue currency;
    [Tooltip("Cantidad máxima de pociones")]
    [SerializeField] private ExtendedFloatValue Potions;
    [Tooltip("Número máximo de objetos equipables actualmente")]
    [SerializeField] private int maxEquipItems;
    /*[Tooltip("Comprueba si se ha desbloqueado o no la magia")]
    [SerializeField] private BoolValue manaUnlocked;
    [Tooltip("Cantidad actual de maná")]
    public ExtendedFloatValue mana;
    [Tooltip("Objetos en el inventario")]
    public List<Item> items = new List<Item>();
    [Tooltip("Objetos equipados")]
    public List<Item> equipment = new List<Item>();*/

    public void Reset()
    {
        currency.Reset();
        Potions.Reset();
        maxEquipItems = 0;
    }
}

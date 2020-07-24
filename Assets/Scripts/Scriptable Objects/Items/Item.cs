using UnityEngine;
using UnityEngine.Events;

// Scriptable Object de objetos del juego
[CreateAssetMenu(menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [Tooltip("Imagen del objeto")]
    public Sprite itemSprite;
    [Tooltip("Nombre del objeto")]
    public string itemName;
    [Tooltip("Descripción del objeto")]
    public string itemDesciption;
    [Tooltip("Es un objeto utilizable o no")]
    public bool usable;
    [Tooltip("Evento que invoca la función correspondiente")]
    public UnityEvent itemEvent;

    // Invoca a la función correspondiente que activa el uso del objeto
    public void Use()
    {
        Debug.Log("using");
        itemEvent.Invoke();
    }
}

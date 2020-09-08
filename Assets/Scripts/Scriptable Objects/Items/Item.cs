using UnityEngine;
using UnityEngine.Events;

// Scriptable Object de objetos del juego
[CreateAssetMenu(menuName = "Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    /*[Tooltip("Imagen del objeto")]
    public string itemSpritePath;*/
    [Tooltip("Sprite del item")]
    [SerializeField] public Sprite itemSprite;
    [Tooltip("Nombre del objeto")]
    [SerializeField] private string itemName;
    [Tooltip("Descripción del objeto")]
    [SerializeField] public string itemDesciption;
    [Tooltip("Es un objeto utilizable o no")]
    [SerializeField] public bool usable;
    [Tooltip("Evento que invoca la función correspondiente")]
    [SerializeField] private UnityEvent itemEvent;

    /*[JsonIgnore]
    public Sprite itemSprite;*/

    /*private void Awake()
    {
        itemSprite = Resources.Load<Sprite>(itemSpritePath);
    }*/

    // Invoca a la función correspondiente que activa el uso del objeto
    public void Use()
    {
        Debug.Log("using");
        itemEvent.Invoke();
    }
}

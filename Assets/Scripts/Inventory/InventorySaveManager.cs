using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaveManager : MonoBehaviour
{
    // Referencia estática del script
    public static InventorySaveManager inventorySaveManager;

    [Tooltip("Referencia al inventario del jugador")]
    [SerializeField] private Inventory playerInventory;

    // Singleton del manager
    void Awake()
    {
        if (inventorySaveManager == null)
        {
            inventorySaveManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Carga los objetos del inventario al empezar
    void OnEnable()
    {
        LoadInventory();
        LoadEquipment();
    }

    // Borra los datos de los objetos y los reescribe
    void OnDisable()
    {
        ResetInventory();
        ResetEquipment();
        SaveInventory();
        SaveEquipment();
    }

    // Guarda los datos de los objetos del inventario
    private void SaveInventory()
    {
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(playerInventory.items[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    // Guarda los datos de los objetos equipados
    private void SaveEquipment()
    {
        for (int i = 0; i < playerInventory.equipment.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.eqp", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(playerInventory.equipment[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    // Carga los datos de los objetos del inventario
    private void LoadInventory()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var aux = ScriptableObject.CreateInstance<Item>();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), aux);
            file.Close();
            playerInventory.items.Add(aux);
            i++;
        }
    }

    // Carga los datos de los objetos equipados
    private void LoadEquipment()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.eqp", i)))
        {
            var aux = ScriptableObject.CreateInstance<Item>();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.eqp", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), aux);
            file.Close();
            playerInventory.equipment.Add(aux);
            i++;
        }
    }

    // Borra los datos de los objetos del inventario
    private void ResetInventory()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
            i++;
        }
    }

    // Borra los datos de los objetos equipados
    private void ResetEquipment()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.eqp", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.eqp", i));
            i++;
        }
    }

    // Llama a los métodos Save
    public void SaveItems()
    {
        SaveInventory();
        SaveEquipment();
    }

    // Llama a los métodos Load
    public void LoadItems()
    {
        LoadInventory();
        LoadEquipment();
    }

    // Llama a los métodos Reset
    public void RestartGame()
    {
        ResetInventory();
        ResetEquipment();
    }
}

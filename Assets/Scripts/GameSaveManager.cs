using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    // Referencia estática del script
    public static GameSaveManager gameSaveManager;

    [Tooltip("Lista de elementos que hay que guardar entre partidas")]
    [SerializeField] private List<GenericScriptableObject> scriptableObjects = new List<GenericScriptableObject>();

    // Singleton del manager
    void Awake()
    {
        if (gameSaveManager == null)
        {
            gameSaveManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    /*// Carga los datos guardados al empezar
    void OnEnable()
    {
        LoadScriptableObjects();
    }

    // Guarda los datos al salir
    void OnDisable()
    {
        SaveScriptableObjects();
    }*/

    // Guarda todos los datos
    public void SaveScriptableObjects()
    {
        for (int i = 0; i < scriptableObjects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(scriptableObjects[i]);
            binary.Serialize(file, json);
            file.Close();
        }
    }

    // Carga todos los datos
    public void LoadScriptableObjects()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
        {
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), scriptableObjects[i]);
            file.Close();
            i++;
        }
    }

    // Borra todos los datos
    public void ResetScriptableObjects()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
            i++;
        }
    }

    // Borra los datos guardados y reinicia todas las variables
    public void RestartGame()
    {
        ResetScriptableObjects();
        foreach (GenericScriptableObject scriptableObject in scriptableObjects)
        {
            scriptableObject.Reset();
        }
    }
}

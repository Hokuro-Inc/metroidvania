using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
/*
[System.Serializable]
public class GameData
{
    public ExtendedFloatValue playerHealth;
    public Inventory playerInventory;
    public VectorValue playerPosition;
}*/

public class GameSaveManager : MonoBehaviour
{
    // Referencia estática del script
    public static GameSaveManager instance;

    //[Tooltip("Lista de elementos que hay que guardar entre partidas")]
    //[SerializeField] private List<GenericScriptableObject> scriptableObjects = new List<GenericScriptableObject>();
    //[SerializeField] private SaveManager saveManager;
    //[SerializeField] private GameData gameData;
    //[SerializeField] private GenericSORuntimeSet set;

    [SerializeField] private Inventory playerInventory;

    // Singleton del manager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Carga los datos guardados al empezar
    void OnEnable()
    {
        //LoadScriptableObjects();
        LoadData();
    }

    // Guarda los datos al salir
    void OnDisable()
    {
        //SaveScriptableObjects();
        SaveData();
    }

    #region test

    // Guarda todos los datos
    /*public void SaveScriptableObjects()
    {
        for (int i = 0; i < scriptableObjects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(scriptableObjects[i]);
            File.WriteAllText(Application.persistentDataPath + string.Format("/{0}.json", i), json);
            binary.Serialize(file, json);
            file.Close();
        }
        //FileStream file = File.Create(Application.persistentDataPath + "/gamedata.save");
        //BinaryFormatter binary = new BinaryFormatter();
        /*var json = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);
        MemoryStream ms = new MemoryStream();
        BsonWriter writer = new BsonWriter(ms);
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(writer, gameData);*/
    //binary.Serialize(file, json);
    //file.Close();
    /*MemoryStream ms = new MemoryStream();
    BsonWriter writer = new BsonWriter(ms);
    JsonSerializer serializer = new JsonSerializer();
    serializer.Serialize(writer, set);
    string data = Convert.ToBase64String(ms.ToArray());
    File.WriteAllText(Application.persistentDataPath + "/gamedata.save", data);

    var json = JsonConvert.SerializeObject(set);
    File.WriteAllText(Application.persistentDataPath + "/gamedata.json", json);
}

// Carga todos los datos
/*public void LoadScriptableObjects()
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
    /*if (File.Exists(Application.persistentDataPath + "/gamedata.save"))
    {
        //FileStream file = File.Open(Application.persistentDataPath + "/gamedata.save", FileMode.Open);
        //BinaryFormatter binary = new BinaryFormatter();
        //JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), saveManager);
        StreamReader sr = new StreamReader(Application.persistentDataPath + "/gamedata.save");
        string json = sr.ReadToEnd();
        //gameData = JsonConvert.DeserializeObject<GameData>(json);
        /*MemoryStream ms = new MemoryStream();
        BsonReader reader = new BsonReader(ms);
        JsonSerializer serializer = new JsonSerializer();
        gameData = serializer.Deserialize<GameData>(reader);
        //file.Close();
        byte[] data = Convert.FromBase64String(json);
        MemoryStream ms = new MemoryStream(data);
        BsonReader reader = new BsonReader(ms);
        JsonSerializer serializer = new JsonSerializer();
        set = serializer.Deserialize<GenericSORuntimeSet>(reader);

        //Debug.Log(set.set[0].name);
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
    /*if (File.Exists(Application.persistentDataPath + "/gamedata.save"))
    {
        File.Delete(Application.persistentDataPath + "/gamedata.save");
    }
}

// Borra los datos guardados y reinicia todas las variables
public void RestartGame()
{
    //ResetScriptableObjects();
    foreach (GenericScriptableObject scriptableObject in scriptableObjects)
    {
        scriptableObject.Reset();
    }
    //saveManager.Reset();
}*/

    #endregion

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/data.sav", FileMode.Create, FileAccess.Write);
        var json = JsonUtility.ToJson(playerInventory);
        File.WriteAllText(Application.persistentDataPath + string.Format("/data.json"), json);
        formatter.Serialize(stream, json);
        stream.Close();
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/data.sav"))
        {
            //var aux = ScriptableObject.CreateInstance<Inventory>();
            Stream file = new FileStream(Application.persistentDataPath + "/data.sav", FileMode.Open, FileAccess.Read);
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), playerInventory);
            //playerInventory = aux;
            file.Close();
        }
    }

    // Borra todos los datos
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/data.sav"))
        {
            File.Delete(Application.persistentDataPath + "/data.sav");
        }
    }

    // Borra los datos guardados y reinicia todas las variables
    public void RestartGame()
    {
        ResetData();
        playerInventory.Reset();
    }
}
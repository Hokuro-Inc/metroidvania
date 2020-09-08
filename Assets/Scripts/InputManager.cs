using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public static Dictionary<string, KeyCode> keys;

    public KeyCode LeftKey { get; private set; }
    public KeyCode RightKey { get; private set; }
    public KeyCode JumpKey { get; private set; }
    public KeyCode AttackKey { get; private set; }
    public KeyCode DashKey { get; private set; }
    public KeyCode InteractKey { get; private set; }
    public KeyCode InventoryKey { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (Instance == null) Instance = this;

        InitializeInputDictionary();
        AssignKeys();
    }

    /// <summary>
    /// Initializes the input dictionary
    /// </summary>
    private void InitializeInputDictionary()
    {
        keys = new Dictionary<string, KeyCode>
        {
            { "Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")) },
            { "Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")) },
            { "Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")) },
            { "Attack", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "LeftControl")) },
            { "Dash", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dash", "LeftShift")) },
            { "Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F")) },
            { "Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "I")) }
            //keys.Add("Pause", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Pause", "Escape")));
        };
    }

    private void AssignKeys()
    {
        LeftKey = keys["Left"];
        RightKey = keys["Right"];
        JumpKey = keys["Jump"];
        AttackKey = keys["Attack"];
        DashKey = keys["Dash"];
        InteractKey = keys["Interact"];
        InventoryKey = keys["Inventory"];
    }

    /// <summary>
    /// Saves the input mananager configuration
    /// </summary>
    public static void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Resets the input manager configuration
    /// </summary>
    public static void ResetKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.DeleteKey(key.Key);
        }
        Instance.InitializeInputDictionary();
        Instance.AssignKeys();
    }
}
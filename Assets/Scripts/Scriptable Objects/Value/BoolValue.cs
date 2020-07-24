using UnityEngine;

// Scriptable Object para almacenar valores booleanos
[CreateAssetMenu(menuName = "Value/Bool Value")]
[System.Serializable]
public class BoolValue : GenericScriptableObject
{
    [Tooltip("Valor al inicio del juego")]
    public bool initialValue;
    [Tooltip("Valor durante la partida")]
    public bool runtimeValue;
    
    // Reseta al valor por defecto para empezar nueva partida
    public override void Reset()
    {
        runtimeValue = initialValue;
    }
}

using UnityEngine;

// Scriptable Object para almacenar valores vectoriales
[CreateAssetMenu(menuName = "Value/Vector Value")]
public class VectorValue : GenericScriptableObject, ISerializationCallbackReceiver
{
    // En proceso de estudio
    public Vector2 initialValue;
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }

    // Reseta al valor por defecto para empezar nueva partida
    public override void Reset()
    {
        initialValue = defaultValue;
    }
}

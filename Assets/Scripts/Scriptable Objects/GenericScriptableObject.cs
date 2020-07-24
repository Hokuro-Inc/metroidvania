using UnityEngine;

// Scriptable Object genérico para permitir que el resto pueda usar la función Reset al reiniciar el juego
public class GenericScriptableObject : ScriptableObject
{
    // Función virtual para que pueda ser reescrita por las clases descendientes
    public virtual void Reset() { }
}

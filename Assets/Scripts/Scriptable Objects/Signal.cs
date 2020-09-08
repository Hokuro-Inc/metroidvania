using System.Collections.Generic;
using UnityEngine;

// Scriptable Object para crear sistema de señales
[CreateAssetMenu(menuName = "Custom Signal")]
public class Signal : ScriptableObject
{
    // Lista de suscriptores de la señal
    private readonly List<SignalListener> listeners = new List<SignalListener>();

    // Recorre la lista llamando a los distintos suscriptores
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaise();
        }
    }

    // Añade suscriptor a la lista
    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }

    // Elimina suscriptor de la lista
    public void UnregisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}
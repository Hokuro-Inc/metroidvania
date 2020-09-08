using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    [Tooltip("Señal a la que se suscribe")]
    [SerializeField] private Signal signal;
    [Tooltip("Evento que dispara la señal")]
    [SerializeField] private UnityEvent signalEvent;

    // Invoca la función correspondiente por la activación de la señal
    public void OnSignalRaise()
    {
        signalEvent.Invoke();
    }

    // Se regista como suscriptor de la señal
    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    // Se desuscribe de la señal
    private void OnDisable()
    {
        signal.UnregisterListener(this);
    }
}

using UnityEngine;

[System.Serializable]
// Scriptable Object genérico para permitir que el resto pueda usar la función Reset al reiniciar el juego
public class GenericScriptableObject : ScriptableObject
{
    /*[Tooltip("Necesita o no guardarse entre partidas")]
    [SerializeField] protected bool needsSave;
    
    [SerializeField] protected GenericSORuntimeSet runtimeSet;*/

    // Función abstracta para que pueda ser reescrita por las clases descendientes
    public virtual void Reset() { }

    /*public void Awake()
    {
        Debug.Log("hola");
        runtimeSet = FindObjectOfType<GenericSORuntimeSet>();
        Debug.Log(runtimeSet);
    }*/

    /*protected void OnEnable()
    {
        /*runtimeSet = Object.FindObjectOfType<GenericSORuntimeSet>();
        Debug.Log(runtimeSet);
        if (needsSave)
        {
            //runtimeSet.Add(this);
        }
    }

    protected void OnDisable()
    {
        if (needsSave)
        {
            //runtimeSet.Remove(this);
        }
    }*/
}

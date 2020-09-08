using System.Collections.Generic;
using UnityEngine;

// Scriptable Object para almacenar listas
[CreateAssetMenu(menuName = "Value/List Value")]
[System.Serializable]
public class ListValue<T> : GenericScriptableObject
{
    public List<T> list = new List<T>();

    public void Add(T gameObject)
    {
        list.Add(gameObject);
    }

    public void Remove(T gameObject)
    {
        list.Remove(gameObject);
    }

    public bool Contains(T gameObject)
    {
        return list.Contains(gameObject);
    }

    public override void Reset()
    {
        list.Clear();
    }
}

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> set = new List<T>();

    public void Add(T item)
    {
        if (!set.Contains(item))
        {
            set.Add(item);
        }
    }

    public void Remove(T item)
    {
        if (set.Contains(item))
        {
            set.Remove(item);
        }
    }
}
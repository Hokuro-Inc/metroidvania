using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerClickHandler
{
    public Action clickFunc = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (clickFunc != null) clickFunc();
        }
    }
}

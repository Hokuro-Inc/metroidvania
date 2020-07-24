using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    [SerializeField] private TabManager tabManager;

    void Start()
    {
        tabManager.Suscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabManager.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabManager.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabManager.OnTabExit(this);
    }

}

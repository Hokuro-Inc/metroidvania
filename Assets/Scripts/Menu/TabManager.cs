using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] private List<TabButton> buttons;
    [SerializeField] private Sprite idleTab;
    [SerializeField] private Sprite hoverTab;
    [SerializeField] private Sprite activeTab;
    private TabButton selectedTab;

    void Start()
    {
        buttons = new List<TabButton>();
    }

    public void Suscribe(TabButton button)
    {
        buttons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.image.sprite = hoverTab;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.image.sprite = activeTab;
    }

    private void ResetTabs()
    {
        foreach (TabButton button in buttons)
        {
            if (button != selectedTab || selectedTab == null)
            {
                button.image.sprite = idleTab;
            }
        }
    }
}

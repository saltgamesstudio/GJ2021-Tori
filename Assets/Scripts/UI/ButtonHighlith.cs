using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHighlith : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Text theText;
    private Color defautlColor;

    private void Awake()
    {
        defautlColor = theText.color;
    }
    void Start()
    {

    }
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        theText.color = Color.white;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        theText.color = defautlColor;
    }
}

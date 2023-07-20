using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI theText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = new Color(1f, 1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = new Color(0.8039216f, 0.3647059f, 0.3647059f);
    }
}
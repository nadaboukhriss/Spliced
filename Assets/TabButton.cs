using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler
{
    [SerializeField]
    private TabGroup tabGroup;

    public Image background;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
    }

    public void Select()
    {

    }
    public void Deselect()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}

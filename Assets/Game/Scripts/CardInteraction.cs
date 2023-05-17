using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CardInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;
    
    private Canvas canvas;
    private bool isDragging;
    private GameObject startedContainer;
    private GameObject container;

    private void Awake()
    {
        canvas = gameObject.transform.root.GetComponent<Canvas>();
        startedContainer = gameObject.transform.parent.gameObject;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        eventData.selectedObject.transform.SetParent(canvas.gameObject.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (container != null && isDragging)
        {
            eventData.selectedObject.transform.SetParent(container.gameObject.transform);
            if (container.name == "BattleHerosContent")
            {
                eventData.selectedObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (container.name == "SquadContent")
            {
                eventData.selectedObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);
            }
            container = null;
        }
        else if (container == null && isDragging)
        {
            eventData.selectedObject.transform.SetParent(startedContainer.gameObject.transform);

        }
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging == false)
        {
            GameEvents.OpenCharacterScreen();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        PointerEventData pointerData = (PointerEventData)eventData;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerData.position,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position) + offset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            container = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            container = null;
        }
    }
}

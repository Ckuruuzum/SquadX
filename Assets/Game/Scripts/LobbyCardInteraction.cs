using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LobbyCardInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas canvas;
    private bool isDragging;
    private GameObject tmpContainer;


    [SerializeField] private GameObject humanContainer;
    [SerializeField] private GameObject demonContainer;
    [SerializeField] private GameObject zombieContainer;
    private Unit unit;

    private void Awake()
    {
        canvas = gameObject.transform.root.GetComponent<Canvas>();
        unit = gameObject.transform.GetComponent<UnitDisplay>().unit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        eventData.selectedObject.transform.SetParent(canvas.gameObject.transform);
        RemoveCardFromSquad(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (tmpContainer != null && isDragging)
        {
            eventData.selectedObject.transform.SetParent(tmpContainer.gameObject.transform);
            AddCardToSquad(eventData);
            tmpContainer = null;
        }
        else if (tmpContainer == null && isDragging)
        {

            switch (unit.unitRace)
            {
                case Unit.UnitRace.HUMAN:
                    eventData.selectedObject.transform.SetParent(humanContainer.transform);
                    break;
                case Unit.UnitRace.DEMON:
                    eventData.selectedObject.transform.SetParent(demonContainer.transform);
                    break;
                case Unit.UnitRace.ZOMBIE:
                    eventData.selectedObject.transform.SetParent(zombieContainer.transform);
                    break;
            }
        }
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging == false)
        {
            GameEvents.OpenCharacterScreen(unit);
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
            tmpContainer = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            tmpContainer = null;
        }
    }

    private void AddCardToSquad(PointerEventData eventData)
    {
        GameManager.Instance.deck.Add(eventData.selectedObject.GetComponent<UnitDisplay>().unit); 
    }
    private void RemoveCardFromSquad(PointerEventData eventData)
    {
        GameManager.Instance.deck.Remove(eventData.selectedObject.GetComponent<UnitDisplay>().unit);
    }
}

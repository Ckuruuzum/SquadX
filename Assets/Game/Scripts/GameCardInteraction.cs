using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas canvas;
    private bool isDragging;
    [SerializeField] private GameObject cardContainer;
    [SerializeField] private GameObject tmpContainer;
    [SerializeField] private Unit unit;

    private void OnEnable()
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (tmpContainer != null && isDragging)
        {
            // eventData.selectedObject.transform.SetParent(tmpContainer.gameObject.transform);
            SpawnCard(eventData);
        }
        else if (tmpContainer == null && isDragging)
        {
            eventData.selectedObject.transform.SetParent(cardContainer.transform);
        }
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging == false)
        {
            // GameEvents.OpenCharacterScreen(unit);
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

    private void SpawnCard(PointerEventData eventData)
    {
        RemoveFromSelectable();
        AddToReserve();
        ResetCardStatus(eventData);
        GameEvents.PlayManagerEvents.RefillSelectable();
    }

    private void ResetCardStatus(PointerEventData eventData)
    {
        eventData.selectedObject.SetActive(false);
        eventData.selectedObject.transform.SetParent(cardContainer.transform);
        gameObject.transform.GetComponent<UnitDisplay>().unit = null;
        tmpContainer = null;
    }

    private void AddToReserve()
    {
        if (PlayManager.instance.reserveSquadUnits.Contains(unit))
        {
            Debug.Log("CAN1");
        }
        PlayManager.instance.reserveSquadUnits.Add(unit);
    }

    private void RemoveFromSelectable()
    {
        PlayManager.instance.selectableSquadUnits.Remove(unit);

        if (PlayManager.instance.selectableSquadUnits.Contains(unit))
        {
            Debug.Log("CAN2");
        }
    }

}

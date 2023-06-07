using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas _canvas;
    private bool _isDragging;
    [SerializeField] private GameObject _cardContainer;
    [SerializeField] private GameObject _currentSpawnContainer;
    [SerializeField] private UnitDisplay _unitDisplay;

    private void Start()
    {
        _cardContainer = gameObject.transform.parent.gameObject;
    }
    private void OnEnable()
    {
        _canvas = gameObject.transform.root.GetComponent<Canvas>();
        _unitDisplay = gameObject.transform.GetComponent<UnitDisplay>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        eventData.selectedObject.transform.SetParent(_canvas.gameObject.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentSpawnContainer != null && _isDragging)
        {
            SpawnCard(eventData);
        }
        else if (_currentSpawnContainer == null && _isDragging)
        {
            eventData.selectedObject.transform.SetParent(_cardContainer.transform);
        }
        _isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDragging == false)
        {
            // GameEvents.OpenCharacterScreen(unit);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        PointerEventData pointerData = (PointerEventData)eventData;

        Vector2 currentPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            pointerData.position,
            _canvas.worldCamera,
            out currentPosition);

        transform.position = _canvas.transform.TransformPoint(currentPosition) + offset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            _currentSpawnContainer = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            _currentSpawnContainer = null;
        }
    }

    private void SpawnCard(PointerEventData eventData)
    {
        GameEvents.PlayManagerEvents.SpawnUnit(_unitDisplay.Unit, eventData);
        ResetCardStatus(eventData);
    }

    private void ResetCardStatus(PointerEventData eventData)
    {
        eventData.selectedObject.transform.SetParent(_cardContainer.transform);
        _currentSpawnContainer = null;
    }



}

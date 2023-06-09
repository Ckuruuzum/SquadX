using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas _canvas;
    private bool _isDragging;
    [SerializeField] private GameObject _cardTransform;
    [SerializeField] private GameObject _currentSpawnContainer;
    [SerializeField] private UnitDisplay _unitDisplay;

    private void Start()
    {
        _cardTransform = gameObject.transform.parent.gameObject;
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
            eventData.selectedObject.transform.SetParent(_cardTransform.transform);
        }
        _isDragging = false;
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
        UnitManager.instance.SpawnUnit(_unitDisplay.Unit, UnitManager.TEAM.Ally);
        //PlayManager.instance.OrganiseDeck(_unitDisplay.Unit, eventData);
        DestroyCard(eventData);
    }

    private void DestroyCard(PointerEventData eventData)
    {
        Destroy(eventData.selectedObject);
    }



}

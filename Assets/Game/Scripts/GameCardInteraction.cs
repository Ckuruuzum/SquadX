using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameCardInteraction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas _canvas;
    private bool _isDragging;
    [SerializeField] private Transform _cardTransform;
    [SerializeField] private GameObject _currentSpawnContainer;
    [SerializeField] private UnitDisplay _unitDisplay;

    private void Start()
    {
        _cardTransform = gameObject.transform.parent.gameObject.transform;
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
            SpawnCard(eventData.selectedObject);
        }
        else if (_currentSpawnContainer == null && _isDragging)
        {
            SendDefaultCardPosition(gameObject);
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

    private void SpawnCard(GameObject cardGo)
    {
        UnitManager.instance.SpawnUnit(_unitDisplay.Unit, UnitManager.TEAM.Ally, cardGo);
    }

    public void SendDefaultCardPosition(GameObject card)
    {
        card.transform.SetParent(_cardTransform);
        card.transform.position = _cardTransform.position;
    }

    public void SetCardTransfrom(Transform cardTransform)
    {
        _cardTransform = cardTransform;
    }


}

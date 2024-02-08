using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        CameraController.canCameraMove = false;
        PlayManager.instance.SwitchSpawnBoxStatus();
        eventData.selectedObject.transform.SetParent(_canvas.gameObject.transform);
        SetCardSize(new Vector3(0.5f, 0.5f, 0.5f), 0.25f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentSpawnContainer != null && _isDragging)
        {
            SpawnCard(eventData.selectedObject, _currentSpawnContainer.GetComponent<SpawnBox>().boxIndex);
        }
        else if (_currentSpawnContainer == null && _isDragging)
        {
            SendDefaultCardPosition(gameObject);
        }
        _isDragging = false;
        CameraController.canCameraMove = true;
        PlayManager.instance.SwitchSpawnBoxStatus();
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
            if (_currentSpawnContainer == null)
            {
                _currentSpawnContainer = collision.gameObject;
                _currentSpawnContainer.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            if (_currentSpawnContainer == null) return;
            if (collider.name == _currentSpawnContainer.name)
            {
                _currentSpawnContainer.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                _currentSpawnContainer = null;
            }
        }
    }

    private void SpawnCard(GameObject cardGo, int spawnBoxIndex)
    {
        UnitManager.instance.SpawnUnit(_unitDisplay.Unit, UnitManager.TEAM.Ally, cardGo, spawnBoxIndex);
    }

    public void SendDefaultCardPosition(GameObject card)
    {
        card.transform.SetParent(_cardTransform);
        card.transform.position = _cardTransform.position;
        SetCardSize(new Vector3(1f, 1f, 1f), 0.25f);
    }

    public void SetCardTransfrom(Transform cardTransform)
    {
        _cardTransform = cardTransform;
    }

    private void SetCardSize(Vector3 vector3, float duration)
    {
        gameObject.transform.DOScale(vector3, duration);
    }
}

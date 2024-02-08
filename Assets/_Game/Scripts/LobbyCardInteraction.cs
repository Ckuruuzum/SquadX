using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LobbyCardInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector3 offset;

    private Canvas _canvas;
    private bool _isDragging;
    private GameObject _tmpContainer;


    [SerializeField] private GameObject _humanContainer;
    [SerializeField] private GameObject _demonContainer;
    [SerializeField] private GameObject _zombieContainer;
    private Unit _unit;

    private void Awake()
    {
        _canvas = gameObject.transform.root.GetComponent<Canvas>();
        _unit = gameObject.transform.GetComponent<UnitDisplay>().Unit;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
        eventData.selectedObject.transform.SetParent(_canvas.gameObject.transform);
        RemoveCardFromSquad(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_tmpContainer != null && _isDragging)
        {
            eventData.selectedObject.transform.SetParent(_tmpContainer.gameObject.transform);
            AddCardToSquad(eventData);
            _tmpContainer = null;
        }
        else if (_tmpContainer == null && _isDragging)
        {

            switch (_unit.unitRace)
            {
                case Unit.UnitRace.HUMAN:
                    eventData.selectedObject.transform.SetParent(_humanContainer.transform);
                    break;
                case Unit.UnitRace.DEMON:
                    eventData.selectedObject.transform.SetParent(_demonContainer.transform);
                    break;
                case Unit.UnitRace.ZOMBIE:
                    eventData.selectedObject.transform.SetParent(_zombieContainer.transform);
                    break;
                case Unit.UnitRace.UNDEAD:
                    eventData.selectedObject.transform.SetParent(_zombieContainer.transform);
                    break;
            }
        }
        _isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isDragging == false)
        {
            GameEvents.OpenCharacterScreen(_unit);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        PointerEventData pointerData = (PointerEventData)eventData;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            pointerData.position,
            _canvas.worldCamera,
            out position);

        transform.position = _canvas.transform.TransformPoint(position) + offset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            _tmpContainer = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoxCollider2D collider))
        {
            _tmpContainer = null;
        }
    }

    private void AddCardToSquad(PointerEventData eventData)
    {
        GameManager.Instance.deck.Add(eventData.selectedObject.GetComponent<UnitDisplay>().Unit);
    }
    private void RemoveCardFromSquad(PointerEventData eventData)
    {
        GameManager.Instance.deck.Remove(eventData.selectedObject.GetComponent<UnitDisplay>().Unit);
    }
}

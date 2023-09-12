using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class SwipeAreaDetector : MonoBehaviour
{
    private Camera _camera;
    private SwipeArea _swipeArea;
    private PlayerInputs.SwipeActionsActions SwipeActions => InputActions.Instance.Actions.SwipeActions;

    [HideInInspector] public UnityEvent<SwipeWindowType> OnSwipeAreaReachedMaxSize = new UnityEvent<SwipeWindowType>();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        SwipeActions.Enable();
        SwipeActions.PrimaryContact.started += PrimaryContactStarted;
        SwipeActions.PrimaryContact.canceled += PrimaryContactCanceled;
    }

    private void Update()
    {
        if (CanSwipeArea())
        {
            _swipeArea.Scale(_camera.ScreenToWorldPoint(SwipeActions.PrimaryPosition.ReadValue<Vector2>()));
        }
    }

    private void OnDisable()
    {
        SwipeActions.Disable();
        SwipeActions.PrimaryContact.started -= PrimaryContactStarted;
        SwipeActions.PrimaryContact.canceled -= PrimaryContactCanceled;
    }

    private void PrimaryContactStarted(InputAction.CallbackContext obj)
    {
        Vector3 startPosition = SwipeActions.PrimaryPosition.ReadValue<Vector2>();
        startPosition.z = _camera.nearClipPlane;
        PointerEventData pointerEventData = new PointerEventData(GameContext.Instance.EventSystem);
        pointerEventData.position = startPosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        GameContext.Instance.EventSystem.RaycastAll(pointerEventData, raycastResults);

        foreach(RaycastResult result in raycastResults)
        {
            if(result.gameObject.TryGetComponent(out SwipeArea swipeArea))
            {
                _swipeArea = swipeArea;
                break;
            }
        }
    }

    private void PrimaryContactCanceled(InputAction.CallbackContext obj)
    {
        if (_swipeArea != null)
        {
            if (_swipeArea.IsSwipeAreaReachedMaxSize())
            {
                OnSwipeAreaReachedMaxSize?.Invoke(_swipeArea.SwipeWindowType);
            }
            StartCoroutine(_swipeArea.ReturnToOriginPosition());
            _swipeArea.ResetPreviousPosition();
            _swipeArea = null;
        }
    }

    private bool CanSwipeArea()
    {
        return _swipeArea != null && SwipeActions.PrimaryContact.IsInProgress();
    }
}
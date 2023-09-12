using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SwipeAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _transformToMove;
    [SerializeField] private float _animationSpeed;
    private Vector3 _originPosition;
    private OverlayCanvas _canvas;

    private const float MIN_DISTANCE_THRESHOLD = 0.005f;

    [HideInInspector] public UnityEvent OnMoveToCanvasCenterEnded = new UnityEvent();
    [HideInInspector] public UnityEvent OnReturnAnimationEnded = new UnityEvent();

    private void Start()
    {
        _canvas = FindObjectOfType<OverlayCanvas>();
    }

    public IEnumerator MoveToCanvasCenter(Vector2 offset)
    {
        StopAllCoroutines();

        _originPosition = _transformToMove.position;
        float startTime = Time.time;
        Vector3 targetPosition = new Vector3(_canvas.transform.position.x + offset.x, _canvas.transform.position.y + offset.y, _transformToMove.position.z);
        float distanceBetweenPoints = Vector3.Distance(_transformToMove.position, targetPosition);

        while (Vector3.Distance(_transformToMove.position, targetPosition) > MIN_DISTANCE_THRESHOLD)
        {
            float distanceCovered = (Time.time - startTime) * _animationSpeed;
            float fractionOfJourney = distanceCovered / distanceBetweenPoints;
            _transformToMove.position = Vector3.Lerp(_transformToMove.position, targetPosition, fractionOfJourney);
            yield return null;
        }
        OnMoveToCanvasCenterEnded?.Invoke();
    }

    public IEnumerator ReturnToOriginPosition()
    {
        StopAllCoroutines();

        float startTime = Time.time;
        float distanceBetweenPoints = Vector3.Distance(_transformToMove.position, _originPosition);

        while (Vector3.Distance(_transformToMove.position, _originPosition) > MIN_DISTANCE_THRESHOLD)
        {
            float distanceCovered = (Time.time - startTime) * _animationSpeed; 
            float fractionOfJourney = distanceCovered / distanceBetweenPoints;
            _transformToMove.position = Vector3.Lerp(_transformToMove.position, _originPosition, fractionOfJourney);
            yield return null;
        }
        OnReturnAnimationEnded?.Invoke();
    }
}
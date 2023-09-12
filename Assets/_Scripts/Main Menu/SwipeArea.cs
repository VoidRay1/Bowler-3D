using System.Collections;
using UnityEngine;

public class SwipeArea : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private SwipeDirection _direction;
    [SerializeField] private SwipeWindowType _swipeWindowType;
    [SerializeField] private Vector2 _maxSwipeSizeDelta;
    [SerializeField] private Vector2 _minSwipeSizeDelta;
    [SerializeField] private float _returnTimeToOriginPosition;
    [SerializeField] private float _speed = 90.0f;
    private Vector3 _previousPosition;

    public SwipeWindowType SwipeWindowType => _swipeWindowType;

    public void Scale(Vector3 endPosition)
    {
        switch (_direction)
        {
            case SwipeDirection.LeftToRight:
                CalculateSwipeSizeDelta(Vector2.right, endPosition);
                break;
            case SwipeDirection.RightToLeft:
                CalculateSwipeSizeDelta(Vector2.left, endPosition);
                break;
        }
    }

    public IEnumerator ReturnToOriginPosition()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _returnTimeToOriginPosition)
        {
            _rectTransform.sizeDelta = Vector2.Lerp(_rectTransform.sizeDelta, _minSwipeSizeDelta, elapsedTime / _returnTimeToOriginPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void ResetPreviousPosition()
    {
       _previousPosition = Vector3.zero;
    }

    public bool IsSwipeAreaReachedMaxSize()
    {
        return _rectTransform.sizeDelta == _maxSwipeSizeDelta;
    }

    private void CalculateSwipeSizeDelta(Vector2 direction, Vector3 endPosition)
    {
        Vector2 currentSizeDelta = _rectTransform.sizeDelta;
        float distance = Vector3.Distance(_previousPosition, endPosition);
        Vector2 normalizedDirection = (endPosition - _previousPosition).normalized;
        _previousPosition = endPosition;
        if (Vector2.Dot(direction, normalizedDirection) > 0)
        {
            if (_rectTransform.sizeDelta.x + (distance * _speed) < _maxSwipeSizeDelta.x)
            {
                _rectTransform.sizeDelta = new Vector2((distance * _speed) + currentSizeDelta.x, currentSizeDelta.y);
            }
            else
            {
                _rectTransform.sizeDelta = _maxSwipeSizeDelta;   
            }
        }
        else if(Vector2.Dot(direction, normalizedDirection) < 0)
        {
            if (_rectTransform.sizeDelta.x - (distance * _speed) > _minSwipeSizeDelta.x)
            {
                _rectTransform.sizeDelta = new Vector2(currentSizeDelta.x - (distance * _speed), currentSizeDelta.y);
            }
            else
            {
                _rectTransform.sizeDelta = _minSwipeSizeDelta;
            }
        }
    }
}
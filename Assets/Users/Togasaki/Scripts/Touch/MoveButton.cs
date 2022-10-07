using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour
{
    private RectTransform _rectTransform;
    private int _fingerId = -1;
    private bool _isTouched = false;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        var eventTrigger = GetComponent<EventTrigger>();

        var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };

        pointerDownEntry.callback.AddListener((data) => OnPointerDownDelegate((PointerEventData)data));

        eventTrigger.triggers.Add(pointerDownEntry);
    }

    private void Update()
    {
        ButtonFingerFunc();

    }

    void ButtonFingerFunc()
    {
        var touchCount = Input.touchCount;

        if (touchCount <= 0)
            return;

        for (var i = 0; i < touchCount; i++)
        {
            var touch = Input.GetTouch(i);

            if (touch.fingerId == _fingerId)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _isTouched = true;
                        Debug.Log(touchCount);
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _rectTransform.position = touch.position;
                        break;
                    case TouchPhase.Ended:
                        _isTouched = false;
                        Debug.Log(_isTouched);

                        _fingerId = -1;
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }

    private void OnPointerDownDelegate(PointerEventData data)
    {
        var touchCount = Input.touchCount;

        switch (touchCount)
        {
            case 1:
                {
                    var touch = Input.GetTouch(0);

                    _fingerId = touch.fingerId;
                    break;
                }
            case 2:
                {
                    var touch0 = Input.GetTouch(0);
                    var touch1 = Input.GetTouch(1);

                    // タップしている2本の指のうち、適する方を選ぶ
                    var delta0 = math.lengthsq(touch0.position - data.position);
                    var delta1 = math.lengthsq(touch1.position - data.position);
                    var touch = delta0 < delta1 ? touch0 : touch1;

                    _fingerId = touch.fingerId;
                    break;
                }
        }
    }
}
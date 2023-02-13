using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RoomViewController : MonoBehaviour
{
    enum State
    {
        None,
        Down,
        Dragging
    }

    State _state = State.None;

    Vector2 startPos = Vector2.zero;

    public static bool Dragging = false;

    private void Start()
    {
        //transform.localEulerAngles = new Vector3(0f, 90f, -90f);
    }

    private void Update()
    {
        HandleTouchState();
    }

    void HandleTouchState()
    {
        var _prevState = _state;

        if (_state == State.None)
        {
            Dragging = false;
            if (Input.touchCount > 0)
            {
                startPos = Input.touches[0].position;
                _state = State.Down;
            }
        }
        if (_state == State.Down)
        {
            Dragging = false;
            if (Input.touchCount == 0)
            {
                Click();
                _state = State.None;
                return;
            }

            var pos = Input.touches[0].position;
            var d = (pos - startPos).magnitude;

            if (d > GameController.Instance.ViewDragThreshold)
            {
                _state = State.Dragging;
                Dragging = true;
            }
        }
        if (_state == State.Dragging)
        {
            if (Input.touchCount == 0) 
            { 
                _state = State.None;
                return;
            }

            float d = (Input.touches[0].deltaPosition).x;
            float pixelsPerDegree = Screen.width / 80f;
            transform.Rotate(-transform.up, d / pixelsPerDegree);

        }
    }

    void Click()
    {

    }
}

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
    Vector2 _prevPos;

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

        bool mouseDown = Input.touchCount > 0 || Input.GetMouseButton(0);
        Vector2 pos = Input.touchCount > 0 ? Input.touches[0].position : Input.mousePosition;

        if (_state == State.None)
        {
            Dragging = false;
            if (mouseDown)
            {
                startPos = pos;
                _prevPos = startPos;
                _state = State.Down;
            }
        }
        if (_state == State.Down)
        {
            Dragging = false;
            if (!mouseDown)
            {
                _state = State.None;
                return;
            }

            var d = (pos - startPos).magnitude;

            if (d > GameController.Instance.ViewDragThreshold)
            {
                _state = State.Dragging;
                Dragging = true;
            }
        }
        if (_state == State.Dragging)
        {
            if (!mouseDown) 
            { 
                _state = State.None;
                return;
            }

            Vector2 d = (pos - _prevPos);
            float pixelsPerDegreeH = Screen.width / 80f;
            float pixelsPerDegreeV = Screen.height / 80f;

            Vector3 angles = transform.localEulerAngles;

            angles += Vector3.down * (d.x / pixelsPerDegreeH);
            angles += Vector3.right * (d.y / pixelsPerDegreeV);

            if (angles.x < 315f && angles.x > 300f)
                angles.x = 315f;
            if (angles.x > 45f && angles.x < 60f)
                angles.x = 45f;

            transform.localEulerAngles = angles;
        }

        if (mouseDown)
        {
            _prevPos = pos;
        }
    }
}

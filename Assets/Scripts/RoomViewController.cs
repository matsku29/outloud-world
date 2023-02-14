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

            Vector2 d = (Input.touches[0].deltaPosition);
            float pixelsPerDegreeH = Screen.width / 80f;
            float pixelsPerDegreeV = Screen.height / 80f;
            float y = transform.eulerAngles.y;
            //Vector3 right = new Vector3(Mathf.Cos(y * Mathf.Deg2Rad), Mathf.Sin(y * Mathf.Deg2Rad));

            Vector3 angles = transform.localEulerAngles;

            angles += Vector3.down * (d.x / pixelsPerDegreeH);
            angles += Vector3.right * (d.y / pixelsPerDegreeV);

            if (angles.x < 315f && angles.x > 300f)
                angles.x = 315f;
            if (angles.x > 45f && angles.x < 60f)
                angles.x = 45f;

            transform.localEulerAngles = angles;
        }
    }

    void Click()
    {

    }
}

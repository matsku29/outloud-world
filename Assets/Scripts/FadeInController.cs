using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fades an image alpha from 0 -> 1
/// Made for fading a scene to black
/// </summary>
public class FadeInController : MonoBehaviour
{
    public delegate void AfterFadeAction();
    public float FadeDuration = 0.5f;
    float _time = 0;
    bool _started = false;
    Image _image;
    Color _c;
    AfterFadeAction function_;

    void Start()
    {
        _image = GetComponent<Image>();
        _c = _image.color;
    }
    
    void Update()
    {
        if (!_started)
            return;

        _time += Time.deltaTime;
        if (_time > FadeDuration)
        {
            function_.Invoke();
            _started = false;
            return;
        }
        if (_image != null)
            _image.color = new Color(_c.r, _c.g, _c.b, _time / FadeDuration);
    }

    // Start fading out of the image, do a function after the fade is done.
    public void StartFade(AfterFadeAction function)
    {
        function_ = function;
        _started = true;
        // Start raycasting to prevent the player from pressing anything
        if (_image != null)
            _image.raycastTarget = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Fades an image alpha from 1 -> 0
/// Made for fading a scene in
/// </summary>

// TODO: consolidate fades into one
public class FadeController : MonoBehaviour
{
    public UnityEvent OnTransitionEnd;
    public float FadeDuration = 0.5f;
    Image _image;
    float _r;
    float _g;
    float _b;

    void Start()
    {
        _image = GetComponent<Image>();
        _r = _image.color.r;
        _g = _image.color.g;
        _b = _image.color.b;
    }
    
    void Update()
    {
        float t = Time.timeSinceLevelLoad;
        if (t > FadeDuration)
        {
            Destroy(gameObject);
            return;
        }
        _image.color = new Color(_r, _g, _b, 1f - Time.timeSinceLevelLoad / FadeDuration);

    }
}

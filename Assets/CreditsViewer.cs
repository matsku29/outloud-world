using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsViewer : MonoBehaviour
{
    public Image background;
    public RectTransform text;
    public GameObject canvas;
    public float scrollSpeed = 10f;

    public void Close()
    {
        background.color = Color.clear;

        text.transform.localPosition = Vector3.zero;
        canvas.SetActive(false);
    }

    public void ShowCredits()
    {
        canvas.SetActive(true);
        StartCoroutine(Sequence());
    }

    IEnumerator Sequence()
    {
        float t = 0f;
        text.transform.localPosition = Vector3.zero;

        // Fade to black
        while (t < 1f)
        {
            background.color = Color.Lerp(Color.clear, Color.black, t);
            t += Time.deltaTime;
            yield return null;
        }
        background.color = Color.black;

        float h = text.rect.height;
        while (text.transform.localPosition.y - h - 500f < 0f)
        {
            text.transform.localPosition += Vector3.up * Time.deltaTime * scrollSpeed;
            yield return null;
        }

        // Fade out
        while (t > 0f)
        {
            background.color = Color.Lerp(Color.clear, Color.black, t);
            t -= Time.deltaTime;
            yield return null;
        }
        background.color = Color.clear;

        text.transform.localPosition = Vector3.zero;
        canvas.SetActive(false);
    }
}

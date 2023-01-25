using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Image image;
    public TMPro.TextMeshProUGUI itemName;

    public void SetImage(Sprite sprite, string name)
    {
        image.sprite = sprite;
        itemName.text = name;
    }
}
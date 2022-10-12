using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionNumbers : MonoBehaviour {

    public Image imageContainer;
    public List<Sprite> imageList;

    public void SetPosition(int position) {
        imageContainer.sprite = imageList[position - 1];
    }
}

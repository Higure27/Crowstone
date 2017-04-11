using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLayoutHelper : MonoBehaviour {
	void Start () {
        //LayoutElement layout = GetComponent<LayoutElement>();
        RectTransform trans = GetComponent<RectTransform>();
        Text text = GetComponentInChildren<Text>();
        int length = 1 + (text.text.Length / 20);
        //layout.preferredHeight = 10 * length;
        //layout.minHeight = 10 * length;
        trans.sizeDelta = new Vector2(trans.sizeDelta.x, 25 * length);
	}
}

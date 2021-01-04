using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NColor : MonoBehaviour {
    public static Color CreateColor(float r, float g, float b, float a) {
        return new Color(r / 255, g / 255, b / 255, a / 255);
    }
}

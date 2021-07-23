using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugButtons : MonoBehaviour
{
    public TextMeshProUGUI debugText1;
    public TextMeshProUGUI debugText2;

    string text1 = "";
    string text2 = "";

    void Update()
    {
        debugText1.text = $"{text1}";
        debugText2.text = $"{text2}";
    }

    public void UpdateText1(string value)
    {
        text1 = $"{value}";
    }

    public void UpdateText2(string value)
    {
        text2 = $"{value}";
    }
}

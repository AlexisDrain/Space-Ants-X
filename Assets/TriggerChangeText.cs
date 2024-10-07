using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerChangeText : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    public void UpdateText(string newText)
    {
        textMeshProUGUI.text = newText;
    }

}

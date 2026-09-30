using TMPro;
using UnityEngine;

public class BackedText : MonoBehaviour
{
    private TextMeshProUGUI frontText;
    private TextMeshProUGUI backText;


    public void UpdateText(string text)
    {
        frontText.text = text;
        backText.text = text;
    }
}

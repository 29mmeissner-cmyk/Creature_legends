using UnityEngine;
using UnityEngine.UI;

public class InGameOverlayUI : MonoBehaviour
{
    public Text overlayText;
    public void ShowMessage(string message)
    {
        if (overlayText != null)
            overlayText.text = message;
    }
}
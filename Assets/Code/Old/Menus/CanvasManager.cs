using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private Canvas mainMenuCanvas, settingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuCanvas.enabled = true;
        settingsCanvas.enabled = false;
    }

    void DisplaySettingsMenu() 
    {
        mainMenuCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }
}

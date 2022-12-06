using UnityEngine;

namespace Code.UI
{
    public class ScreenFader : MonoBehaviour
    {
        [SerializeField] private Canvas menuCanvas;

        public void Show()
        {
            menuCanvas.enabled = true;
        }

        public void Hide()
        {
            menuCanvas.enabled = false;
        }
    }
}
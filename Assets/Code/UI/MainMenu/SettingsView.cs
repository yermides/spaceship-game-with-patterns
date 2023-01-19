using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.MainMenu
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button backToHomeButton;
        private IMainMenuMediator _mainMenuMediator;

        public void Configure(IMainMenuMediator mainMenuMediator)
        {
            _mainMenuMediator = mainMenuMediator;
            backToHomeButton.onClick.AddListener(OnBackToHomePressed);
        }

        private void OnDestroy()
        {
            backToHomeButton.onClick.RemoveListener(OnBackToHomePressed);
        }

        private void OnBackToHomePressed()
        {
            _mainMenuMediator.OnBackToHomePressed();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
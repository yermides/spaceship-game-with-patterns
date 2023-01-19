using System;
using Code.Common.Commands;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.MainMenu
{
    public class HomeView : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button showLeaderboardButton;
        [SerializeField] private Button showSettingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField, Scene] private string sceneToLoadOnStartPlay;
        private IMainMenuMediator _mainMenuMediator;

        public void Configure(IMainMenuMediator mainMenuMediator)
        {
            _mainMenuMediator = mainMenuMediator;
            startGameButton.onClick.AddListener(OnStartButtonPressed);
            showLeaderboardButton.onClick.AddListener(OnShowLeaderboardPressed);
            showSettingsButton.onClick.AddListener(OnShowSettingsPressed);
            quitButton.onClick.AddListener(OnQuitPressed);
        }

        private void OnDestroy()
        {
            startGameButton.onClick.RemoveListener(OnStartButtonPressed);
            showLeaderboardButton.onClick.RemoveListener(OnStartButtonPressed);
            showSettingsButton.onClick.RemoveListener(OnStartButtonPressed);
            quitButton.onClick.RemoveListener(OnQuitPressed);
        }
        
        private void OnStartButtonPressed()
        {
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            commandQueue.AddAndRunCommand(new LoadSceneCommand(sceneToLoadOnStartPlay));
        }
        
        private void OnShowLeaderboardPressed()
        {
            _mainMenuMediator.OnShowLeaderboardPressed();
        }
        
        private void OnShowSettingsPressed()
        {
            _mainMenuMediator.OnShowSettingsPressed();
        }
        
        public void OnQuitPressed()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
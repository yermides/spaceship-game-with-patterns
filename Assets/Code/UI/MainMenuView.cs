using Code.Common.Commands;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button showLeaderboardButton;
        [SerializeField] private Button quitButton;
        [SerializeField, Scene] private string sceneToLoad;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(OnStartButtonPressed);
            showLeaderboardButton.onClick.AddListener(OnShowLeaderboardButtonPressed);
            quitButton.onClick.AddListener(OnQuitButtonPressed);
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(OnStartButtonPressed);
            showLeaderboardButton.onClick.RemoveListener(OnShowLeaderboardButtonPressed);
            quitButton.onClick.RemoveListener(OnQuitButtonPressed);
        }

        private void OnStartButtonPressed()
        {
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            commandQueue.AddAndRunCommand(new LoadSceneCommand(sceneToLoad));
            // SceneManager.LoadScene("Scene_Gameplay");
        }
        
        private void OnShowLeaderboardButtonPressed()
        {
        }
        
        private void OnQuitButtonPressed()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
    }
}
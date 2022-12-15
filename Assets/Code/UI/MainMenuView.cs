using Code.Common.Commands;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public interface IMainMenuMediator
    {
        void OnCloseLeaderboardPressed();
    }

    public class MainMenuView : MonoBehaviour, IMainMenuMediator, IView
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button showLeaderboardButton;
        [SerializeField] private Button quitButton;
        [SerializeField, Scene] private string sceneToLoad;
        [SerializeField] private LeaderboardView leaderboardView;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(OnStartButtonPressed);
            showLeaderboardButton.onClick.AddListener(OnShowLeaderboardButtonPressed);
            quitButton.onClick.AddListener(OnQuitButtonPressed);
        }

        private void Start()
        {
            leaderboardView.Configure(this);
            leaderboardView.Hide();
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
        
        public void OnCloseLeaderboardPressed()
        {
            leaderboardView.Hide();
            Show();
        }
        
        private void OnShowLeaderboardButtonPressed()
        {
            Hide();
            leaderboardView.Show();
        }
        
        private void OnQuitButtonPressed()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
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
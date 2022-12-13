using Code.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameOverView : MonoBehaviour, IView
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        private InGameMenuMediator _mediator;

        public void Configure(InGameMenuMediator mediator)
        {
            _mediator = mediator;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
            backToMenuButton.onClick.AddListener(GoBackToMainMenu);

            // var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            // eventQueue.Subscribe<GameOverEvent>(OnGameOverEvent);
        }
        
        private void UnsubscribeButtons()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);
        }
        
        private void RestartGame()
        {
            _mediator.OnRestartPressed();
            
            // UnsubscribeButtons();
            //
            // var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            // var loadSceneCommand = new LoadSceneCommand(SceneManager.GetActiveScene().name);
            // commandQueue.AddAndRunCommand(loadSceneCommand);
        }

        private void GoBackToMainMenu()
        {
            _mediator.OnBackToMenuPressed();
            
            // UnsubscribeButtons();
            //
            // var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            // var loadSceneCommand = new LoadSceneCommand(sceneToLoad);
            // commandQueue.AddAndRunCommand(loadSceneCommand);
        }

        // private void OnGameOverEvent(GameOverEvent evt)
        // {
        //     Show();
        //     
        //     var currentScore = ServiceLocator.Instance.GetService<ScoreView>().CurrentScore;
        //     scoreText.text = $"Score: {currentScore}";
        // }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);

            // var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            // eventQueue.Unsubscribe<GameOverEvent>(OnGameOverEvent);
        }
        
        public void Show()
        {
            var score = ServiceLocator.Instance.GetService<ScoreView>().CurrentScore;
            scoreText.text = $"Score: {score}";
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}

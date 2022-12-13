using System;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Util;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI
{
    public class VictoryView : MonoBehaviour, IView
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
            // eventQueue.Subscribe<VictoryEvent>(OnEvent);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);

            // var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            // eventQueue.Unsubscribe<VictoryEvent>(OnEvent);
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

        // public void OnEvent(VictoryEvent signal)
        // {
        //     Show();
        // }

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
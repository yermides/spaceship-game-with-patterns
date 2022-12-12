using System;
using Code.Battle;
using Code.Common;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameOverView : MonoBehaviour
    {
        // [SerializeField] private Canvas gameOverCanvas;
        // [SerializeField] private GameFacade gameFacade;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField, Scene] private string sceneToLoad;

        private void Start()
        {
            gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartGame);
            backToMenuButton.onClick.AddListener(GoBackToMainMenu);

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<GameOverEvent>(OnGameOverEvent);
        }
        
        private void UnsubscribeButtons()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);
        }
        
        private void RestartGame()
        {
            // TODO: use menu mediator pls
            UnsubscribeButtons();
            
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            var loadSceneCommand = new LoadSceneCommand(SceneManager.GetActiveScene().name);
            commandQueue.AddAndRunCommand(loadSceneCommand);
        }

        private void GoBackToMainMenu()
        {
            UnsubscribeButtons();

            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            var loadSceneCommand = new LoadSceneCommand(sceneToLoad);
            commandQueue.AddAndRunCommand(loadSceneCommand);
        }

        private void OnGameOverEvent(GameOverEvent evt)
        {
            gameObject.SetActive(true);
            
            var currentScore = ServiceLocator.Instance.GetService<ScoreView>().CurrentScore;
            scoreText.text = $"Score: {currentScore}";
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<GameOverEvent>(OnGameOverEvent);
        }
    }
}

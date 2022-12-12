using System;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.UI
{
    public class VictoryView : MonoBehaviour, IEventReceiver<VictoryEvent>
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backToMenuButton;
        [SerializeField, Scene] private string sceneToLoad;

        private void Start()
        {
            gameObject.SetActive(false);

            restartButton.onClick.AddListener(RestartGame);
            backToMenuButton.onClick.AddListener(GoBackToMainMenu);

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<VictoryEvent>(OnEvent);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);

            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<VictoryEvent>(OnEvent);
        }

        private void UnsubscribeButtons()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);
        }

        private void RestartGame()
        {
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

        public void OnEvent(VictoryEvent signal)
        {
            gameObject.SetActive(true);
        }
    }
}
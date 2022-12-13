using System;
using Code.Common.Commands;
using Code.Common.Events;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public interface InGameMenuMediator
    {
        void OnBackToMenuPressed();
        void OnResumePressed();
        void OnRestartPressed();
    }

    // The menu mediator could be separate from the view, but it's not that big of a problem here
    public class InGameMenuView : MonoBehaviour, 
        InGameMenuMediator, 
        IEventReceiver<VictoryEvent>, 
        IEventReceiver<GameOverEvent>
    {
        [SerializeField] private Button pauseButton;
        [SerializeField] private PauseView pauseView;
        [SerializeField] private GameOverView gameOverView;
        [SerializeField] private VictoryView victoryView;
        [SerializeField, Scene] private string menuScene;
        private CommandQueue _commandQueue;

        private void Awake()
        {
            _commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            
            pauseView.Configure(this);
            gameOverView.Configure(this);
            victoryView.Configure(this);
        }

        private void Start()
        {
            HideAllMenus();
            
            pauseButton.onClick.AddListener(OnPauseButtonPressed);
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<VictoryEvent>(OnEvent);
            eventQueue.Subscribe<GameOverEvent>(OnEvent);
        }

        private void OnDestroy()
        {
            pauseButton.onClick.RemoveListener(OnPauseButtonPressed);
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<VictoryEvent>(OnEvent);
            eventQueue.Unsubscribe<GameOverEvent>(OnEvent);
        }
        
        private void OnPauseButtonPressed()
        {
            // pause the game
            _commandQueue.AddAndRunCommand(new PauseGameCommand());

            // show the pause menu
            pauseView.Show();
        }

        public void OnBackToMenuPressed()
        {
            // resume the time
            _commandQueue.AddAndRunCommand(new ResumeGameCommand());
            
            // load menu scene
            _commandQueue.AddAndRunCommand(new LoadSceneCommand(menuScene));
        }

        public void OnResumePressed()
        {
            // resume the time
            _commandQueue.AddAndRunCommand(new ResumeGameCommand());

            // hide pause menu
            pauseView.Hide();
        }

        public void OnRestartPressed()
        {
            HideAllMenus();
            
            // Resubscribe to events queued
            UnsubscribeAll();
            SubscribeAll();
            
            // resume the time
            _commandQueue.AddAndRunCommand(new ResumeGameCommand());
            
            // restart the game 
            _commandQueue.AddAndRunCommand(new RestartBattleCommand());
        }


        private void HideAllMenus()
        {
            pauseView.Hide();
            gameOverView.Hide();
            victoryView.Hide();
        }
        
        private void SubscribeAll()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<VictoryEvent>(OnEvent);
            eventQueue.Subscribe<GameOverEvent>(OnEvent);
        }

        private void UnsubscribeAll()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<VictoryEvent>(OnEvent);
            eventQueue.Unsubscribe<GameOverEvent>(OnEvent);
        }

        public void OnEvent(VictoryEvent signal)
        {
            UnsubscribeAll();
            victoryView.Show();
        }

        public void OnEvent(GameOverEvent signal)
        {
            UnsubscribeAll();
            gameOverView.Show();
        }
    }
}
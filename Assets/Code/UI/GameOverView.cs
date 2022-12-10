using System;
using Code.Battle;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
using Code.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameOverView : MonoBehaviour
    {
        public static GameOverView Instance => _instance;
        private static GameOverView _instance;

        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private GameFacade gameFacade;
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartBattle);
            gameOverCanvas.gameObject.SetActive(false);
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Subscribe<GameOverEvent>(OnGameOverEvent);
        }

        private void OnGameOverEvent(GameOverEvent evt)
        {
            gameOverCanvas.gameObject.SetActive(true);
            scoreText.text = ScoreView.Instance.CurrentScore.ToString();
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartBattle);
            
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            eventQueue.Unsubscribe<GameOverEvent>(OnGameOverEvent);
        }

        private void RestartBattle()
        {
            gameFacade.StartBattle();
            gameOverCanvas.gameObject.SetActive(false);
        }
    }
}

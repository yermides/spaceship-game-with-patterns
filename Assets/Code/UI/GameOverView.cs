using System;
using Code.Battle;
using Code.Common;
using Code.Common.Events;
using Code.Ships.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class GameOverView : MonoBehaviour, IEventObserver
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
            EventQueue.Instance.Subscribe(EventId.ShipDestroyed, this);
            EventQueue.Instance.Subscribe(EventId.GameOver, this);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartBattle);
            EventQueue.Instance.Unsubscribe(EventId.GameOver, this);
            EventQueue.Instance.Unsubscribe(EventId.ShipDestroyed, this);
        }

        private void RestartBattle()
        {
            gameFacade.StartBattle();
            gameOverCanvas.gameObject.SetActive(false);
        }

        public void Process(EventArgsBase args)
        {
            if (args.EventId == EventId.GameOver)
            {
                gameOverCanvas.gameObject.SetActive(true);
                scoreText.text = ScoreView.Instance.CurrentScore.ToString();
            }
        }
    }
}

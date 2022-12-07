using System;
using Code.Battle;
using Code.Common;
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
            restartButton.onClick.AddListener(RestartBattle);
            gameOverCanvas.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartBattle);
        }

        private void RestartBattle()
        {
            gameFacade.StartBattle();
            gameOverCanvas.gameObject.SetActive(false);
        }

        public void Show()
        {
            gameFacade.EndBattle();
            gameOverCanvas.gameObject.SetActive(true);
            scoreText.text = ScoreView.Instance.CurrentScore.ToString();
        }

        public void Process(EventArgsBase args)
        {
            if (args.EventId != EventId.ShipDestroyed) return;

            var shipDestroyedArgs = (ShipDestroyedEvent)args;

            if (shipDestroyedArgs.team != Teams.Ally) return;
            
            Show();
        }
    }
}

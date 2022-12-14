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
        }
        
        private void UnsubscribeButtons()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);
        }
        
        private void RestartGame()
        {
            _mediator.OnRestartPressed();
        }

        private void GoBackToMainMenu()
        {
            _mediator.OnBackToMenuPressed();
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveListener(RestartGame);
            backToMenuButton.onClick.RemoveListener(GoBackToMainMenu);
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

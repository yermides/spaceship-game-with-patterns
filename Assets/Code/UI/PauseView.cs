using System;
using Code.Common.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PauseView : MonoBehaviour, IView
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button backToMenuButton;
        private InGameMenuMediator _mediator;

        public void Configure(InGameMenuMediator mediator)
        {
            _mediator = mediator;
        }
        
        private void Start()
        {
            resumeButton.onClick.AddListener(OnResumePressed);
            restartButton.onClick.AddListener(OnRestartPressed);
            backToMenuButton.onClick.AddListener(OnBackToMenuPressed);
        }
        
        private void OnDestroy()
        {
            resumeButton.onClick.RemoveListener(OnResumePressed);
            restartButton.onClick.RemoveListener(OnRestartPressed);
            backToMenuButton.onClick.RemoveListener(OnBackToMenuPressed);
        }

        private void OnBackToMenuPressed()
        {
            _mediator.OnBackToMenuPressed();
        }

        private void OnRestartPressed()
        {
            _mediator.OnRestartPressed();
        }

        private void OnResumePressed()
        {
            _mediator.OnResumePressed();
            
            /*
            // resume the game command
            new ResumeGameCommand().Execute();
            // hide
            Hide();
            */
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
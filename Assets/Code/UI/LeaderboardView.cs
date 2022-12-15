using System.Collections.Generic;
using Code.Common.Score;
using Code.Util;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class LeaderboardView : MonoBehaviour, IView
    {
        [SerializeField] private Button backToMainMenuButton;
        [SerializeField] private RectTransform leaderBoardEntryContainer;
        [SerializeField] private LeaderboardEntryView entryPrefab;
        private List<LeaderboardEntryView> _leaderboardEntryViewInstances;
        private IMainMenuMediator _mediator;

        private void Awake()
        {
            _leaderboardEntryViewInstances = new List<LeaderboardEntryView>();
        }

        public void Configure(IMainMenuMediator mediator)
        {
            _mediator = mediator;
        }

        private void Start()
        {   
            backToMainMenuButton.onClick.AddListener(OnCloseLeaderboardPressed);
        }

        private void OnCloseLeaderboardPressed()
        {
            _mediator.OnCloseLeaderboardPressed();
        }

        private void OnDestroy()
        {
            backToMainMenuButton.onClick.RemoveListener(OnCloseLeaderboardPressed);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            var bestScores = ServiceLocator.Instance.GetService<IScoreSystem>().GetBestScores();

            for (var i = 0; i < bestScores.Length; i++)
            {
                var bestScore = bestScores[i];

                if (bestScore == 0) continue;

                var scoreEntryView = Instantiate(entryPrefab, leaderBoardEntryContainer);
                scoreEntryView.Configure(i + 1, bestScore);
                _leaderboardEntryViewInstances.Add(scoreEntryView);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);

            foreach (var leaderboardEntryViewInstance in _leaderboardEntryViewInstances)
            {
                Destroy(leaderboardEntryViewInstance.gameObject);
            }
            
            _leaderboardEntryViewInstances.Clear();
        }
    }
}
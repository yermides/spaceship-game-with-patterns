using System;
using UnityEngine;

namespace Code.UI.MainMenu
{
    public interface IMainMenuMediator
    {
        void OnBackToHomePressed();
        void OnShowLeaderboardPressed();
        void OnShowSettingsPressed();
    }

    public class MainMenuMediator : MonoBehaviour, IMainMenuMediator
    {
        [SerializeField] private HomeView homeView;
        [SerializeField] private LeaderboardView leaderboardView;
        [SerializeField] private SettingsView settingsView;

        private void Awake()
        {
            homeView.Configure(this);
            leaderboardView.Configure(this);
            settingsView.Configure(this);
            
            homeView.Show();
            leaderboardView.Hide();
            settingsView.Hide();
        }

        public void OnBackToHomePressed()
        {
            homeView.Show();
            leaderboardView.Hide();
            settingsView.Hide();
        }

        public void OnShowLeaderboardPressed()
        {
            homeView.Hide();
            leaderboardView.Show();
            settingsView.Hide();
        }

        public void OnShowSettingsPressed()
        {
            homeView.Hide();
            leaderboardView.Hide();
            settingsView.Show();
        }
    }
}
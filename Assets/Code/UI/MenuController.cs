using System;
using Code.Battle;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    [Obsolete]
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameFacade gameFacade;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button endGameButton;

        private void OnEnable()
        {
            startGameButton.onClick.AddListener(StartBattle);
            endGameButton.onClick.AddListener(StopBattle);
        }

        private void OnDisable()
        {
            startGameButton.onClick.RemoveListener(StartBattle);
            endGameButton.onClick.RemoveListener(StopBattle);
        }

        private void StartBattle() => gameFacade.StartBattle();
        private void StopBattle() => gameFacade.StopBattle();
    }
}
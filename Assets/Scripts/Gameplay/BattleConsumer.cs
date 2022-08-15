using System;
using UnityEngine;

namespace Gameplay
{
    public class BattleConsumer : MonoBehaviour
    {
        [SerializeField] private BattleFacade battleFacade;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                battleFacade.StartBattle();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                battleFacade.EndBattle();
            }
        }
    }
}
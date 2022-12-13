using System.Threading.Tasks;
using Code.Util;
using UnityEngine;

namespace Code.Common.Commands
{
    public class PauseGameCommand : ICommand
    {
        public Task Execute()
        {
            Time.timeScale = 0;
            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Code.Util;
using UnityEngine;

namespace Code.Common.Commands
{
    public class ResumeGameCommand : ICommand
    {
        public Task Execute()
        {
            Time.timeScale = 1;
            return Task.CompletedTask;
        }
    }
}
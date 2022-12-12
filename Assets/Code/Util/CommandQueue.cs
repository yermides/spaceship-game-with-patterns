using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Util.Extensions;

namespace Code.Util
{
    public interface ICommand
    {
        Task Execute();
    }

    public class CommandQueue
    {
        private readonly Queue<ICommand> _commandsToExecute;
        private bool _isRunningAnyCommand;

        public CommandQueue()
        {
            _commandsToExecute = new Queue<ICommand>();
        }

        public void AddAndRunCommand(ICommand command)
        {
            _commandsToExecute.Enqueue(command);
            RunNextCommand().WrapErrors();
        }

        private async Task RunNextCommand()
        {
            if (_isRunningAnyCommand) return;
            
            while (_commandsToExecute.Count > 0)
            {
                _isRunningAnyCommand = true;
                var commandToExecute = _commandsToExecute.Dequeue();
                await commandToExecute.Execute();
            }

            _isRunningAnyCommand = false;
        }
    }
}
using System.Threading.Tasks;
using Code.Common.Events;
using Code.Util;

namespace Code.Common.Commands
{
    public class RestartBattleCommand : ICommand
    {
        public async Task Execute()
        {
            var eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
            
            eventQueue.Enqueue(new RestartEvent());
            await new StopBattleCommand().Execute();
            await new StartBattleCommand().Execute();
        }
    }
}
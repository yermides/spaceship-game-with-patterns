using Code.Util;
using UnityEngine;

namespace Code.Core.Installers
{
    public class EventQueueInstaller : InstallerBehaviour
    {
        [SerializeField] private EventQueueImpl eventQueue;

        public override void Install(ServiceLocator serviceLocator)
        {
            DontDestroyOnLoad(eventQueue.gameObject);
            serviceLocator.RegisterService<IEventQueue>(eventQueue);
        }
    }
}
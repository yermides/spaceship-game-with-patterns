using Code.Util;
using UnityEngine;

namespace Code.Core.Installers
{
    public class SoundSystemInstaller : InstallerBehaviour
    {
        [SerializeField] private SoundSystemImpl soundSystemImplementation;
        
        public override void Install(ServiceLocator serviceLocator)
        {
            DontDestroyOnLoad(soundSystemImplementation);
            serviceLocator.RegisterService<ISoundSystem>(soundSystemImplementation);
        }
    }
}
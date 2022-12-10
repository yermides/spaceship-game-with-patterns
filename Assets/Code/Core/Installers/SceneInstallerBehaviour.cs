using Code.Util;
using UnityEngine;

namespace Code.Core.Installers
{
    public abstract class SceneInstallerBehaviour : MonoBehaviour
    {
        [SerializeField] private InstallerBehaviour[] installers;

        private void Awake()
        {
            InstallDependencies();
        }

        private void InstallDependencies()
        {
            foreach (var installer in installers)
            {
                installer.Install(ServiceLocator.Instance);
            }
            
            InstallAdditionalDependencies();
        }
        
        protected abstract void InstallAdditionalDependencies();
        
        private void Start()
        {
            DoStart();
        }

        protected abstract void DoStart();
    }
}
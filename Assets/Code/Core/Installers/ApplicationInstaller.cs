using Code.Util;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Core.Installers
{
    // Global installer, must be the first to execute in the game flow
    public class ApplicationInstaller : SceneInstallerBehaviour
    {
        [SerializeField, Scene] private string sceneToLoad;
        
        protected override void InstallAdditionalDependencies()
        {
        }
        
        protected override void DoStart()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
using Code.Common.Commands;
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
            CommandQueue commandQueue = new CommandQueue();
            ServiceLocator.Instance.RegisterService(commandQueue);
        }
        
        protected override void DoStart()
        {
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            var loadSceneCommand = new LoadSceneCommand(sceneToLoad);
            
            commandQueue.AddAndRunCommand(loadSceneCommand);
            // SceneManager.LoadScene(sceneToLoad);
        }
    }
}
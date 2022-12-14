using Code.Common.Commands;
using Code.Common.Score;
using Code.Core.DataStorage;
using Code.Core.Serializers;
using Code.Util;
using NaughtyAttributes;
using UnityEngine;

namespace Code.Core.Installers
{
    // Global installer, must be the first to execute in the game flow
    public class ApplicationInstaller : SceneInstallerBehaviour
    {
        [SerializeField, Scene] private string sceneToLoad;
        
        protected override void InstallAdditionalDependencies()
        {
            var commandQueue = new CommandQueue();
            ServiceLocator.Instance.RegisterService(commandQueue);
            
            var serializer = new JsonUtilityAdapter();
            var dataStorage = new PlayerPrefsAdapter(serializer);
            var scoreSystem = new ScoreSystemImpl(dataStorage);
            ServiceLocator.Instance.RegisterService<IScoreSystem>(scoreSystem);
        }
        
        protected override void DoStart()
        {
            var commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
            var loadSceneCommand = new LoadSceneCommand(sceneToLoad);
            
            commandQueue.AddAndRunCommand(loadSceneCommand);
        }
    }
}
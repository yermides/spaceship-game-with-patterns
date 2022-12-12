using System.Threading.Tasks;
using Code.Util;
using UnityEngine.SceneManagement;

namespace Code.Common.Commands
{
    public class LoadSceneCommand : ICommand
    {
        private readonly string _sceneToLoad;

        public LoadSceneCommand(string sceneToLoad)
        {
            _sceneToLoad = sceneToLoad;
        }

        public async Task Execute()
        {
            await Task.Delay(2000);
            await LoadSceneAsync(_sceneToLoad);
        }
        
        private async Task LoadSceneAsync(string scene)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(scene);

            do
            {
                await Task.Yield();
            }
            while (!loadSceneAsync.isDone);
        }
    }
}
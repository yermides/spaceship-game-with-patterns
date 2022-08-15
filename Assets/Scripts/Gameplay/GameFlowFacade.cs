using UnityEngine;
using UnityEditor;

namespace Gameplay
{
    public class GameFlowFacade : MonoBehaviour
    {
        public void ExitApplication()
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    }
}

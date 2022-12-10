using Code.Util;
using UnityEngine;

namespace Code.Core.Installers
{
    public abstract class InstallerBehaviour : MonoBehaviour
    {
        public abstract void Install(ServiceLocator serviceLocator);
    }
}
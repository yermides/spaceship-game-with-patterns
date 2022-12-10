using System;
using Code.Common;
using Code.Core.Installers;
using Code.Util;
using UnityEngine;

namespace Code.Tests
{
    [DefaultExecutionOrder(-1)]
    public class TestSceneInstaller : SceneInstallerBehaviour
    {
        protected override void InstallAdditionalDependencies()
        {
            var dispatcher = new EventDispatcherImpl();
            ServiceLocator.Instance.RegisterService<IEventDispatcher>(dispatcher);
        }

        protected override void DoStart()
        {
        }
    }
}
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility
{
    public static class GameBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            if (Object.Instantiate(Resources.Load("App")) is GameObject app)
                Object.DontDestroyOnLoad(app);
            else
                throw new ApplicationException();
        }
    }
}

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
            var app = Object.Instantiate(Resources.Load("App")) as GameObject;
            if (app == null)
                throw new ApplicationException();
            
            Object.DontDestroyOnLoad(app);
        }
    }
}

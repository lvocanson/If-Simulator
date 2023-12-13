using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public sealed class GWorld
    {
        private static readonly GWorld instance = new GWorld();
        private static WorldStates world;

        static GWorld()
        {
            world = new WorldStates();
        }

        private GWorld()
        {
        }

        public static GWorld Instance
        {
            get
            {
                return instance;
            }
        }

        public WorldStates GetWorld()
        {
            return world;
        }
    }
}

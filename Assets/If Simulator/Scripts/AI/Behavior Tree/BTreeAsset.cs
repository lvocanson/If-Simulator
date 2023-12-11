using System;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New BTree", menuName = "Scriptable Objects/Behavior Tree")]
    public class BTreeAsset : ScriptableObject
    {
        private NodeConstructor _constructor;

        /// <summary>
        /// Creates the tree from the asset.
        /// </summary>
        public Node CreateTree(BTree tree) => _constructor.Construct(tree);

        public struct NodeConstructor
        {
            public Type NodeType;
            public NodeConstructor[] Children;
            public object[] Args;

            /// <summary>
            /// Creates the node and all of its children.
            /// </summary>
            public readonly Node Construct(BTree tree)
            {
                var args = new object[2 + Children.Length + Args.Length];
                args[0] = tree;
                for (int i = 0; i < Children.Length; i++)
                {
                    args[i + 1] = Children[i].Construct(tree);
                }
                for (int i = 0; i < Args.Length; i++)
                {
                    args[i + 1 + Children.Length] = Args[i];
                }
                return (Node)Activator.CreateInstance(NodeType, args);
            }
        }
    }
}

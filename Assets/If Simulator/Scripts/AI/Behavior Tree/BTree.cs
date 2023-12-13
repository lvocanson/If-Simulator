using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New BTree", menuName = "Scriptable Objects/Behavior Tree")]
    public class BTree : ScriptableObject
    {
        [SerializeField]
        private RootNode _root;

        [field: SerializeField]
        public List<Node> AllNodes { get; private set; } = new();

        /// <summary>
        /// The tree's blackboard.
        /// </summary>
        public Blackboard Blackboard { get; } = new();

        /// <summary>
        /// State of the tree.
        /// </summary>
        public NodeState State => _root.State;

        public void Update()
        {
            if (State == NodeState.Running)
                _root.Evaluate();
        }

        public Node CreateNode(Type nodeType)
        {
            Node node = (Node)CreateInstance(nodeType);
            AllNodes.Add(node);
            node.name = nodeType.Name;
            node.Guid = Guid.NewGuid().ToString();
            node.hideFlags = HideFlags.HideInHierarchy;
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return node;
        }

        public void DeleteNode(Node node)
        {
            AllNodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

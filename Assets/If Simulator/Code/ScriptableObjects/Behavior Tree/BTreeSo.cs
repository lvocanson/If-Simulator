using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New BTree", menuName = "Scriptable Objects/Behavior Tree")]
    public class BTreeSo : ScriptableObject
    {
        [field: SerializeField, HideInInspector]
        public RootNodeSo Root { get; set; }

        /// <summary>
        /// The tree's blackboard.
        /// </summary>
        public Blackboard Blackboard => Root.Blackboard;

        /// <summary>
        /// State of the tree.
        /// </summary>
        public NodeState State => Root.State;

        /// <summary>
        /// Clones the tree. Needed for running multiple instances of the same tree.
        /// </summary>
        public BTreeSo Clone(Blackboard blackboard)
        {
            BTreeSo clone = Instantiate(this);
            clone.Root = (RootNodeSo)Root.DeepInitialize(blackboard);
            return clone;
        }

        /// <summary>
        /// Evaluates the tree if it is still running.
        /// </summary>
        public void Update()
        {
            if (State == NodeState.Running)
                Root.Evaluate();
        }

#if UNITY_EDITOR

        [field: SerializeField, HideInInspector]
        public List<NodeSo> AllNodes { get; private set; } = new();

        public NodeSo CreateNode(Type nodeType)
        {
            NodeSo node = (NodeSo)CreateInstance(nodeType);
            AllNodes.Add(node);
            node.name = nodeType.Name;
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return node;
        }

        public void DeleteNode(NodeSo node)
        {
            AllNodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Checks if the tree is valid.
        /// </summary>
        /// <param name="message">If the tree is invalid, contains a message explaining why.</param>
        /// <returns>True if the tree is valid, false otherwise.</returns>
        public bool Validate(out string message)
        {
            message = string.Empty;

            if (Root == null)
            {
                message = "The tree has no root node.";
                return false;
            }

            if (Root.Child == null)
            {
                message = "The root node has no child.";
                return false;
            }

            foreach (var node in AllNodes)
            {
                if (node is DecoratorNodeSo decorator)
                {
                    if (decorator.Child == null)
                    {
                        message = $"The decorator node {node.name} has no child.";
                        return false;
                    }
                }
                else if (node is CompositeNodeSo composite)
                {
                    if (composite.Children.Length == 0)
                    {
                        message = $"The composite node {node.name} has no children.";
                        return false;
                    }
                }
            }

            return true;
        }

#endif
    }
}

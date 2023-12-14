using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running, // The node is still running (Needs to be updated again)
        Success, // The node has finished running and succeeded
        Failure, // The node has finished running and failed
    }

    /// <summary>
    /// Base class for all nodes of a tree.
    /// </summary>
    public abstract class Node : ScriptableObject
    {
        /// <summary>
        /// The state of the last evaluation of the node.
        /// </summary>
        public NodeState State { get; protected set; } = NodeState.Running;
        private bool _entered = false;

        // TODO: Blackboard

        /// <summary>
        /// Evaluates the node.
        /// </summary>
        /// <returns>The state of the node after evaluation.</returns>
        public NodeState Evaluate()
        {
            if (!_entered)
            {
                _entered = true;
                OnEnter();
            }

            OnUpdate();

            if (State != NodeState.Running)
            {
                OnExit();
                _entered = false;
            }

            return State;
        }

        /// <summary>
        /// Called before the first update of the node.
        /// </summary>
        protected virtual void OnEnter() { }
        /// <summary>
        /// Called every frame the node is running.
        /// You should change the state of the node in this method.
        /// </summary>
        protected abstract void OnUpdate();
        /// <summary>
        /// Called after the last update of the node.
        /// </summary>
        protected virtual void OnExit() { }

        #region Editor

        [field: SerializeField, HideInInspector]
        public Vector2 GraphPosition { get; set; }

        #endregion
    }
}

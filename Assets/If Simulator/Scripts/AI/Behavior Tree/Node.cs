namespace BehaviorTree
{
    public enum NodeState
    {
        None = 0,// The node has not been evaluated yet
        Running, // The node is still running (Needs to be updated again)
        Success, // The node has finished running and succeeded
        Failure, // The node has finished running and failed
    }

    /// <summary>
    /// Base class for all nodes of a tree.
    /// </summary>
    [System.Serializable]
    public abstract class Node
    {
        /// <summary>
        /// The state of the last evaluation of the node.
        /// </summary>
        public NodeState State { get; protected set; } = NodeState.None;

        /// <summary>
        /// Gets the tree's blackboard.
        /// </summary>
        [UnityEngine.SerializeReference]
        protected Blackboard _blackboard;

        /// <summary>
        /// Evaluates the node.
        /// </summary>
        /// <returns>The state of the node after evaluation.</returns>
        public NodeState Evaluate()
        {
            if (State != NodeState.Running)
            {
                State = NodeState.Running;
                OnEnter();
            }

            OnUpdate();

            if (State != NodeState.Running)
            {
                OnExit();
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
    }
}

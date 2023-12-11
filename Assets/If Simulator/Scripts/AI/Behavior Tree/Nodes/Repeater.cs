namespace BehaviorTree
{
    /// <summary>
    /// A decorator node that evaluates and resets its child a set number of times or until it fails.
    /// </summary>
    public class Repeater : Decorator
    {
        private readonly uint _repeatCount;
        private uint _repeatIndex;

        /// <summary>
        /// Creates a repeater decorator node.
        /// </summary>
        /// <param name="repeatCount">The maximum number of times to repeat the child. 0 means repeat forever.</param>
        public Repeater(BTree tree, Node child, uint repeatCount = 0) : base(tree, child)
        {
            _repeatCount = repeatCount;
        }

        public override NodeState Evaluate()
        {
            switch (Child.Evaluate())
            {
                case NodeState.Running:
                    State = NodeState.Running;
                    return State;
                case NodeState.Success:
                    _repeatIndex++;
                    if (_repeatIndex == _repeatCount)
                    {
                        State = NodeState.Success;
                        return State;
                    }
                    else
                    {
                        Child.Reset();
                        State = NodeState.Running;
                        return State;
                    }
                default:
                    State = NodeState.Failure;
                    return State;
            }
        }

        public override void Reset()
        {
            _repeatIndex = 0;
            base.Reset();
        }
    }
}
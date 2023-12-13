namespace BehaviorTree
{
    /// <summary>
    /// A sequencer is a composite node that evaluates its children in order until one fails.
    /// </summary>
    public class Sequencer : CompositeNode
    {
        int _index = 0;

        protected override void OnEnter()
        {
            _index = 0;
        }

        protected override void OnUpdate()
        {
            State = Children[_index].Evaluate();

            if (State != NodeState.Success)
                return;

            _index++;
            if (_index == Children.Length)
                return;

            State = NodeState.Running;
        }
    }
}

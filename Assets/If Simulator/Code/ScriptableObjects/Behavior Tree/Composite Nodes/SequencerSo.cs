namespace BehaviorTree
{
    /// <summary>
    /// A sequencer is a composite node that evaluates its children in order until one fails.
    /// </summary>
    public class SequencerSo : CompositeNodeSo
    {
        int _index = 0;

        protected override void OnEnter()
        {
            _index = 0;
        }

        protected override void OnUpdate()
        {
            State = Children[_index].Evaluate();

            while (State == NodeState.Success)
            {
                if (++_index == Children.Length)
                    return;

                State = Children[_index].Evaluate();
            }
        }
    }
}

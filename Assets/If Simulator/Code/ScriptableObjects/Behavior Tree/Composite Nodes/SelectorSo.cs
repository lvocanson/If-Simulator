namespace BehaviorTree
{
    /// <summary>
    /// A selector is a composite node that evaluates its children in order until one succeeds.
    /// </summary>
    public class SelectorSo : CompositeNodeSo
    {
        int _index = 0;

        protected override void OnEnter()
        {
            _index = 0;
        }

        protected override void OnUpdate()
        {
            State = Children[_index].Evaluate();

            while (State == NodeState.Failure)
            {
                if (++_index == Children.Length)
                    return;

                State = Children[_index].Evaluate();
            }
        }
    }
}

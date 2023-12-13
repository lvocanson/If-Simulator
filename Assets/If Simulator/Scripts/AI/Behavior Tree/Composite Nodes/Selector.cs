namespace BehaviorTree
{
    /// <summary>
    /// A selector is a composite node that evaluates its children in order until one succeeds.
    /// </summary>
    public class Selector : CompositeNode
    {
        protected override void OnUpdate()
        {
            foreach (var child in Children)
            {
                State = child.Evaluate();
                if (State != NodeState.Failure)
                    return;
            }
        }
    }
}

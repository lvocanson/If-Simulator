namespace BehaviorTree
{
    /// <summary>
    /// A repeat node always returns running.
    /// </summary>
    public class Repeat : DecoratorNode
    {
        protected override void OnUpdate()
        {
            Child.Evaluate();
        }
    }
}

namespace BehaviorTree
{
    /// <summary>
    /// A repeat node always returns running.
    /// </summary>
    public class RepeatSo : DecoratorNodeSo
    {
        protected override void OnUpdate()
        {
            Child.Evaluate();
        }
    }
}

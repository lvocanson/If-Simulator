namespace BehaviorTree
{
    /// <summary>
    /// A random node chooses a random child to evaluate.
    /// </summary>
    public class Random : CompositeNode
    {
        protected override void OnUpdate()
        {
            var index = UnityEngine.Random.Range(0, Children.Length);
            State = Children[index].Evaluate();
        }
    }
}

namespace BehaviorTree
{
    /// <summary>
    /// A random node chooses a random child to evaluate.
    /// </summary>
    public class RandomSo : CompositeNodeSo
    {
        private int _index;

        protected override void OnEnter()
        {
            _index = UnityEngine.Random.Range(0, Children.Length);
        }

        protected override void OnUpdate()
        {
            State = Children[_index].Evaluate();
        }
    }
}

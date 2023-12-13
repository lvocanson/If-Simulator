using UnityEngine;

namespace BehaviorTree
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        /// <summary>
        /// The node being edited.
        /// </summary>
        public Node Node { get; }


        public NodeView(Node node)
        {
            Node = node;
            title = node.name;
            viewDataKey = node.Guid;
            SetPosition(new Rect(node.GraphPosition, Vector2.zero));
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.GraphPosition = newPos.position;
        }
    }
}

﻿using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTree
{
    public class NodeView : Node
    {
        /// <summary>
        /// The node being edited.
        /// </summary>
        public NodeSo Node { get; }

        public Port InputPort { get; } = null;
        public Port OutputPort { get; } = null;

        public NodeView(NodeSo node) : base("Assets/If Simulator/Code/Editor/Behavior Tree/NodeView.uxml")
        {
            Node = node;
            title = node.name;
            SetPosition(new Rect(node.GraphPosition, Vector2.zero));

            // Setup the ports.
            switch (node)
            {
                case ActionNodeSo:
                    AddToClassList("action");
                    InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
                    break;
                case CompositeNodeSo:
                    AddToClassList("composite");
                    InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
                    OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                    break;
                case DecoratorNodeSo:
                    AddToClassList("decorator");
                    InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(bool));
                    OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                    break;
                case RootNodeSo:
                    AddToClassList("root");
                    OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                    break;
                default:
                    throw new System.NotImplementedException("Unknown node type: " + node.GetType().Name);
            }

            if (InputPort != null)
            {
                InputPort.portName = "";
                InputPort.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(InputPort);
            }

            if (OutputPort != null)
            {
                OutputPort.portName = "";
                OutputPort.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(OutputPort);
            }
        }

        // Called when the user drags the node.
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Node.GraphPosition = newPos.position;
        }

        // Called when the user clicks on the node.
        public override void OnSelected()
        {
            // Select the node in the inspector.
            UnityEditor.Selection.activeObject = Node;
        }

        public void AddChild(NodeView child)
        {
            if (Node is CompositeNodeSo composite)
            {
                composite.Children = composite.Children.Concat(new[] { child.Node }).ToArray();
            }
            else if (Node is DecoratorNodeSo decorator)
            {
                decorator.Child = child.Node;
            }
            else if (Node is RootNodeSo root)
            {
                root.Child = child.Node;
            }
        }

        public void RemoveChild(NodeView child)
        {
            if (Node is CompositeNodeSo composite)
            {
                composite.Children = composite.Children.Where(c => c != child.Node).ToArray();
            }
            else if (Node is DecoratorNodeSo decorator)
            {
                decorator.Child = null;
            }
            else if (Node is RootNodeSo root)
            {
                root.Child = null;
            }
        }

        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        public NodeSo[] GetChildren()
        {
            if (Node is CompositeNodeSo composite)
            {
                return composite.Children;
            }
            if (Node is DecoratorNodeSo decorator)
            {
                if (decorator.Child == null)
                    return new NodeSo[0];
                return new[] { decorator.Child };
            }
            if (Node is RootNodeSo root)
            {
                if (root.Child == null)
                    return new NodeSo[0];
                return new[] { root.Child };
            }
            return new NodeSo[0];
        }
    }
}

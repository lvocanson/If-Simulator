using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTree
{

    public class BTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BTreeView, UxmlTraits> { }

        /// <summary>
        /// The tree being edited.
        /// </summary>
        BTree _tree;

        public NodeView RootNodeView => FindNodeView(_tree.Root);

        public BTreeView()
        {
            StyleSheet _styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/If Simulator/Scripts/AI/Behavior Tree/Editor/BTreeEditorWindow.uss");
            styleSheets.Add(_styleSheet);

            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        internal void Open(BTree tree)
        {
            _tree = tree;
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);

            if (_tree == null) return;
            graphViewChanged += OnGraphViewChanged;

            if (_tree.Root == null)
            {
                _tree.Root = ScriptableObject.CreateInstance<RootNode>();
                _tree.Root.name = "Root";
                _tree.Root.hideFlags = HideFlags.HideInHierarchy;
                AssetDatabase.AddObjectToAsset(_tree.Root, _tree);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            var rootAndNodes = _tree.AllNodes.Prepend(_tree.Root);

            // Populate the graph view with its nodes.
            foreach (var node in rootAndNodes)
            {
                CreateNodeView(node);
            }

            // Add the connections between the nodes.
            foreach (var node in rootAndNodes)
            {
                var parentView = FindNodeView(node);
                Node[] children = parentView.GetChildren();
                for (int i = 0; i < children.Length; i++)
                {
                    Node child = children[i];
                    var childView = FindNodeView(child);
                    Edge edge = parentView.OutputPort.ConnectTo(childView.InputPort);
                    AddElement(edge);
                }
            }
        }

        /// <summary>
        /// Returns the view of the given node, or null if it doesn't exist.
        /// </summary>
        private NodeView FindNodeView(Node node) => graphElements.ToList().OfType<NodeView>().FirstOrDefault(view => view.Node == node);

        /// <summary>
        /// Called when the user tries to create a new connection between two ports.
        /// </summary>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(port => port.direction != startPort.direction && port.node != startPort.node).ToList();
        }

        /// <summary>
        /// Called when the graph view is changed.
        /// </summary>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var element in graphViewChange.elementsToRemove)
                {
                    // Delete the node from the tree.
                    if (element is NodeView nodeView)
                    {
                        _tree.DeleteNode(nodeView.Node);
                    }

                    // Delete a connection between two nodes.
                    else if (element is Edge edge)
                    {
                        if (edge.input.node is NodeView childView && edge.output.node is NodeView parentView)
                        {
                            parentView.RemoveChild(childView);
                        }
                    }
                }
            }

            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    // Create a connection between two nodes.
                    if (edge.input.node is NodeView child && edge.output.node is NodeView parent)
                    {
                        parent.AddChild(child);
                    }
                }
            }

            return graphViewChange;
        }

        /// <summary>
        /// Called when the user right clicks on the graph view.
        /// </summary>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_tree == null)
            {
                evt.menu.AppendAction("No tree open", (a) => { });
                return;
            }

            // If the user right clicks on a node, show the menu to delete it.
            if (evt.target is NodeView nodeView)
            {
                if (nodeView.Node != _tree.Root)
                    evt.menu.AppendAction("Delete", (a) => DeleteElements(new[] { nodeView }));

                return;
            }

            // If the user right clicks on the background, show the menu to create a new node.
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            Vector2 position = evt.mousePosition - parent.layout.position;
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Action/{type.Name}", (a) => CreateNode(type, position));
            }
            types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Composite/{type.Name}", (a) => CreateNode(type, position));
            }
            types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"Decorator/{type.Name}", (a) => CreateNode(type, position));
            }
        }

        private void CreateNode(Type nodeType, Vector2 position = default)
        {
            Node node = _tree.CreateNode(nodeType);
            node.GraphPosition = position;
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new(node);
            AddElement(nodeView);
        }

        public void DuplicateSelection()
        {
            if (selection.Count == 0) return;
            var nodes = selection.OfType<NodeView>();
            ClearSelection();

            foreach (var node in nodes)
            {
                CreateNode(node.Node.GetType(), node.GetPosition().position + Vector2.right * 20);
                selection.Add(graphElements.Last());
            }
        }
    }
}

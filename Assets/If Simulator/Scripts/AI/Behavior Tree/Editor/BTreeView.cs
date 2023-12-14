using System;
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
            graphViewChanged += OnGraphViewChanged;
            _tree.AllNodes.ForEach(CreateNodeView);
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
                    if (element is NodeView nodeView)
                    {
                        _tree.DeleteNode(nodeView.Node);
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
            foreach (var node in nodes)
            {
                CreateNode(node.Node.GetType(), node.GetPosition().position + Vector2.right * 20);
            }
        }
    }
}

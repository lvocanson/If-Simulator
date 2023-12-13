using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BehaviorTree
{
    [CreateAssetMenu(fileName = "New BTree", menuName = "Scriptable Objects/Behavior Tree")]
    public class BTreeAsset : ScriptableObject
    {
        /// <summary>
        /// Syntax:
        /// {ParentName{Child1Name,Child2Name{ChildAgain},Child3Name}}
        /// </summary>
        [SerializeField]
        private string _serializedTree;
        private BTree _tree; // Temporary variable to pass the tree around while deserializing.

        /// <summary>
        /// Creates the tree from the asset.
        /// </summary>
        public Node CreateTree(BTree tree)
        {
            _tree = tree;
            return Deserialize(_serializedTree);
        }

        // Recursive function to deserialize the tree.
        private Node Deserialize(string serializedTree)
        {
            if (serializedTree.Length == 0)
            {
                Debug.LogError("Empty tree.", this);
                return null;
            }

            int index = serializedTree.IndexOf('{');
            if (index == -1)
                // No children, create leaf node.
                return Node.Create(serializedTree, _tree);

            // Deserialize all children. (Regex splits the string by commas, but ignores commas inside curly brackets)
            MatchCollection children = Regex.Matches(serializedTree[(index + 1)..^1], @"(?:[^,{}]+|{[^}]*})+");
            var siblingNodes = children.Select(child => Deserialize(child.Value));

            // Create parent node.
            string name = serializedTree[..index];
            return Node.Create(name, _tree, siblingNodes.ToArray());
        }
    }
}

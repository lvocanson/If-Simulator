using System.Collections.Generic;

namespace BehaviorTree
{
    /// <summary>
    /// A blackboard is a dictionary that can be used to store values that are shared between nodes.
    /// </summary>
    public class Blackboard
    {
        private readonly Dictionary<string, object> _dict = new();

        /// <summary>
        /// Reads a value from the blackboard, and casts it to the specified type.
        /// </summary>
        public void Read<T>(string key, out T value)
        {
            value = (T)_dict[key];
        }

        /// <summary>
        /// Writes a value to the blackboard.
        /// </summary>
        public void Write(string key, object value)
        {
            _dict[key] = value;
        }

        /// <summary>
        /// Checks if the blackboard contains a value with the specified key.
        /// </summary>
        public bool Contains(string key)
        {
            return _dict.ContainsKey(key);
        }

        /// <summary>
        /// Removes a value from the blackboard.
        /// </summary>
        public void Remove(string key)
        {
            _dict.Remove(key);
        }

        /// <summary>
        /// Clears the blackboard.
        /// </summary>
        public void Clear()
        {
            _dict.Clear();
        }
    }
}

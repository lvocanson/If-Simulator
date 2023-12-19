using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState = null;

        private BaseState _currentState = null;
        public BaseState CurrentState
        {
            get => _currentState;
            set => ChangeState(value);
        }

        private void Start()
        {
            if (_initialState != null)
                ChangeState(_initialState);
        }

        /// <summary>
        /// Changes the current state to the given state.
        /// </summary>
        /// <param name="newState">The state to change to.</param>
        /// <param name="args">Arguments forwarded to the state's Enter method.</param>
        public void ChangeState(BaseState newState, params object[] args)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = newState;

            if (_currentState != null)
                _currentState.Enter(this, args);
            else
                enabled = false;
        }

        private void OnDestroy()
        {
            if (_currentState != null)
                _currentState.Exit();
        }
    }
}

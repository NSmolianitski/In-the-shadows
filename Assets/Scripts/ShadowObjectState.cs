using InTheShadows.Managers;
using InTheShadows.UserInput;
using UnityEngine;

namespace InTheShadows
{
    public class ShadowObjectState : MonoBehaviour
    {
        public enum State
        {
            HorizontalRotation,
            VerticalRotation,
            Moving
        }
        
        [SerializeField] private bool verticalRotationEnabled = false;
        [SerializeField] private bool movementEnabled = false;
        
        public State CurrentState { get; private set; } = State.HorizontalRotation;

        private void Update()
        {
            if (GameManager.Instance.IsUserInputEnabled)
                UpdateState();
        }

        private void UpdateState()
        {
            if (verticalRotationEnabled && InputManager.IsVerticalRotationButtonPressed)
                CurrentState = State.VerticalRotation;
            else if (movementEnabled && InputManager.IsMovementButtonPressed)
                CurrentState = State.Moving;
            else
                CurrentState = State.HorizontalRotation;
        }
    }
}
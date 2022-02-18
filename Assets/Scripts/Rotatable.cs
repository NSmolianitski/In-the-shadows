using System;
using System.Collections;
using InTheShadows.Managers;
using InTheShadows.UserInput;
using UnityEngine;

namespace InTheShadows
{
    public class Rotatable : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float smoothSpeed = 1.2f;

        private ShadowObjectState _state;

        float _verticalRotation = 0;
        float _horizontalRotation = 0;

        private void Awake()
        {
            _state = GetComponent<ShadowObjectState>();
        }

        private void OnMouseDrag()
        {
            if (!GameManager.Instance.IsUserInputEnabled)
                return;
            
            SetRotationDirection();
                
            transform.Rotate(_verticalRotation, _horizontalRotation, 0f, Space.World);
        }
        
        private void SetRotationDirection()
        {
            ResetDirections();
            
            if (_state.CurrentState == ShadowObjectState.State.VerticalRotation)
                _verticalRotation = GetVerticalRotation();
            else if (_state.CurrentState == ShadowObjectState.State.HorizontalRotation)
                _horizontalRotation = GetHorizontalRotation();
        }

        private void ResetDirections()
        {
            _verticalRotation = 0;
            _horizontalRotation = 0;
        }
        
        private float GetVerticalRotation()
        {
            return (InputManager.VerticalAxis) * Time.deltaTime * rotationSpeed;
        }

        private float GetHorizontalRotation()
        {
            return (-InputManager.HorizontalAxis) * Time.deltaTime * rotationSpeed;
        }

        public void LaunchMovingToWinPosition(bool isLinkedObject, Vector3 winPosition, Quaternion winRotation)
        {
            if (isLinkedObject)
                StartCoroutine(MoveToWinPosition(winPosition));
            StartCoroutine(RotateToWinRotation(winRotation));
        }

        private IEnumerator MoveToWinPosition(Vector3 winPosition)
        {
            while (!IsAtWinPosition(winPosition))
            {
                float smoothTime = Time.deltaTime * smoothSpeed;
                
                transform.position = Vector3.Lerp(transform.position, winPosition,
                    smoothTime);
                yield return null;
            }
        }
        
        private IEnumerator RotateToWinRotation(Quaternion winRotation)
        {
            while (!IsAtWinRotation(winRotation))
            {
                float smoothTime = Time.deltaTime * smoothSpeed;
                
                transform.rotation = Quaternion.Lerp(transform.rotation, winRotation,
                    smoothTime);
                yield return null;
            }
        }

        private bool IsAtWinPosition(Vector3 winPosition)
        {
            bool atWinPosition = Vector3.Distance(transform.position, winPosition) <= 0.01f;

            return atWinPosition;
        }

        private bool IsAtWinRotation(Quaternion winRotation)
        {
            bool atWinRotation = Quaternion.Angle(transform.rotation, winRotation) <= 0.01f;

            return atWinRotation;
        }
    }
}
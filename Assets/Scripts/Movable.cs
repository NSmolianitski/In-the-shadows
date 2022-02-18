using System;
using InTheShadows.Managers;
using InTheShadows.UserInput;
using UnityEngine;

namespace InTheShadows
{
    public class Movable : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private float maxDistanceFromAreaCenter = 5f;
        [SerializeField] private Transform movableArea;
        
        private ShadowObjectState _state;
        private Camera _cam;

        private Vector3 _mousePosOffset;
        private float _zPos;

        private float _defaultZPosition;

        private void Awake()
        {
            _state = GetComponent<ShadowObjectState>();
            _defaultZPosition = transform.position.z;
        }

        private void Start()
        {
            _cam = MainCamera.Instance.MovementCamera;
            MainCamera.Instance.ResetMovementCamera(transform.position + Vector3.back * movementSpeed);
            _zPos = _cam.WorldToScreenPoint(transform.position).z;
        }

        private void OnMouseDown()
        {
            _mousePosOffset = transform.position - GetMousePosition();
        }

        private void OnMouseDrag()
        {
            if (!GameManager.Instance.IsUserInputEnabled || _state.CurrentState != ShadowObjectState.State.Moving)
                return;
            
            Move();
        }

        private void Move()
        {
            Vector3 newPosition = GetMousePosition() + _mousePosOffset;
            if (Vector3.Distance(newPosition, movableArea.position) > maxDistanceFromAreaCenter)
            {
                newPosition = (newPosition - movableArea.position).normalized;

                Vector3 a = movableArea.position;
                a.z = _defaultZPosition;
                
                transform.position = a + maxDistanceFromAreaCenter * newPosition;
                return;
            }
            
            transform.position = newPosition;
        }

        private Vector3 GetMousePosition()
        {
            Vector3 mousePosition = new Vector3(InputManager.MousePosition.x, InputManager.MousePosition.y, _zPos);
            Vector3 worldMousePosition = _cam.ScreenToWorldPoint(mousePosition);
            return worldMousePosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(movableArea.position, maxDistanceFromAreaCenter);
        }
    }
}
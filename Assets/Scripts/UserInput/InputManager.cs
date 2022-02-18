using InTheShadows.Managers;
using UnityEngine;

namespace InTheShadows.UserInput
{
    public static class InputManager
    {
        public static bool IsBackButtonPressed { get; set; }
        public static bool IsVerticalRotationButtonPressed { get; private set; }
        public static bool IsMovementButtonPressed { get; private set; }
        public static float HorizontalAxis { get; private set; }
        public static float VerticalAxis { get; private set; }
        public static Vector3 MousePosition { get; private set; }

        public static void ReadInput()
        {
            IsBackButtonPressed = Input.GetKeyDown(ControlKeys.BackKey);
            IsVerticalRotationButtonPressed = Input.GetKey(ControlKeys.VerticalRotationKey);
            IsMovementButtonPressed = Input.GetKey(ControlKeys.MovementKey);
            
            HorizontalAxis = Input.GetAxisRaw("Mouse X");
            VerticalAxis = Input.GetAxisRaw("Mouse Y");
            MousePosition = Input.mousePosition;
        }

        public static void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.EnableUnclickableLayer();
        }
        
        public static void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameManager.Instance.DisableUnclickableLayer();
        }
    }
}
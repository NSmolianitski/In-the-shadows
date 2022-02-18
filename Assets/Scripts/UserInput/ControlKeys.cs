using UnityEngine;

namespace InTheShadows.UserInput
{
    public static class ControlKeys
    {
        public static KeyCode BackKey { get; private set; } = KeyCode.Escape;
        public static KeyCode VerticalRotationKey { get; private set; } = KeyCode.LeftControl;
        public static KeyCode MovementKey { get; private set; } = KeyCode.LeftShift;
    }
}
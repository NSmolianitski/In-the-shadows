using UnityEngine;

namespace InTheShadows
{
    public class WinChecker : MonoBehaviour
    {
        [SerializeField] private Vector3[] winRotations;
        [SerializeField] private Vector3 winLinkedVector;
        [SerializeField] private float rotationTolerance = 10f;
        [SerializeField] private float linkedDistanceTolerance = 10f;
        [SerializeField] private Transform linkedObject;

        public Vector3 LinkedWinPosition { get; set; }
        public bool IsInWinPosition { get; private set; } = false;
        public bool IsLinkedObject { get; set; } = false;

        private WinChecker _linkedObjectWinChecker;

        private void Start()
        {
            if (linkedObject)
            {
                _linkedObjectWinChecker = linkedObject.GetComponent<WinChecker>();
                _linkedObjectWinChecker.IsLinkedObject = true;
                UpdateLinkedObjectWinPosition();
            }
        }

        private void OnDrawGizmos()
        {
            if (IsLinkedObject)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(LinkedWinPosition, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }

        private void UpdateLinkedObjectWinPosition()
        {
            _linkedObjectWinChecker.LinkedWinPosition = transform.position + winLinkedVector;
        }

        private void Update()
        {
            if (linkedObject)
                UpdateLinkedObjectWinPosition();
            
            if (GetClosestWinRotation() != null && IsCloseToLinkedObject())
            {
                IsInWinPosition = true;
            }
            else
                IsInWinPosition = false;
        }

        public void LaunchWinAnimation()
        {
            Vector3? closestRotation = GetClosestWinRotation();
            Vector3 checkedClosestRotation;
            if (closestRotation == null)
                checkedClosestRotation = winRotations[0];
            else
                checkedClosestRotation = (Vector3) closestRotation;
            
            GetComponent<Rotatable>().LaunchMovingToWinPosition(IsLinkedObject, LinkedWinPosition ,
                Quaternion.Euler(checkedClosestRotation));
        }

        private bool IsCloseToLinkedObject()
        {
            if (!IsLinkedObject)
                return true;
            
            if (Vector3.Distance(transform.position, LinkedWinPosition) <= linkedDistanceTolerance)
                return true;
            
            return false;
        }

        private Vector3? GetClosestWinRotation()
        {
            foreach (var rotation in winRotations)
            {
                float rotationDistance = Quaternion.Angle(transform.rotation, Quaternion.Euler(rotation));
                if (rotationDistance <= rotationTolerance)
                    return rotation;
            }

            return null;
        }
    }
}
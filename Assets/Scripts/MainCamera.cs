using UnityEngine;

public class MainCamera : MonoBehaviour
{
    #region Singleton

        public static MainCamera Instance { get; private set; } 
        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

    #endregion

    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera movementCamera;
    [SerializeField] private bool disableUICameraOnStart = true;
    [SerializeField] private LayerMask movableAreaMask;
    
    public Camera UICamera => uiCamera;
    public Camera MovementCamera => movementCamera;

    private void Start()
    {
        if (disableUICameraOnStart)
            DisableUICamera();
        // GetComponent<Camera>().eventMask = movableAreaMask;
    }

    public void ResetMovementCamera(Vector3 position)
    {
        movementCamera.transform.position = position;
    }
    
    public void EnableUICamera()
    {
        uiCamera.gameObject.SetActive(true);
    }
        
    public void DisableUICamera()
    {
        uiCamera.gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Mouse : MonoBehaviour
{
    public enum State
    {
        InMovableArea,
        OutOfMovableArea
    }

    public static State _state;
    
    private void OnMouseEnter()
    {
        Debug.Log("ENTER");
        _state = State.InMovableArea;
    }
    
    private void OnMouseExit()
    {
        Debug.Log("EXIT");
        _state = State.OutOfMovableArea;
    }
}

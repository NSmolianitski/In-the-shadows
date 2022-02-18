using UnityEngine;

public class TipWindow : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Start = Animator.StringToHash("Start");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetTrigger(Start);
    }
}

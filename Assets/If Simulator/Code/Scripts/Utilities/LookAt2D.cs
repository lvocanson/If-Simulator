using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    enum CardinalDirection
    {
        North,
        East,
        South,
        West
    }

    [SerializeField] private Transform _target;
    [SerializeField] private CardinalDirection _cardinalDirection = CardinalDirection.East;

    public Transform Target
    {
        get => _target;
        set
        {
            _target = value;
            enabled = _target != null;
        }
    }

    private void Awake()
    {
        enabled = _target != null;
    }

    private void Update()
    {
        switch (_cardinalDirection)
        {
            case CardinalDirection.North:
                transform.up = _target.position - transform.position;
                break;
            case CardinalDirection.East:
                transform.right = _target.position - transform.position;
                break;
            case CardinalDirection.South:
                transform.up = transform.position - _target.position;
                break;
            case CardinalDirection.West:
                transform.right = transform.position - _target.position;
                break;
        }
    }
}

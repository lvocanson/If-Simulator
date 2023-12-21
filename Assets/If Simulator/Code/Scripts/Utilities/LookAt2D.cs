using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0, 360)] private float _angleOffset;
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
        var direction = _target.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + _angleOffset, Vector3.forward);
    }
}

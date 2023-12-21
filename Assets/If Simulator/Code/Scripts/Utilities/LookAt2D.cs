using UnityEngine;

public class LookAt2D : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0, 360)] private float _angleOffset;
    [SerializeField] private float _speed = 100;
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
        var targetDirection = _target.position - transform.position;
        var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle + _angleOffset, _speed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}

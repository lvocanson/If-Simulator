using UnityEngine;

public class NebulaForearm : MonoBehaviour
{
    [SerializeField] private Transform _shoulder;
    [SerializeField] private Transform _elbow;
    [SerializeField, Tooltip("Degrees per second")]
    private float _shoulderSpeed;
    [SerializeField, Tooltip("Degrees per second")]
    private float _elbowSpeed;

    public float ShoulderAngle
    {
        get => _shoulder.localEulerAngles.z;
        set => _targetShoulderAngle = value % 360f;
    }
    public float ElbowAngle
    {
        get => _elbow.localEulerAngles.z;
        set => _targetElbowAngle = value % 360f;
    }

    private float _targetShoulderAngle;
    private float _targetElbowAngle;

    private void Update()
    {
        _shoulder.localEulerAngles = new(0, 0, Mathf.MoveTowardsAngle(ShoulderAngle, _targetShoulderAngle, _shoulderSpeed * Time.deltaTime));
        _elbow.localEulerAngles = new(0, 0, Mathf.MoveTowardsAngle(ElbowAngle, _targetElbowAngle, _elbowSpeed * Time.deltaTime));
    }
}

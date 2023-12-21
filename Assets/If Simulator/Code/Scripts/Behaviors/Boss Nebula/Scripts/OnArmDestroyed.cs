using UnityEngine;

public class OnArmDestroyed : MonoBehaviour
{
    [SerializeField] BehaviorTree.BTreeRunner _btRunner;

    private void OnDestroy()
    {
        _btRunner.Blackboard.Write("ArmDestroyed", true);
    }
}

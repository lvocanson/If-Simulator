using FiniteStateMachine;
using UnityEngine;

namespace Behaviors
{
    public class TurretSeek : BaseState
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private SpriteRenderer _laserSight;
        [SerializeField] private LayerMask _layerMask;
        
        private void Update()
        {
            _root.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.up, Mathf.Infinity, _layerMask);
            _laserSight.size = hit.collider ? new Vector2(_laserSight.size.x, hit.distance) : new Vector2(_laserSight.size.x, 50);
        }
    }
}
using UnityEngine;

public class SingleDamagePopup : DamagePopup
{
    public static GameObject Create(Vector3 position, int damage)
    {
        SingleDamagePopup damagePopup = Instantiate(LevelContext.Instance.PrefabsHolder.DamagePopupPrefab, position, Quaternion.identity).GetComponent<SingleDamagePopup>();
        damagePopup.Setup(damage);
        return damagePopup.gameObject;
    }
    
    [Header("Scale")]
    [SerializeField, Range(0, 1)] private float _increaseScaleTimeThreshold = 0.5f;
    [SerializeField] private float _increaseScaleSpeed = 1f;
    [SerializeField] private float _decreaseScaleSpeed = 1f;
    
    [Header("Position")]
    [SerializeField] private Vector2 _randomOffsetX;
    [SerializeField] private Vector2 _randomOffsetY;
    
    private static int _sortingOrder;

    private Vector3 _moveVector;
    private float _increaseScaleStep;
    private float _decreaseScaleStep;
    
    
    public override void Setup(int damageAmount)
    {
        base.Setup(damageAmount);
        
        _moveVector = new Vector3(0.7f, 1) * 5f;
        transform.position += new Vector3(Random.Range(_randomOffsetX.x, _randomOffsetX.y), Random.Range(_randomOffsetY.x, _randomOffsetY.y), 0);
        
        _increaseScaleStep = _increaseScaleSpeed / _duration * _increaseScaleTimeThreshold;
        _decreaseScaleStep = _decreaseScaleSpeed / _duration * (1 - _increaseScaleTimeThreshold);
        
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
    }
    
    private void Update()
    {
        base.Update();
        
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= _moveVector * (8f * Time.deltaTime);

        if (_disappearTimer > _duration * _increaseScaleTimeThreshold)
        {
            transform.localScale += Vector3.one * (_increaseScaleStep * Time.deltaTime);
        }
        else
        {
            transform.localScale -= Vector3.one * (_decreaseScaleStep * Time.deltaTime);
        }
    }
}

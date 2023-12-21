using UnityEngine;

public class TotalDamagePopup : DamagePopup
{
    public static GameObject Create(Transform entity, Vector3 offset, int damage, Color color)
    {
        TotalDamagePopup damagePopup = Instantiate(LevelContext.Instance.PrefabsHolder.TotalDamagePopupPrefab, entity.position + offset, Quaternion.identity).GetComponent<TotalDamagePopup>();
        damagePopup.Setup(damage, color);
        damagePopup._entity = entity;
        damagePopup._offset = offset;
        return damagePopup.gameObject;
    }
    
    private Transform _entity;
    private Vector3 _offset;
    private float _totalDamage;
    
    public override void Setup(int damageAmount, Color color)
    {
        base.Setup(damageAmount, color);
        
        _totalDamage = damageAmount;
    }
    
    public void UpdateDamage(int damageAmount)
    {
        _totalDamage += damageAmount;
        _disappearTimer = _duration;
        _textMesh.SetText(_totalDamage.ToString());
    }

    protected override void Update()
    {
        base.Update();
        
        if (_entity != null)
        {
            transform.position = _entity.position + _offset;
        }
    }
}

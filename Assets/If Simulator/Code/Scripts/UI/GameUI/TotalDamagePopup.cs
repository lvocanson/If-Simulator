using UnityEngine;

public class TotalDamagePopup : DamagePopup
{
    public static GameObject Create(Vector3 position, int damage)
    {
        TotalDamagePopup damagePopup = Instantiate(LevelContext.Instance.PrefabsHolder.TotalDamagePopupPrefab, position, Quaternion.identity).GetComponent<TotalDamagePopup>();
        damagePopup.Setup(damage);
        return damagePopup.gameObject;
    }
    
    private float _totalDamage;
    
    public override void Setup(int damageAmount)
    {
        base.Setup(damageAmount);
        
        _totalDamage = damageAmount;
    }
    
    public void UpdateDamage(int damageAmount)
    {
        _totalDamage += damageAmount;
        _disappearTimer = _duration;
        _textMesh.SetText(_totalDamage.ToString());
    }
}

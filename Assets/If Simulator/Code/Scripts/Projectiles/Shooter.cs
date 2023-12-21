using UnityEngine;

public class Shooter : Ability.AbilityPrimaryShoot
{
    public bool Shoot()
    {
        return TryActivate();
    }
}

using UnityEngine;

namespace Utility
{
    public static class TransformUtility
    {
        public static float AngleBetweenTwoPoints(Vector2 a, Vector2 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
        }
    }
}

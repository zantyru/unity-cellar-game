using UnityEngine;


namespace CellarGame
{
    public static class QuaternionExtensions
    {
        public static Quaternion ClampRotationAroundXAxis(this Quaternion q, float minAngleDegree, float maxAngleDegree)
        {
            q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;
			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
			angleX = Mathf.Clamp(angleX, minAngleDegree, maxAngleDegree);
			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
			return q;
        }
    }
}
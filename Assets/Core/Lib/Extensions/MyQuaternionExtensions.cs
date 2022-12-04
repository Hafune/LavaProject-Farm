using UnityEngine;

namespace Lib
{
    public static class MyQuaternionExtensions
    {
        public static Quaternion RotateTowards(this Quaternion from, Vector3 to, float maxDegreesDelta) =>
            Quaternion.RotateTowards(from, Quaternion.LookRotation(to), maxDegreesDelta);
    }
}
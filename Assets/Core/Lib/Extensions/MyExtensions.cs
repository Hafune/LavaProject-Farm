using System;
using System.Linq;
using UnityEngine;

namespace Lib
{
    public static class MyExtensions
    {
        public static void RepeatTimes(this int count, Action callback)
        {
            for (int i = 0; i < count; i++)
                callback.Invoke();
        }

        public static int Sign(this int value) => Math.Sign(value);

        public static int Sign(this float value) => Math.Sign(value);

        public static float RoundToDecimal(this float value, int accuracy)
        {
            var acc = (float)Math.Pow(10, accuracy);
            return (int)(value * acc) / acc;
        }

        public static bool ContainsPoint(this BoxCollider box, Vector3 point, Vector3? sizeMultiply = null)
        {
            var m = sizeMultiply ?? new Vector3(1, 1, 1);
            point = box.transform.InverseTransformPoint(point) - box.center;
            float halfX = box.size.x * 0.5f * m.x;
            float halfY = box.size.y * 0.5f * m.y;
            float halfZ = box.size.z * 0.5f * m.z;
            return point.x < halfX && point.x > -halfX &&
                   point.y < halfY && point.y > -halfY &&
                   point.z < halfZ && point.z > -halfZ;
        }

        public static (Vector3 point0, Vector3 point1, float rdaius) TransformCapsuleValues(this CapsuleCollider col)
        {
            var direction = new Vector3 { [col.direction] = 1 };
            var offset = col.height / 2 - col.radius;
            var localPoint0 = col.center - direction * offset;
            var localPoint1 = col.center + direction * offset;

            var point0 = col.transform.TransformPoint(localPoint0);
            var point1 = col.transform.TransformPoint(localPoint1);

            var rawRadius = col.transform.TransformVector(col.radius, col.radius, col.radius);
            var radius = Enumerable.Range(0, 3).Select(xyz => xyz == col.direction ? 0 : rawRadius[xyz])
                .Select(Mathf.Abs).Max();

            return (point0, point1, radius);
        }

        public static int OverlapCapsuleNonAlloc(this CapsuleCollider col, Collider[] _hitBuffer)
        {
            var (point0, point1, radius) = col.TransformCapsuleValues();
            return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, _hitBuffer);
        }

        public static bool Raycast(this Camera camera, Vector3 position, out RaycastHit info) =>
            Physics.Raycast(camera.ScreenPointToRay(position), out info);

        public static AnimationClip GetAnimationClipByName(this Animator animator, string name) =>
            animator.runtimeAnimatorController.animationClips.First(clip => clip.name == name);
    }
}
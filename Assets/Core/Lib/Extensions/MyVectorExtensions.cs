using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Lib
{
    public static class MyVectorExtensions
    {
        public static Vector3 Multiply(this Vector3 v1, Vector3 v2) =>
            new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);

        public static Vector3 Divide(this Vector3 v1, Vector3 v2) =>
            new Vector3(v1.x / v2.x, v1.y / v2.y, v1.z / v2.z);

        public static float ModuleDifference(this Vector3 v0, Vector3 v1)
        {
            var diff = v0 - v1;
            return Mathf.Abs(diff.x) + Mathf.Abs(diff.y) + Mathf.Abs(diff.z);
        }

        public static Vector3 Copy(this Vector3 vector, float? x = null, float? y = null, float? z = null) =>
            new Vector3(
                x ?? vector.x,
                y ?? vector.y,
                z ?? vector.z
            );

        public static Vector2 ToVector2XZ(this Vector3 vector) => new Vector3(vector.x, vector.z);


        public static Vector2 Copy(this Vector2 vector, float? x = null, float? y = null) =>
            new Vector2(
                x ?? vector.x,
                y ?? vector.y
            );

        public static Vector2 RotatedBy(this Vector2 v, float angle)
        {
            angle *= Mathf.Deg2Rad;
            var ca = Math.Cos(angle);
            var sa = Math.Sin(angle);
            var rx = v.x * ca - v.y * sa;

            return new Vector2((float) rx, (float) (v.x * sa + v.y * ca));
        }

        public static Vector2 RotatedBySignedAngle(this Vector2 v, Vector2 first, Vector2 second) => v.RotatedBy(
            Vector2.SignedAngle(first, second));

        public static Vector2 ReflectedBy(this Vector2 vector, Vector2 normal)
        {
            var reflect = Vector2.Reflect(vector.normalized, normal);
            return vector.RotatedBy(Vector2.SignedAngle(vector.normalized, reflect));
        }

        public static Vector2 RotatedToward(this Vector2 from, Vector2 to, float a)
        {
            var angle = Vector2.SignedAngle(from, to);
            a = Mathf.Min(Mathf.Abs(angle), a) * a.Sign();
            return angle is > 0 and < 180 ? from.RotatedBy(a) : from.RotatedBy(-a);
        }

        public static Vector2 ReflectedAlong(this Vector2 vector, Vector2 normal) =>
            (vector + vector.ReflectedBy(normal)) / 2;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class BCH_PATHS
    {
        public static string PLACES_PATH = "Assets/InkDatas/Resources/Places/";
    }
    public static class UnityOperations
    {
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            Component copy = destination.AddComponent(type);
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }
    }
    public static class Ease
    {


        // Easing constants.
        private static float PI = Mathf.PI;
        private static float PI2 = PI / 2.0f;
        private static float EL = 2 * PI / 0.45f;
        private static float B1 = 1.0f / 2.75f;
        private static float B2 = 2.0f / 2.75f;
        private static float B3 = 1.5f / 2.75f;
        private static float B4 = 2.5f / 2.75f;
        private static float B5 = 2.25f / 2.75f;
        private static float B6 = 2.625f / 2.75f;

        /** Linear easing */
        public static float linear(float t)
        {
            return t;
        }

        /** Quadratic in. */
        public static float quadIn(float t)
        {
            return t * t;
        }

        /** Quadratic out. */
        public static float quadOut(float t)
        {
            return -t * (t - 2);
        }

        /** Quadratic in and out. */
        public static float quadInOut(float t)
        {
            return t <= .5 ? t * t * 2 : 1 - (--t) * t * 2;
        }

        /** Cubic in. */
        public static float cubeIn(float t)
        {
            return t * t * t;
        }

        /** Cubic out. */
        public static float cubeOut(float t)
        {
            return 1 + (--t) * t * t;
        }

        /** Cubic in and out. */
        public static float cubeInOut(float t)
        {
            return t <= .5 ? t * t * t * 4 : 1 + (--t) * t * t * 4;
        }

        /** Quart in. */
        public static float quartIn(float t)
        {
            return t * t * t * t;
        }

        /** Quart out. */
        public static float quartOut(float t)
        {
            return 1 - (t -= 1) * t * t * t;
        }

        /** Quart in and out. */
        public static float quartInOut(float t)
        {
            return t <= .5f ? t * t * t * t * 8 : (1 - (t = t * 2 - 2) * t * t * t) / 2 + .5f;
        }

        /** Quint in. */
        public static float quintIn(float t)
        {
            return t * t * t * t * t;
        }

        /** Quint out. */
        public static float quintOut(float t)
        {
            return (t = t - 1) * t * t * t * t + 1;
        }

        /** Quint in and out. */
        public static float quintInOut(float t)
        {
            return ((t *= 2) < 1) ? (t * t * t * t * t) / 2 : ((t -= 2) * t * t * t * t + 2) / 2;
        }

        /** Sine in. */
        public static float sineIn(float t)
        {
            return -Mathf.Cos(PI2 * t) + 1;
        }

        /** Sine out. */
        public static float sineOut(float t)
        {
            return Mathf.Sin(PI2 * t);
        }

        /** Sine in and out. */
        public static float sineInOut(float t)
        {
            return -Mathf.Cos(PI * t) / 2.0f + .5f;
        }

        /** Bounce in. */
        public static float bounceIn(float t)
        {
            t = 1 - t;
            if (t < B1) return 1.0f - 7.5625f * t * t;
            if (t < B2) return 1.0f - (7.5625f * (t - B3) * (t - B3) + .75f);
            if (t < B4) return 1.0f - (7.5625f * (t - B5) * (t - B5) + .9375f);
            return 1.0f - (7.5625f * (t - B6) * (t - B6) + .984375f);
        }

        /** Bounce out. */
        public static float bounceOut(float t)
        {
            if (t < B1) return 7.5625f * t * t;
            if (t < B2) return 7.5625f * (t - B3) * (t - B3) + .75f;
            if (t < B4) return 7.5625f * (t - B5) * (t - B5) + .9375f;
            return 7.5625f * (t - B6) * (t - B6) + .984375f;
        }

        /** Bounce in and out. */
        public static float bounceInOut(float t)
        {
            if (t < .5f)
            {
                t = 1.0f - t * 2;
                if (t < B1) return (1.0f - 7.5625f * t * t) / 2.0f;
                if (t < B2) return (1.0f - (7.5625f * (t - B3) * (t - B3) + .75f)) / 2.0f;
                if (t < B4) return (1.0f - (7.5625f * (t - B5) * (t - B5) + .9375f)) / 2.0f;
                return (1.0f - (7.5625f * (t - B6) * (t - B6) + .984375f)) / 2.0f;
            }
            t = t * 2 - 1;
            if (t < B1) return (7.5625f * t * t) / 2.0f + .5f;
            if (t < B2) return (7.5625f * (t - B3) * (t - B3) + .75f) / 2.0f + .5f;
            if (t < B4) return (7.5625f * (t - B5) * (t - B5) + .9375f) / 2.0f + .5f;
            return (7.5625f * (t - B6) * (t - B6) + .984375f) / 2.0f + .5f;
        }

        /** Circle in. */
        public static float circIn(float t)
        {
            return -(Mathf.Sqrt(1 - t * t) - 1);
        }

        /** Circle out. */
        public static float circOut(float t)
        {
            return Mathf.Sqrt(1 - (t - 1) * (t - 1));
        }

        /** Circle in and out. */
        public static float circInOut(float t)
        {
            return t <= .5 ? (Mathf.Sqrt(1 - t * t * 4) - 1) / -2 : (Mathf.Sqrt(1 - (t * 2 - 2) * (t * 2 - 2)) + 1) / 2;
        }

        /** Exponential in. */
        public static float expoIn(float t)
        {
            return Mathf.Pow(2, 10 * (t - 1));
        }

        /** Exponential out. */
        public static float expoOut(float t)
        {
            return -Mathf.Pow(2, -10 * t) + 1;
        }

        /** Exponential in and out. */
        public static float expoInOut(float t)
        {
            return t < .5 ? Mathf.Pow(2, 10 * (t * 2 - 1)) / 2 : (-Mathf.Pow(2, -10 * (t * 2 - 1)) + 2) / 2;
        }

        /** Back in. */
        public static float backIn(float t)
        {
            return t * t * (2.70158f * t - 1.70158f);
        }

        /** Back out. */
        public static float backOut(float t)
        {
            return 1 - (--t) * (t) * (-2.70158f * t - 1.70158f);
        }

        /** Back in and out. */
        public static float backInOut(float t)
        {
            t *= 2;
            if (t < 1) return t * t * (2.70158f * t - 1.70158f) / 2.0f;
            t--;
            return (1 - (--t) * (t) * (-2.70158f * t - 1.70158f)) / 2.0f + .5f;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class MathHelper
{
    public static float ToRadians(this float val)
    {
        return val * (Mathf.PI / 180);
    }

    public static float ToDegrees(this float val)
    {
        return val * (180 / Mathf.PI);
    }

    public static float AngleBetween(Vector3 p1, Vector3 p2)
    {
        return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x);
    }
}
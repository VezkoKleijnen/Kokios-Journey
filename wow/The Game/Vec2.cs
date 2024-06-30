using System;
using GXPEngine;	// For Mathf

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        float length = 0;
        length = Mathf.Sqrt(x * x + y * y);
        return length;
    }

    public void SetXY(float _x, float _y)
    {
        x = _x;
        y = _y;
    }

    public float GetAngleDegrees()
    {
        float angle = Rad2Deg(Mathf.Atan2(y, x));

        if (angle < 0)
        {
            return 360 + angle;
        }
        else
        {
            return angle;
        }
    }

    public float GetAngleRadians()
    {
        float angle = Mathf.Atan2(y, x);

        if (angle < 0)
        {
            return (2 * Mathf.PI) + angle;
        }
        else
        {
            return angle;
        }
    }

    public void Normalize()
    {
        float length = Length();

        if (length != 0)
        {
            x /= length;
            y /= length;
        }
    }


    public void SetAngleDegrees(float deg)
    {
        float angle = Deg2Rad(deg);
        y = Mathf.Sin(angle) * Length();
        x = Mathf.Cos(angle) * Length();
    }

    public void SetAngleRadians(float angle)
    {
        y = Mathf.Sin(angle) * Length();
        x = Mathf.Cos(angle) * Length();
    }

    public void RotateDegrees(float angle)
    {
        angle = Deg2Rad(angle);
        RotateRadians(angle);
    }

    public void RotateRadians(float angle)
    {
        this = new Vec2(Mathf.Cos(angle) * x - Mathf.Sin(angle) * y, Mathf.Cos(angle) * y + Mathf.Sin(angle) * x);
    }

    public void RotateAroundDegrees(Vec2 point, float angle)
    {
        angle = Deg2Rad(angle);
        RotateAroundRadians(point, angle);
    }

    public void RotateAroundRadians(Vec2 point, float angle)
    {
        this -= point;
        RotateRadians(angle);
        this += point;
    }


    public Vec2 Normalized()
    {
        float length = Length();

        if (length != 0)
        {
            return new Vec2(x / length, y / length);
        }
        else
        {
            return this;
        }
    }


    public static float Deg2Rad(float angle)
    {
        return (angle / 180 * Mathf.PI);
    }

    public static float Rad2Deg(float angle)
    {
        return (angle / Mathf.PI * 180);
    }

    public static Vec2 GetUnitVectorDeg(float angle)
    {
        angle = Deg2Rad(angle);
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vec2 GetUnitVectorRad(float angle)
    {
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vec2 RandomUnitVector()
    {
        float angle = Utils.Random(0, 361);
        angle = Deg2Rad(angle);
        return new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));
    }


    public float Dot(Vec2 other)
    {
        return x * other.x + y * other.y;
    }

    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 reflectVec, float bounciness)
    {
        Vec2 normal = reflectVec.Normal();
        this -= (1 + bounciness) * (Dot(normal)) * normal;
        //projects his length onto the normal vector, times the normal vector, times 1 bounciness, 1 to cancel out the normal, and bounciness is the amount it bounces back
    }

    public static Vec2 GetReflect(Vec2 toReflect, Vec2 reflectVec, float bounciness)
    {
        Vec2 normal = reflectVec.Normal();
        return toReflect - (1 + bounciness) * (toReflect.Dot(normal)) * normal;
        //projects his length onto the normal vector, times the normal vector, times 1 bounciness, 1 to cancel out the normal, and bounciness is the amount it bounces back
    }



    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }



    public static Vec2 operator *(Vec2 v, float scalar)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator *(float scalar, Vec2 v)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator /(Vec2 v, float scalar)
    {
        return new Vec2(v.x / scalar, v.y / scalar);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}


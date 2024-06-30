using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class AttachedBall : GameObject
{
    Vec2 position;
    Vec2 oldVel;
    float radius;
    Plank plank;


    public bool touching;
    bool left;

    float bounces;



    float xOffset;
    float yOffset;

    public AttachedBall(float _radius, Plank _plank, float _x, float _y, bool _left)
    {
        radius = _radius;
        plank = _plank;
        xOffset = _x;
        yOffset = _y;
        position = new Vec2(_x, _y);
        left = _left;

        bounces = 1;
    }

    void Update()
    {
        touching = false;
        //UpdatePos();
        HitCheck();
        oldVel = plank.velocity;
    }

    void HitCheck()
    {
        foreach (LineSegment _lineSegment in plank.stage.lines)
        {
            if (!(_lineSegment is BridgeLine))
            {
                float ballDistance = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

                float oldBallDistance = ((position - oldVel) - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

                float projection = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normalized());

                if (ballDistance < radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance > 0 || (ballDistance < 0 && oldBallDistance > 0)))
                {
                    //plank.position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                    //plank.velocity.Reflect((_lineSegment.end - _lineSegment.start), 1f);

                    if (bounces <= 0)
                    {
                        plank.velocity.SetXY(0, 0);

                    }
                    else
                    {
                        plank.velocity.y = Mathf.Abs(plank.velocity.y) * -0.5f;
                        bounces++;
                    }
                    //Console.WriteLine(_lineSegment.start);
                    //Console.WriteLine(_lineSegment.end);
                    //touching = true;
                    /*
                    plank.rotVec += Vec2.GetReflect(plank.rotVec, (_lineSegment.end - _lineSegment.start), 0);
                    plank.velocity.RotateAroundDegrees(position, Vec2.GetReflect(plank.rotVec, (_lineSegment.end - _lineSegment.start), 0).GetAngleDegrees());
                    */
                    //dont waste time on this, if u do that, do it after project, but than u gotta do proffesional orientation, so nono, dont
                    // plank.rotVec.Reflect(, 1f);
                }

                if (ballDistance > -radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && ballDistance < 0)
                {
                    //plank.position -= (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                    //plank.velocity.Reflect((_lineSegment.end - _lineSegment.start), 1f);

                    //touching = true;
                    /*
                    plank.rotVec += Vec2.GetReflect(plank.rotVec, (_lineSegment.end - _lineSegment.start), 0);
                    plank.velocity.RotateAroundDegrees(position, Vec2.GetReflect(plank.rotVec, (_lineSegment.end - _lineSegment.start), 0).GetAngleDegrees());
                    */
                }

            }
        }
    }

    void Draw(byte red, byte green, byte blue)
    {
        EasyDraw myself = new EasyDraw(50, 50, false);
        myself.Fill(red, green, blue);
        myself.Stroke(red, green, blue);
        myself.Ellipse(radius, radius, 2 * radius, 2 * radius);
        myself.x -= radius;
        myself.y -= radius;
        AddChild(myself);
    }
    public void UpdatePos()
    {
        
        position.SetXY(plank.position.x + xOffset, plank.position.y + yOffset);
        position.RotateAroundDegrees(plank.position, plank.rotation);
        
        x = position.x;
        y = position.y;

    }
}


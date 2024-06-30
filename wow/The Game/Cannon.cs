using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Cannon : AnimationSprite
{
    public Vec2 position;
    public Vec2 velocity;
    public Vec2 rotationVec;

    float radius;
    bool sliding;
    StageNew _game;
    public Cannon(Vec2 _position, StageNew _stage) : base("cannonNew.png", 1, 1) 
    {
        scale = 0.4f;
        position = _position;
        velocity = new Vec2(0, 0);
        rotationVec = new Vec2(0, 0);

        SetOrigin(width , height + 128);
        radius = 32;
        sliding = false;
        _game = _stage;

        // Draw(255, 0, 0);


        AddLine(new Vec2(-width , -height ), new Vec2(width , -height ));
        AddLine(new Vec2(width , -height), new Vec2(width, height));
        AddLine(new Vec2(-width, height), new Vec2(width, height));
        AddLine(new Vec2(-width, -height), new Vec2(-width, height));
    }

    void Update()
    {
        EmitPart();

        CheckBounce();
        //PushCheck();
        //NewPushCheck();
        GhostPush();
        UpdatePos();
        velocity.y += 0.1f;


        if (Player.Main.shoot)
        {
            Bullet bullet = new Bullet(position + new Vec2(-5, -35), Vec2.GetUnitVectorDeg(rotation+ 225), 30);
            parent.AddChild(bullet);
        }

    }

    void UpdatePos()
    {
        position += velocity;
        x = position.x;
        y = position.y;
    }


    void NewPushCheck()
    {
        float ballDistance = Mathf.Abs((position - Player.Main.position).Length());
        if (Input.GetKey(Key.Q) && ballDistance < Player.Main.radius + radius * 1.2 && Player.Main.activated)
        {
            position -= (position - Player.Main.position).Normalized();
        }
        else if (ballDistance < Player.Main.radius + radius && Player.Main.position.y + 32 > position.y)
        {
            velocity.Reflect((position - position).Normal(), 0f);
            if (Player.Main.position.x < position.x && Player.Main.velocity.x > 0)
            {
                velocity.x = Player.Main.velocity.x;
            }
            if (Player.Main.position.x > position.x && Player.Main.velocity.x < 0)
            {
                velocity.x = Player.Main.velocity.x;
            }

            position += (position - Player.Main.position).Normalized();

        }
        else
        {
            if (!sliding)
            {
                velocity.x = 0;
            }

        }

        sliding = false;
    }

    void CheckBounce()
    {
        foreach (LineSegment _lineSegment in _game.lines)
        {
            if (!(_lineSegment is CannonLine))
            {
                float ballDistance = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

                float projection = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normalized());

                if (ballDistance < radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && ballDistance > 0)
                {

                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 225 && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 135)
                    {

                    }
                    else
                    {
                        //velocity.y++;
                        //sliding = true;
                    }
                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 260 && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 100)
                    {
                        rotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                    }

                    position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                    velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
                }

                if (ballDistance > -radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && ballDistance < 0)
                {
                    position -= (_lineSegment.end - _lineSegment.start).Normal() * (+ballDistance + radius); //DE + BALLDISTANCE IS DE FIX VOOR DE PLAYER DIE BUGT
                    velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
                }
            }
        }
    }

    void EmitPart()
    {
        if ((position - Player.Main.position).Length() < 2100) { 
        GhostParticle part = new GhostParticle(new Vec2(Utils.Random(-width/2, width), Utils.Random(-height, height/2)));
        part.scale = 2.5f;
        AddChild(part);
        }
    }

    void AddLine(Vec2 start, Vec2 end)
    {
        CannonLine enemyLine = new CannonLine(this, start.x, start.y, end.x, end.y);
        _game.AddChild(enemyLine);
        _game.lines.Add(enemyLine);
    }

    void GhostPush()
    {
        sliding = false;
        if (Player.Main.mainGhost != null && Player.Main.mainGhost.activated)
        {
            
            float ballDistance = Mathf.Abs((position - Player.Main.mainGhost.position).Length());
            if (ballDistance < 64)
            {
                
                velocity.x += (position - Player.Main.mainGhost.position).Normalized().x;
                Player.Main.mainGhost.velocity.Reflect((Player.Main.mainGhost.position - position).Normal(), 0);
            }
            else
            {

                velocity.x = 0;

            }
        }
        else
        {
            velocity.x = 0;
        }



        sliding = false;

    }

    void Draw(byte red, byte green, byte blue)
    {
        EasyDraw gravityRadius = new EasyDraw(width, height, false);
        gravityRadius.Fill(red, green, blue, 40);
        gravityRadius.Stroke(red, green, blue);
        gravityRadius.Ellipse(width / 2, height / 2, width, height);
        gravityRadius.x = -width / 2;
        gravityRadius.y = -height / 2;
        AddChild(gravityRadius);

    }

}


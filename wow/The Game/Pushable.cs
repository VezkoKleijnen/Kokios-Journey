using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Pushable : AnimationSprite
{
    public Vec2 position;
    public Vec2 velocity;
    Vec2 oldVel;
    public Vec2 rotationVec;

    Prompt prompt;

    int playTimer;

    float radius;
    bool sliding;
    bool enabled;
    StageNew _game;

    Sound sound = new Sound("sounds/pushableVine.wav");

    int partTimer;

    Sound pushSound = new Sound("sounds/push.wav");

    public bool inWind;
    public Pushable(Vec2 _position, StageNew _stage, bool _enabled = true) : base("enemy.png", 2, 1)
    {
        partTimer = 0;

        inWind = false;

        playTimer = 0;

        position = _position;
        velocity = new Vec2(0,0);
        rotationVec = new Vec2(0,0);


        SetOrigin(width/2, height/2);
        radius =32;
        sliding = false;
        _game = _stage;

        enabled = _enabled;
        prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
        AddChild(prompt);
        if (!_enabled)
        {
            SetCycle(1, 1, 5);

        }
        else
        {
            SetCycle(0, 1, 5);
        }

        // Draw(255, 0, 0);

        AddLine(new Vec2(-width / 2.5f, -height / 2.5f), new Vec2(width / 2.5f, -height / 2.5f));
        AddLine(new Vec2(width / 2.5f, -height / 2.5f), new Vec2(width / 2.5f, height / 2.5f));
        AddLine(new Vec2(-width / 2.5f, height / 2.5f), new Vec2(width / 2.5f, height / 2.5f));
        AddLine(new Vec2(-width / 2.5f, -height / 2.5f), new Vec2(-width / 2.5f, height / 2.5f));
    }

    void Update()
    {
        if (!enabled && partTimer > 3 && (position - Player.Main.position).Length() < 2100)
        {
                GhostParticle part = new GhostParticle(new Vec2(Utils.Random(-width / 2, width/2), Utils.Random(-height/2, height / 2)));
                AddChild(part);
            partTimer = 0;
        }
        partTimer++;

        Animate();

        if (enabled)
        {
            CheckBounce();
            //PushCheck();
            NewPushCheck();

        }
            UpdatePos();
        if (enabled) 
        { 
            velocity.y += 0.2f;
            oldVel = velocity;

        }



        prompHandle();

        inWind = false;
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
            if (playTimer > 12)
            {
                playTimer = 0;
                pushSound.Play();
            }
        }
        else if (ballDistance < Player.Main.radius + radius && Player.Main.position.y + 32 > position.y)
        {
            //velocity.Reflect((position - position).Normal(), 0f);
                if (Player.Main.position.x < position.x && Player.Main.velocity.x > 0)
                {
                    velocity.x = Player.Main.velocity.x;
                if (playTimer > 7)
                {
                    playTimer = 0;
                    pushSound.Play();
                }
            }
                if (Player.Main.position.x > position.x && Player.Main.velocity.x < 0)
                {
                    velocity.x = Player.Main.velocity.x;
                if (playTimer > 7)
                {
                    playTimer = 0;
                    pushSound.Play();
                }
            }
                if (ballDistance < 32)
            {
                velocity.x += (position - Player.Main.position).x * 2f;
            }

                position += (position - Player.Main.position).Normalized();

        }
        else
        {
            if (!sliding && !inWind)
            {
                velocity.x = 0;
            }

        }
        playTimer++;
        sliding = false;
    }

    void CheckBounce()
    {
        foreach (LineSegment _lineSegment in _game.lines)
        {
            if (!(_lineSegment is EnemyLine))
            {
                float ballDistance = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

                float oldBallDistance = ((position - oldVel) - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

                float projection = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normalized());

                if (ballDistance < radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance > 0 || (ballDistance < 0 && oldBallDistance > 0)))
                {

                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 225 && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 135)
                    {

                    }
                    else
                    {
                        velocity.y++;
                        sliding = true;
                    }
                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 260  && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 100)
                    {
                        rotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                    }
                    


                    position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                    velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
                }

                if (ballDistance > -radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && ballDistance < 0)
                {
                    //Console.WriteLine("AAAAAAAAA");
                    position -= (_lineSegment.end - _lineSegment.start).Normal() * (+ballDistance + radius); //DE + BALLDISTANCE IS DE FIX VOOR DE PLAYER DIE BUGT
                    velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
                }
            }
        }
    }

    void AddLine(Vec2 start, Vec2 end)
    {
        EnemyLine enemyLine = new EnemyLine(this, start.x, start.y, end.x, end.y);
        _game.AddChild(enemyLine);
        _game.lines.Add(enemyLine);
    }


    void Draw(byte red, byte green, byte blue)
    {
        EasyDraw gravityRadius = new EasyDraw(width, height, false);
        gravityRadius.Fill(red, green, blue, 40);
        gravityRadius.Stroke(red, green, blue);
        gravityRadius.Ellipse(width/2, height/2, width, height);
        gravityRadius.x = -width/2;
        gravityRadius.y = -height/2;
        AddChild(gravityRadius);

    }


    void prompHandle()
    {
        if (prompt.scaleX > 0)
        {
            prompt.scaleX -= 0.1f;
        }
        if (prompt.scaleX > 1)
        {
            prompt.scaleX = 1;
        }
    }

    void OnCollision(GameObject other)
    {
        if (other is Ghost && !enabled)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                enabled = true;
                SetCycle(0, 1, 5);
                sound.Play();
            }

            if (prompt.scaleX < 1.2)
            {
                prompt.scaleX += 0.2f;
            }
        }

    }
}


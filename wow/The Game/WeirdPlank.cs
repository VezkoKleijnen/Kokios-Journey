using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class WeirdPlank : Sprite
{
    public Vec2 position;
    public Vec2 velocity;
    //public Vec2 oldVel;
    public Vec2 rotVec;
    public float speed;
    WeirdAttachedBall leftBall;
    AttachedBall rightBall;
    public StageNew stage;

    Sound sound = new Sound("sounds/weirdBridge.wav");
    Sound fallSound = new Sound("sounds/bridgeFall.wav");


    bool playedSound;
    bool playedSound2;
    Prompt prompt;

    bool activated;
    bool enabled;
    public WeirdPlank(StageNew _stage, float _x, float _y) : base("weirdBridge.png")
    {
        position = new Vec2(_x, _y);
        velocity = new Vec2(0, 0);
        rotVec = new Vec2(1, 0);
        speed = 1;

        leftBall = new WeirdAttachedBall(10, this, -width / 2 + 10, height/2, true); //de hardcoded + 10 is de radius
        _stage.AddChild(leftBall);
        //rightBall = new AttachedBall(10, this, width/2-10, 0, false); //same als hierboven
        //_stage.AddChild (rightBall);
        stage = _stage;

        prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
        AddChild(prompt);

        SetOrigin(width / 2, height / 2);
        //rotation;

        activated = false;

        playedSound = false;
        playedSound2 = false;

        WeirdBridgeLine line = new WeirdBridgeLine(this, width / 2 - 10, -height/2 + 5, -width / 2 + 10, -height / 2+5);
        _stage.lines.Add(line);
        _stage.AddChild(line);
    }

    void Update()
    {
        if (velocity.y < -1 && !playedSound)
        {
            Console.WriteLine("playSound");
            playedSound = true;
            fallSound.Play();
        }

        if (!enabled && (position - Player.Main.position).Length() < 2100)
        {

                GhostParticle part = new GhostParticle(new Vec2(Utils.Random(-width / 2, width / 2), Utils.Random(-height / 2, height / 2)));
                AddChild(part);
            

        }

        UpdatePos();
        //rotation = rotVec.GetAngleDegrees();
        leftBall.UpdatePos();
        //rightBall.UpdatePos();

        if (activated)
        {
            velocity.y += 0.1f;
        }
        prompHandle();

        BallCheck();
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

    void UpdatePos()
    {
        position += velocity * speed;

        x = position.x;
        y = position.y;
    }

    void OnCollision(GameObject other)
    {
        if (other is Ghost && !enabled)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                enabled = true;
            }

            if (prompt.scaleX < 1.2)
            {
                prompt.scaleX += 0.2f;
            }
        }
        if (other is Pushable && enabled && !playedSound2)
        {
            activated = true;
            sound.Play();
            playedSound2 = true;
        }
    }

    void BallCheck()
    {
        /*
        if (leftBall.touching)
        {
            rotation++;
            rotVec.Normalize();

        }
        if (rightBall.touching)
        {
            rotation--;
            rotVec.Normalize();
        }
        */
    }
}


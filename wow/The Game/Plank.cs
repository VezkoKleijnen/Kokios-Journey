using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Plank : Sprite
{
    public Vec2 position;
    public Vec2 velocity;
    //public Vec2 oldVel;
    public Vec2 rotVec;
    public float speed;
    AttachedBall leftBall;
    AttachedBall rightBall;
    public StageNew stage;

    Vec2 contLine;

    bool playedSound;

    Prompt prompt;

    bool activated;


    Sound falling = new Sound("sounds/bridgeFall.wav");
    Sound falling2 = new Sound("sounds/fallLog.wav");
    public Plank (StageNew _stage, float _x, float _y) : base("bridge.png")
    {
        position = new Vec2(_x, _y);
        velocity = new Vec2(0,0);
        rotVec = new Vec2(1, 0);
        speed = 1;
        
        leftBall = new AttachedBall(10, this, -width / 2 + 10, -5, true); //de hardcoded + 10 is de radius
        _stage.AddChild(leftBall);
        //rightBall = new AttachedBall(10, this, width/2-10, 0, false); //same als hierboven
        //_stage.AddChild (rightBall);
        stage = _stage;

        prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
        AddChild(prompt);

        SetOrigin(width/2, height/2);
        //rotation;

        activated = false;
        /*
        BridgeLine line = new BridgeLine(this, width / 2 - 10, 0, -width / 2 + 10, 0);
        _stage.lines.Add(line);
        _stage.AddChild(line);
        */

        playedSound = false;

        contLine = new Vec2(1471, 195);
        ContinueLine(new Vec2(1302, 74));
        ContinueLine(new Vec2(247, 88));

        ContinueLine(new Vec2(0, 206));
    }

    void ContinueLine(Vec2 end, bool drawMe = true)
    {
        if (drawMe)
        {
            BridgeLine lineSegment = new BridgeLine(this, contLine.x - 736, contLine.y - 200, end.x - 736, end.y - 200);
            stage.AddChild(lineSegment);
            stage.lines.Add(lineSegment);
        }
        contLine = end;
    }

    void Update()
    {

        if (velocity.y < 0 && !playedSound)
        {
            Console.WriteLine("playSound");
            playedSound = true;
            falling.Play();
        }
        Console.WriteLine(velocity.y);
        if (!activated)
        {
            if((position - Player.Main.position).Length() < 2100)
            {

                for (int i = 0; i < 2; i++)
                {
                    GhostParticle part = new GhostParticle(new Vec2(Utils.Random(-width / 2, width / 2), Utils.Random(-height / 2, height / 2)));
                    part.scale = 2;
                    AddChild(part);
                }

            }


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
        if (other is Ghost && !activated)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                activated = true;
                falling2.Play();
            }

            if (prompt.scaleX < 1.2)
            {
                prompt.scaleX += 0.2f;
            }
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


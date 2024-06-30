using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Player : AnimationSprite
{
    public static Player Main;

    public Vec2 position;
    public Vec2 rotationVec;
    public Vec2 velocity;
    Vec2 forward;
    Vec2 gravity;
    float speed;
    public float radius;

    StageNew game;
    bool touchGround;
    public bool activated;
    Vec2 oldVel;

    bool onLadder;
    public bool inHorWind;
    bool sliding;
    public Ghost mainGhost;

    float targetRotation;
    float rotationSpeed;


    float correctionTimer;
    float correctTime;

    public bool shoot;

    public bool disable;

    Sound toGhostSound = new Sound("sounds/goingGhost.wav");
    Sound jumpSound = new Sound("sounds/jumpSound.wav");
    public Player (float _x, float _y, StageNew _game, float _speed = 1) : base("spiritWalk.png", 8, 2)
    {

        position.SetXY(_x, _y);
        rotationVec.SetXY(1, 0);
        velocity.SetXY(0, 0);
        speed = _speed;
        game = _game;
        radius = 30;
        SetOrigin(width/2, height * 0.75f);
        onLadder = false;

        Main = this;

        oldVel = new Vec2(0, 0);
        gravity = new Vec2(0, 0);

        disable = false;

        scale = 0.23f;

        activated = true;

        mainGhost = null;

        sliding = false;

        inHorWind = false;

        correctTime = 5;
        correctionTimer = 0;

        targetRotation = 0;

        //Draw(255, 0, 0);
    }

    void Update()
    {
        if (!disable)
        {
            CorrectPosition();

            if (Input.GetKeyDown(Key.R) && mainGhost != null)
            {
                position = mainGhost.position;
            }

            CheckBounce();
            Animate();
        
            velocity.y += 0.5f;
            Aerial();
            if (activated)
            {
                PlayerController();
                LadderControls();

                onLadder = false;
                touchGround = false;
                //ToGhost();
            }
            else
            {
                if (currentFrame == 10)
                {
                    SetCycle(10, 1, 5);
                }
                if ((mainGhost.position - position).Length() < 128 && !mainGhost.activated)
                {
                    SetCycle(11, 3, 5);
                }
            }
        
            UpdatePos();
            if (velocity.x > 0)
            {
                scaleX = 0.23f;
            }
            if (velocity.x < 0)
            {
                scaleX = -0.23f;
            }
        }
        shoot = false;
    }

    void UpdatePos()
    {
        position += velocity * speed;
        oldVel = velocity;
        x = position.x;
        y = position.y;
    }

    void Draw(byte red, byte green, byte blue)
    {
        EasyDraw canvas = new EasyDraw((int)radius * 2 + 1, (int)radius * 2 + 1);
        canvas.SetOrigin(canvas.width/2, canvas.height/2);
        canvas.scale = 5;
        canvas.Fill(red, green, blue);
        canvas.Stroke(red, green, blue);
        canvas.Ellipse(radius, radius, 2 * radius, 2 * radius);

        AddChild(canvas);
    }


    void CheckBounce()
    {

        foreach (LineSegment _lineSegment in game.lines)
        {
            float ballDistance = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

            float oldBallDistance = ((position - oldVel * speed)- _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

            float projection = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normalized());
            
            //CHECK TOP

            if (ballDistance < radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance > 0 || (ballDistance < 0 &&  oldBallDistance > 0))) //voeg in de haakjes van balldistance iets toe ofzo met old ball distance
            {
                position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 180 + 45 && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 180 - 45)
                {
                    velocity.SetXY(0, 0);

                    /*
                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 180)
                    {
                        if (rotation < (_lineSegment.end - _lineSegment.start).GetAngleDegrees())
                        {
                            rotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                        }
                    }
                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 180)
                    {
                        if (rotation > (_lineSegment.end - _lineSegment.start).GetAngleDegrees())
                        {
                            rotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                        }
                    }
                    if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() == 180)
                    {
                            rotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                    }
                    */
                    targetRotation = (_lineSegment.end - _lineSegment.start).GetAngleDegrees() - 180;
                    
                } 
                else
                {
                    position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + radius);
                    sliding = true;
                    correctionTimer = 0;
                    velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
                    
                }
            }
            if (ballDistance < radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance > -2 || (ballDistance < -2 && oldBallDistance > -2))) //dit is de kleef code
            {
                if ((_lineSegment.end - _lineSegment.start).GetAngleDegrees() < 180 + 45 && (_lineSegment.end - _lineSegment.start).GetAngleDegrees() > 180 - 45)
                {
                    touchGround = true;
                    position += (_lineSegment.start - _lineSegment.end).Normal() * 2;
                }
                else
                {
                    sliding = true;
                    correctionTimer = 0;
                }
            }

                //CHECK BOTTOM
            if (ballDistance > -radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance < 0 || (ballDistance > 0 && oldBallDistance < 0)) )
            {
                //sliding = true;
                
                position -= (_lineSegment.end - _lineSegment.start).Normal() * (+ballDistance + radius);
                velocity.Reflect((_lineSegment.end - _lineSegment.start), 0);
            }


        }

    }

    void PlayerController()
    {
        if (touchGround) { 
            if (Input.GetKey(Key.D))
            {
                forward.SetXY(5, 0);
                forward.RotateDegrees(targetRotation);
                SetCycle(0, 8, 5);

            }
            else if (Input.GetKey(Key.A))
            {
                forward.SetXY(-5, 0);
                forward.RotateDegrees(targetRotation);
                SetCycle(0, 8, 5);
            }
            else
            {
                SetCycle(8, 1, 5);
                forward.SetXY(0, 0);
            }

            if (Input.GetKey(Key.SPACE))
            {
                forward.y = -10;
                jumpSound.Play();
                forward.RotateDegrees(targetRotation);
            }
            velocity = forward;

        }
        else if (!sliding && !inHorWind) 
        {
            
            if (Input.GetKey(Key.D) && velocity.x < 4)
            {
                velocity.x += 2f;

            }
            else if (Input.GetKey(Key.A) && velocity.x > -4)
            {
                velocity.x -= 2f;
            }
            else
            {
                velocity.x *= 0.95f;
            }
            
            forward.SetXY(0, 0);
        }
        inHorWind = false;
        if (!touchGround && !onLadder)
        {
            /* 
            velocity.x *= 0.99f;
            */
        }
    }

    void LadderControls()
    {

        if (onLadder)
        {
            velocity *= 0.1f;

            if (Input.GetKey(Key.W) || Input.GetKey(Key.SPACE))
            {
                velocity.y = -5f;
            }
            if ((Input.GetKey(Key.A)))
            {
                velocity.x = -5f;
            }
            if ((Input.GetKey(Key.D)))
            {
                velocity.x = 5f;
            }
            if ((Input.GetKey(Key.S)))
            {
                velocity.y = 5f;
            }
        }
    }

    void Aerial()
    {
        if (!touchGround && !onLadder)
        {
            gravity.y += 0.1f;
            targetRotation = 0;
            rotationSpeed = 1;
            if (activated)
            {
                SetCycle(8, 1, 5);
            }
        }
        else
        {
            rotationSpeed = 2;
        }

            
            if (rotation < targetRotation+2 && rotation > targetRotation-2)
            {
                rotation = targetRotation;
            }
            else if (rotation > targetRotation)
            {
                rotation-= rotationSpeed;
            }
            else if (rotation < targetRotation)
            {
                rotation+= rotationSpeed;
            }
            
            //rotation = targetRotation;
        
    }

    void OnCollision(GameObject other)
    {
        if (other is Ladder)
        {
            onLadder = true;
        }
    }


    public void ToGhost()
    {
        if (Input.GetKeyDown(Key.E))
        {
            activated = false;
            toGhostSound.Play();
            Ghost ghost = new Ghost(position + velocity, this);
            mainGhost = ghost;
            parent.LateAddChild(ghost);
            SetCycle(8, 3, 5);
        }
    }

    void CorrectPosition()
    {
        if (correctionTimer < correctTime)
        {
            correctionTimer++;
        }
        else
        {
            sliding = false;
        }
    }
}

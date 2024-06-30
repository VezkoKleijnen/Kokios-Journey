using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Ghost : AnimationSprite
{
    public Vec2 position;
    public Vec2 velocity;
    //Vec2 oldVec;
    
    float speed;
    public bool activated;
    bool travelBack;
    bool initiated;
    bool fade;
    bool transition;

    Sound goBack = new Sound("sounds/fromGhost.wav");
    public Ghost(Vec2 _position, Player player) : base("spiritFly.png", 11, 2)
    {

        
        scale = 0.23f;
        if (player.scaleX < 0)
        {
            scaleX = -0.23f;
        }
        position = _position;
        velocity = new Vec2(0,0);
        UpdatePos();

        travelBack = false;
        activated = true;
        initiated = false;
        fade = false;

        transition = true;

        rotation = player.rotation;

        alpha = 1f;

        SetCycle(11, 7, 5);
        //SetCycle(0, 11, 5);

        SetOrigin(255,390);
        speed = 10;
    }

    public void Update()
    {
        //Console.WriteLine(position);
        Animate();
        if (initiated)
        {
            if (!transition)
            {
                rotation = 0;
                if (activated)
                {
                    PlayerMovement();
                }

                UpdatePos();

                if (Input.GetKeyDown(Key.E))
                {
                    travelBack = true;
                    activated = false;

                }
                if ((position - Player.Main.position).Length() < 64 && !activated && travelBack)
                {

                    travelBack = false;
                    goBack.Play();
                    fade = true;

                }

                if (fade)
                {
                    alpha -= 0.075f;
                    scaleX *= 0.9f;
                    
                    scaleY *= 0.9f;
                }
                if (alpha <= 0)
                {
                    Player.Main.activated = true;
                    Player.Main.mainGhost = null;
                    LateDestroy();
                }

                if (travelBack)
                {
                    velocity = (Player.Main.position - position).Normalized() * ((Player.Main.position - position).Length() * 0.01f);
                }
                if (velocity.x > 0.1f && !fade)
                {
                    scaleX = 0.23f;
                }
                if (velocity.x < -0.1f && !fade)
                {
                    scaleX = -0.23f;
                }
            }
            else
            {
                alpha -= 0.015f;
                if (currentFrame == 17)
                {
                    transition = false;
                    SetCycle(0, 11, 5);
                }
                if (rotation < 1 && rotation > -1)
                {
                    rotation = 0;
                }
                if (rotation > 1)
                {
                    rotation--;
                }
                if (rotation < -1)
                {
                    rotation++;
                }
            }
        }
        initiated = true;
    }

    void PlayerMovement()
    {
        if (Input.GetKey(Key.D))
        {
            velocity.x += 0.1f;
        }
        else if (Input.GetKey(Key.A))
        {
            velocity.x += -0.1f;
        }
        else
        {
            if (velocity.x > 0.1)
            {
                velocity.x -= 0.1f;
            }
            else if (velocity.x < -0.1f)
            {
                velocity.x += 0.1f;
            }
            else
            {
                velocity.x = 0;
            }
        }


        if (Input.GetKey(Key.S))
        {
            velocity.y += 0.1f;
        }
        else if (Input.GetKey(Key.W))
        {
            velocity.y += -0.1f;
        }
        else
        {
            if (velocity.y > 0.1)
            {
                velocity.y -= 0.1f;
            }
            else if (velocity.y < -0.1f)
            {
                velocity.y += 0.1f;
            }
            else
            {
                velocity.y = 0;
            }
        }

        if (velocity.Length() > 1)
        {
            velocity.Normalize();
        }
    }

    public void UpdatePos()
    {
        position += velocity * speed;
        x = position.x;
        y = position.y;
    }
}

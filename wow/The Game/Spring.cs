using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Spring : AnimationSprite
{
    Vec2 position;
    int timer;
    public bool activated;
    Sound springSound = new Sound("sounds/spring.wav");
    public Spring(Vec2 _position, bool _activated) : base("springNew.png", 4, 1)
    {
        SetCycle(0, 1, 5);
        x = _position.x;
        y = _position.y;
        position = _position;
        SetOrigin(width/2, height);
        timer = 0;
        activated = _activated;
    }

    void Update()
    {
        if (!activated)
        {
            alpha = 0.4f;
        }
        else
        {
            alpha = 1;
        }
        Animate();
        if (activated)
        {
            CheckBounce();
        }
        else
        {

        }
    }

    void CheckBounce()
    {

        if (timer < 333)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SetCycle(0, 1, 5);
        }
        if ((Player.Main.position - position).Length() < 64 && Player.Main.velocity.y > 1)
        {
            Player.Main.velocity.y = -18;
            SetCycle(0, 4, 5);
            timer = 0;
            springSound.Play();
        }
    }
}
    



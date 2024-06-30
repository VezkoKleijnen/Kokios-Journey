using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class SpringLever : AnimationSprite
{
    Spring spring;
    Prompt prompt;

    int partTimer;
    public SpringLever(Vec2 _positiion, Spring _spring) : base("button.png", 2, 1)
    {
        partTimer = 0;
        SetCycle(0, 8, 5);
        spring = _spring;

        x = _positiion.x;
        y = _positiion.y;

        SetOrigin(width/2, height/2);

        prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
        AddChild(prompt);
    }

    void Update()
    {
        if (prompt.scaleX > 0)
        {
            prompt.scaleX -= 0.1f;
        }
        if (prompt.scaleX > 1)
        {
            prompt.scaleX = 1;
        }
        if (!spring.activated && partTimer > 2)
        {
                GhostParticle part = new GhostParticle(new Vec2(Utils.Random(-width / 2, width / 2), Utils.Random(-height /2, height / 2)));
                AddChild(part);
            partTimer = 0;
        }
        partTimer++;

    }

    void OnCollision(GameObject other)
    {
        if (other is Ghost)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                spring.activated = true;
                SetCycle(1, 1, 5);
            }

            if (prompt.scaleX < 1.2)
            {
                prompt.scaleX += 0.2f;
            }
        }

    }
}


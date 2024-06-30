using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Target : AnimationSprite
{
    Vec2 position;

    public bool hit;

    Sound sound = new Sound("sounds/target_hit.wav");

    public Target(Vec2 _position) : base("target.png", 1, 1)
    {
        hit = false;
        SetCycle(0, 1, 5);
        position = _position;
        scale = 0.3f;
        UpdatePos();
    }

    void Update()
    {
        Animate();

        if (hit)
        {
            SetCycle(1, 1, 5);
        }
        else
        {
            SetCycle(0, 1, 5);
        }

        UpdatePos();
    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;

    }

    void OnCollision(GameObject other)
    {
        if (other is Bullet && !hit)
        {
            hit = true;
            other.LateDestroy();
            sound.Play();
            alpha = 0.1f;
        }
    }
}

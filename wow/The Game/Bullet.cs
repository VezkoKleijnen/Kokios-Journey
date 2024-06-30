using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

internal class Bullet : AnimationSprite
{
    public Vec2 position;
    public Vec2 velocity;
    Vec2 oldVel;

    Sound sound = new Sound("sounds/target_miss.wav");

    

    float speed;
    public Bullet(Vec2 _position, Vec2 _velocity, float _speed) : base("enemyBullet.png", 4, 1)
    {
        position = _position;
        velocity = _velocity;
        oldVel = new Vec2(0,0);
        speed = _speed;
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        if (y > 2900)
        {
            LateDestroy();
            sound.Play();
        }
        UpdatePos();
    }

    void UpdatePos()
    {
        position += velocity * speed;
        rotation = velocity.GetAngleDegrees();
        velocity.y += 0.2f / speed;
        speed *= 0.99f;
        x = position.x;
        y = position.y;
    }
}

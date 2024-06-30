using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

internal class WindParticle : Sprite
{
    Vec2 velocity;
    Vec2 position;
    float timer;
    float endTime;
    public WindParticle(Vec2 _position, Vec2 _velocity, float _timer) : base("line.png")
    { 
        velocity = _velocity;
        position = _position;
        endTime = _timer;
        scale = Utils.Random(0.8f, 1.2f);
        timer = 0;
    }

    void Update()
    {
        timer++;
        if (timer > endTime)
        {
            LateDestroy();
        }
        rotation = velocity.GetAngleDegrees();
        alpha -= 0.01f;
        UpdatePos();
    }

    void UpdatePos()
    {
        position += velocity;
        x = position.x;
        y = position.y;
    }

}


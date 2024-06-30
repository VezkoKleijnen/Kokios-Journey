using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class GhostParticle : Sprite
{
    Vec2 position;
    Vec2 velocity;
    float rotating;
    Vec2 opoint;
    public GhostParticle(Vec2 _position) : base("part.png")
    {
        alpha = Utils.Random(0.2f, 1f);
        position = _position;
        velocity = new Vec2(Utils.Random(-0.3f, 0.3f), Utils.Random(-0.3f, 0.3f));
        UpdatePos();
        rotating = Utils.Random(-5f, 5f);
        opoint = new Vec2(0, 0);
    }

    void Update()
    {
        alpha -= 0.02f;
        velocity.RotateAroundDegrees(opoint, rotating);
        scale += Utils.Random(-0.1f, 0.1f);
        if (alpha <= 0f)
        {
            LateDestroy();
        }
        UpdatePos();
    }

    void UpdatePos()
    {
        position += velocity;
        x = position.x;
        y = position.y;
    }
}


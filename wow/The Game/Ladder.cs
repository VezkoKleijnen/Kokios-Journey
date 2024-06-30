using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Ladder : Sprite
{
    Vec2 position;
    public Ladder(Vec2 _position) : base("ladder.png", false, true)
    {
        position = _position;
    }

    void Update()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        x = position.x; y = position.y;
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

internal class Prompt : Sprite
{
    Vec2 position;
    
    public Prompt(Vec2 _position, String filename) : base(filename)
    {
        position = _position;
        SetOrigin(width / 2, height / 2);
        scaleX = 0;

    }

    void Update()
    {
        UpdatePos();
    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;
    }
}


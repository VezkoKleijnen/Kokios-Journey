using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Altar : AnimationSprite
{
    public Vec2 position;
    bool caveVersion;
    public Altar (Vec2 _position, bool _caveVersion) : base("altar.png", 4, 1)
    {
        if (_caveVersion)
        {
            SetCycle(2, 1, 5);
        }
        else
        {
            SetCycle(0, 1, 5);
        }


        SetOrigin(width/2, height/2);

        scale = 0.4f;

        position = _position;

        caveVersion = _caveVersion;

        UpdatePos();
    }

    void Update()
    {
        if ((position - Player.Main.position).Length() < 200)
        {
            if (Input.GetKeyDown(Key.E) && Player.Main.activated)
            {
                Player.Main.ToGhost();
            }
            if (Player.Main.mainGhost != null)
            {
                if (caveVersion)
                {
                    SetCycle(3, 1, 5);
                }
                else
                {
                    SetCycle(1, 1, 5);
                }
            }
        }
        if (Player.Main.mainGhost == null)
        {
            if (caveVersion)
            {
                SetCycle(2, 1, 5);
            }
            else
            {
                SetCycle(0, 1, 5);
            }

        }


        UpdatePos();
    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;
    }
}

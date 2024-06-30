using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class CannonLine : LineSegment
{
    float xOffS;
    float yOffS;
    float xOffE;
    float yOffE;
    public int health;
    public Cannon enemy;
    int stayColor;
    float myRotation;
    public CannonLine(Cannon _enemy, float _xOffS, float _yOffS, float _xOffE, float _yOffE) : base(_xOffS - _enemy.position.x, _yOffS - _enemy.position.y, _xOffE - _enemy.position.x, _yOffE - _enemy.position.y, 0xffffffff, 2)
    {
        health = 6;
        xOffS = _xOffS;
        yOffS = _yOffS;
        xOffE = _xOffE;
        yOffE = _yOffE;
        enemy = _enemy;
    }

    void Update()
    {
        myRotation = Vec2.Deg2Rad(enemy.rotation + 90);
        if (stayColor < 5)
        {
            stayColor++;
        }
        else
        {
            color = 0x00ffffff;
        }

        start.x = (xOffS * enemy.scale) + enemy.position.x;
        start.y = (yOffS * enemy.scale) + enemy.position.y;
        end.x = (xOffE * enemy.scale) + enemy.position.x;
        end.y = (yOffE * enemy.scale) + enemy.position.y;

        start.RotateAroundRadians(enemy.position, myRotation);
        end.RotateAroundRadians(enemy.position, myRotation);
    }
}


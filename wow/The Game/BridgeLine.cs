using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GXPEngine;
internal class BridgeLine : LineSegment
{
    float xOffS;
    float yOffS;
    float xOffE;
    float yOffE;
    public Plank enemy;
    int stayColor;
    float myRotation;

    float fixTimer;
    public BridgeLine(Plank _enemy, float _xOffS, float _yOffS, float _xOffE, float _yOffE) : base(_xOffS - _enemy.position.x, _yOffS - _enemy.position.y, _xOffE - _enemy.position.x, _yOffE - _enemy.position.y, 0x00ffffff, 2)
    {
        xOffS = _xOffS;
        yOffS = _yOffS;
        xOffE = _xOffE;
        yOffE = _yOffE;
        enemy = _enemy;
    }

    void Update()
    {
        


        start.x = (xOffS * enemy.scale) + enemy.position.x;
        start.y = (yOffS * enemy.scale) + enemy.position.y;

        fixTimer++;


        if (Player.Main.position.y > enemy.y-10)
        {
            fixTimer = 0;
        }

        if (fixTimer > 30)
        {
            end.x = (xOffE * enemy.scale) + enemy.position.x;
            end.y = (yOffE * enemy.scale) + enemy.position.y;
        }
        else
        {
            end.x = start.x;
            end.y = start.y;
        }


        start.RotateAroundRadians(enemy.position, myRotation);
        end.RotateAroundRadians(enemy.position, myRotation);
    }
}


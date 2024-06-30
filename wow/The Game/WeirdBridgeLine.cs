using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GXPEngine;
internal class WeirdBridgeLine : LineSegment
{
    float xOffS;
    float yOffS;
    float xOffE;
    float yOffE;
    public WeirdPlank enemy;
    int stayColor;
    float myRotation;

 
    public WeirdBridgeLine(WeirdPlank _enemy, float _xOffS, float _yOffS, float _xOffE, float _yOffE) : base(_xOffS - _enemy.position.x, _yOffS - _enemy.position.y, _xOffE - _enemy.position.x, _yOffE - _enemy.position.y, 0x00ffffff, 2)
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







            end.x = (xOffE * enemy.scale) + enemy.position.x;
            end.y = (yOffE * enemy.scale) + enemy.position.y;
        


        start.RotateAroundRadians(enemy.position, myRotation);
        end.RotateAroundRadians(enemy.position, myRotation);
    }
}


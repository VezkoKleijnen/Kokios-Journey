using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class GoalBall : AnimationSprite
{
    RotTablet rotTablet;
    Vec2 position;
    float myRotation;

    float xOffS;
    float yOffS;
    public GoalBall(Vec2 _position, RotTablet _rotTablet) : base("goal.png",1, 1)
    {
        rotTablet = _rotTablet;
        //position = _position;

        SetOrigin(width/2, height/2);

        scale = 0.5f;

        xOffS = _position.x;
        yOffS = _position.y;

    }

    void Update()
    {


        myRotation = Vec2.Deg2Rad(rotTablet.rotation);
        
        rotateManager();
        UpdatePos();

        if ((Ball.main.position - position).Length() < 32)
        {
            rotTablet.puzzle.done = true;
            rotTablet.puzzle.stage.ballPuzzleComplete = true;
        }

    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;
    }

    void rotateManager()
    {
        rotation = rotTablet.rotation;
        position.x = (xOffS * rotTablet.scale) + rotTablet.position.x;
        position.y = (yOffS * rotTablet.scale) + rotTablet.position.y;
        position.RotateAroundRadians(rotTablet.position, myRotation);
    }
}

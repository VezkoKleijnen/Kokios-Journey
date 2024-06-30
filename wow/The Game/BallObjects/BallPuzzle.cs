using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class BallPuzzle : GameObject
{
    public List<LineSegment> ballLines;
    Camera cam;
    RotTablet rotTablet;
    public StageNew stage;
    public bool done;
    public BallPuzzle(float _x, float _y, Camera _cam, StageNew _stage)
    {
        ballLines = new List<LineSegment>();

        Player.Main.disable = true;

        x = _x;
        y = _y;

        done = false;


        rotTablet = new RotTablet(this);
        AddChild(rotTablet);

        Ball ball = new Ball(10, new Vec2(100, -100), this);
        AddChild(ball);

        GoalBall goal = new GoalBall(new Vec2(0, 0), rotTablet);
        AddChild(goal);

        stage = _stage;
        cam = _cam;
    }

    void Update()
    {
        
        x = cam.x;
        y = cam.y;


        if (done)
        {
            rotTablet.alpha -= 0.05f;
        }
        if (rotTablet.alpha <= 0)
        {
            LateDestroy();
        }
        
    }


}


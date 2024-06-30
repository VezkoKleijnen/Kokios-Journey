using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class StartPuzzle : AnimationSprite
{
    Vec2 position;

    Camera cam;

    Prompt prompt;

    bool activated;
    StageNew stage;
    public StartPuzzle(Vec2 _position, Camera _cam, StageNew _stage) : base("table.png", 1, 1)
    {
        position = _position;

        SetOrigin(width/2, height/2);
    
        scale = 0.3f;

        prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
        AddChild(prompt);

        activated = false;

        stage = _stage;
        cam = _cam;
    }

    void Update()
    {
        if (!activated && (position - Player.Main.position).Length() < 64 && Input.GetKeyDown(Key.Q))
        {
            activated = true;
            BallPuzzle ballPuzzle = new BallPuzzle(game.width/2, game.height/2, cam, stage);
            parent.AddChild(ballPuzzle);
        }

        if (prompt.scaleX > 0)
        {
            prompt.scaleX -= 0.1f;
        }
        if (prompt.scaleX > 1)
        {
            prompt.scaleX = 1;
        }
        UpdatePos();
    }

    void OnCollision(GameObject other)
    {
        if (other is Player)
        {
            if (Input.GetKeyDown(Key.Q))
            {
                Player.Main.shoot = true;
            }

            if (prompt.scaleX < 1.2)
            {
                prompt.scaleX += 0.2f;
            }
        }

    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class Wind : Sprite
{
    Vec2 position;
    Vec2 wind;
    float timer;
    bool alreadyPlay;
    Sound sound = new Sound("sounds/wind.wav");
    int timer2;
    public Wind(Vec2 _position, float _width, float _height, Vec2 _wind) : base("wind.png")
    {
        position = _position;
        wind = _wind;
        scaleX = _width;
        scaleY = _height;
        timer = 0;

        timer2 = 0;


        alreadyPlay = false;
    }

    void Update()
    {
        if (timer2 < 20) timer2++;
        timer++;
        if (timer > 10 / scaleX)
        {
            Vec2 _pos = (new Vec2(Utils.Random(0, width), Utils.Random(height/2, height)) + position);
            _pos.RotateAroundDegrees(position, rotation);
            WindParticle part = new WindParticle(_pos, wind * Utils.Random(1f, 3f), Utils.Random(20, 200));
            part.x = _pos.x;
            part.y = _pos.y;
            parent.AddChild(part);
            timer = 0;
        }
        UpdatePos();
    }

    void UpdatePos() {
        x = position.x;
        y = position.y;
    }


    void OnCollision(GameObject other)
    {
        if (other is Player)
        {
            Player.Main.velocity += wind;
            if (wind.x != 0)
            {
                Player.Main.inHorWind = true;
            }
            if (timer2 > 10)
            {
                sound.Play();
                timer2 = 0;
            }
               
        }
        if (other is Pushable)
        {
            Pushable pushable = other as Pushable;
            pushable.velocity += wind;
            pushable.inWind = true;
        }
    }
}


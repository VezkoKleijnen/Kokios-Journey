using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Shoot : Sprite
    {
        Vec2 position;
        Prompt prompt;

        Sound sound = new Sound("sounds/shooting_cannon.wav");

        public Shoot (Vec2 _position) : base("shoot.png")
        {
            scale = 0.6f;
            SetOrigin (width/2, height/2);
            x = _position.x;
            y = _position.y;
            position = _position;

            prompt = new Prompt(new Vec2(0, -64), "BlackHole.png");
            AddChild(prompt);
        }

        void Update()
        {
            if (prompt.scaleX > 0)
            {
                prompt.scaleX -= 0.1f;
            }
            if (prompt.scaleX > 1)
            {
                prompt.scaleX = 1;
            }
        }

        void OnCollision(GameObject other)
        {
            if (other is Player && Player.Main.activated)
            {
                if (Input.GetKeyDown(Key.Q))
                {
                    Player.Main.shoot = true;
                    sound.Play();
                }

                if (prompt.scaleX < 1.2)
                {
                    prompt.scaleX += 0.2f;
                }
            }

        }
    }
}

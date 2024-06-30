using System;
using GXPEngine;

internal class Ball : EasyDraw
{
	public int radius {
		get {
			return _radius;
		}
	}

	public Vec2 velocity;
	public Vec2 position;
	Vec2 oldPosition;
	Vec2 oldVel;

	public static Ball main;

	Sprite sprite;

	int _radius;
	float speed;

	BallPuzzle ballPuzzle;

	public Ball (int pRadius, Vec2 pPosition, BallPuzzle _ballPuzzle) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		//sprite = new Sprite("centerCheck.png");
		//sprite.SetOrigin(sprite.width/2, sprite.height/2);
		//AddChild (sprite);

		_radius = pRadius;
		position = pPosition;
		speed = 1;

		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);

		ballPuzzle = _ballPuzzle;

		main = this;

		Draw (255, 255, 255);
	}

	void Update()
	{
        oldVel = velocity;
        PlayerControls();
		CheckBounce();
		UpdateScreenPosition();

	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (_radius, _radius, 2*_radius, 2*_radius);
	}

	void PlayerControls() {
		velocity.y += 0.1f;
    }

	void UpdateScreenPosition() {
		position += velocity;
        //AAAA DIT VOOR EEN DEELTJE BIJ DE VELOCITY DOEN IS MISSCHIEN LEUK WANT DAN KAN JE DE BAL DUWEN

        x = position.x;
		y = position.y;
    }



    void CheckBounce()
    {

        foreach (PuzzleLine _lineSegment in ballPuzzle.ballLines)
        {
            float ballDistance = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normal());

            float oldBallDistance = ((position - oldVel * speed) - _lineSegment.oldStart).Dot((_lineSegment.oldEnd - _lineSegment.oldStart).Normal());
			//VERVANG HIER DUS DE LINESEGMENT START EN END DUS MET DE OUDE LINESEGMENT START EN ENDS

            float projection = (position - _lineSegment.start).Dot((_lineSegment.end - _lineSegment.start).Normalized());

            //CHECK TOP

            if (ballDistance < _radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance > 0 || (ballDistance < 0 && oldBallDistance > 0))) //voeg in de haakjes van balldistance iets toe ofzo met old ball distance
            {
                position += (_lineSegment.end - _lineSegment.start).Normal() * (-ballDistance + _radius);
				float bouncyness;
				if (_lineSegment.oldStart.x == _lineSegment.start.x && _lineSegment.oldStart.y == _lineSegment.start.y)
                {
					bouncyness = 0f;
				}
				else
				{
					bouncyness = 0f;
				}
                velocity.Reflect((_lineSegment.end - _lineSegment.start), bouncyness);
            }
			
            //CHECK BOTTOM
            if (ballDistance > -_radius && projection < (_lineSegment.end - _lineSegment.start).Length() && projection > 0 && (ballDistance < 0 || (ballDistance > 0 && oldBallDistance < 0)))
            {
                position -= (_lineSegment.end - _lineSegment.start).Normal() * (+ballDistance + _radius);
                float bouncyness;
                if (_lineSegment.oldStart.x == _lineSegment.start.x && _lineSegment.oldStart.y == _lineSegment.start.y)
                {
                    bouncyness = 0.2f;
                }
                else
                {
                    bouncyness = 0.3f;
                }
				//Console.WriteLine("this one?");
                velocity.Reflect((_lineSegment.end - _lineSegment.start), bouncyness);
            }


        }
    }
}

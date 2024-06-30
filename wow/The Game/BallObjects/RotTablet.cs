using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
internal class RotTablet : Sprite
{
    public Vec2 position;
    public BallPuzzle puzzle;
    Vec2 contLine;
    public RotTablet(BallPuzzle _puzzle) : base("tableTop.png")
    {
        puzzle = _puzzle;

        SetOrigin(width/2, height/2);

       

        contLine = new Vec2 (51, 165);

        position = new Vec2 (0, 0);

        ContinueLine(new Vec2(167, 49));
        ContinueLine(new Vec2(332, 49));
        ContinueLine(new Vec2(448, 165));
        ContinueLine(new Vec2(448, 330));
        ContinueLine(new Vec2(448, 330));
        ContinueLine(new Vec2(332, 446));
        ContinueLine(new Vec2(168, 446));
        ContinueLine(new Vec2(51, 329));
        ContinueLine(new Vec2(51, 165));

        contLine = new Vec2(51, 231);

        ContinueLine(new Vec2(101, 231));
        ContinueLine(new Vec2(101, 247));
        ContinueLine(new Vec2(105, 247));
        ContinueLine(new Vec2(105, 181));
        ContinueLine(new Vec2(171, 115));
        ContinueLine(new Vec2(167, 112));
        ContinueLine(new Vec2(101, 178));
        ContinueLine(new Vec2(101, 227));
        ContinueLine(new Vec2(51, 227));

        contLine = new Vec2(299, 49);

        ContinueLine(new Vec2(299, 103));
        ContinueLine(new Vec2(393, 198));
        ContinueLine(new Vec2(393, 219));
        ContinueLine(new Vec2(393, 219));
        ContinueLine(new Vec2(388, 219));
        ContinueLine(new Vec2(388, 200));
        ContinueLine(new Vec2(350, 162));
        ContinueLine(new Vec2(324, 190));
        ContinueLine(new Vec2(333, 204));
        ContinueLine(new Vec2(333, 295));
        ContinueLine(new Vec2(286, 340));
        ContinueLine(new Vec2(317, 387));
        ContinueLine(new Vec2(384, 323));
        ContinueLine(new Vec2(384, 323));
        ContinueLine(new Vec2(384, 295));
        ContinueLine(new Vec2(389, 295));
        ContinueLine(new Vec2(389, 326));
        ContinueLine(new Vec2(318, 394));
        ContinueLine(new Vec2(187, 394));
        ContinueLine(new Vec2(117, 324));
        ContinueLine(new Vec2(118, 317));
        ContinueLine(new Vec2(189, 388));
        ContinueLine(new Vec2(311, 388));
        ContinueLine(new Vec2(281, 343));
        ContinueLine(new Vec2(212, 343));
        ContinueLine(new Vec2(157, 288));
        ContinueLine(new Vec2(157, 200));
        ContinueLine(new Vec2(204, 154));
        ContinueLine(new Vec2(263, 154));
        ContinueLine(new Vec2(263, 160));
        ContinueLine(new Vec2(206, 160));
        ContinueLine(new Vec2(164, 202));
        ContinueLine(new Vec2(164, 285));
        ContinueLine(new Vec2(216, 337));
        ContinueLine(new Vec2(276, 337));
        ContinueLine(new Vec2(328, 297));
        ContinueLine(new Vec2(328, 205));
        ContinueLine(new Vec2(320, 195));
        ContinueLine(new Vec2(289, 226));
        ContinueLine(new Vec2(285, 226));
        ContinueLine(new Vec2(267, 208));
        ContinueLine(new Vec2(230, 207));
        ContinueLine(new Vec2(213, 227));
        ContinueLine(new Vec2(213, 261));
        ContinueLine(new Vec2(233, 284));
        ContinueLine(new Vec2(268, 284));
        ContinueLine(new Vec2(279, 272));
        ContinueLine(new Vec2(285, 272));
        ContinueLine(new Vec2(272, 288));
        ContinueLine(new Vec2(228, 288));
        ContinueLine(new Vec2(207, 263));
        ContinueLine(new Vec2(206, 226));
        ContinueLine(new Vec2(225, 203));
        ContinueLine(new Vec2(270, 203));
        ContinueLine(new Vec2(288, 219));
        ContinueLine(new Vec2(346, 158));
        ContinueLine(new Vec2(294, 106));
        ContinueLine(new Vec2(294, 49));




    }


    void Update()
    {
        if (Input.GetKey(Key.D))
        {
            rotation++;
        }
        if (Input.GetKey(Key.A))
        {
            rotation--;
        }
        if (Input.GetKey(Key.D) && Input.GetKey(Key.LEFT_SHIFT))
        {
            rotation += 2f;
        }
        if (Input.GetKey(Key.A) && Input.GetKey(Key.LEFT_SHIFT))
        {
            rotation -= 2f;
        }
        UpdatePos();
    }

    void UpdatePos()
    {
        x = position.x;
        y = position.y;
    }


    void AddLine(Vec2 start, Vec2 end)
    {
        PuzzleLine lineSegment = new PuzzleLine(this, start.x, start.y, end.x, end.y);
        puzzle.AddChild(lineSegment);
        puzzle.ballLines.Add(lineSegment);
    }

    void ContinueLine(Vec2 end, bool drawMe = true)
    {
        if (drawMe)
        {
            PuzzleLine lineSegment = new PuzzleLine(this, contLine.x-248, contLine.y-248, end.x-248, end.y-248);
            puzzle.AddChild(lineSegment);
            puzzle.ballLines.Add(lineSegment);
        }
        contLine = end;
    }
}


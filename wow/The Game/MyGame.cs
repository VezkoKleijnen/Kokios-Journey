using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}


    StageNew stage;

    public MyGame () : base(1920, 1019, false, false)
	{

		targetFps = 60;
        stage = new StageNew();
        AddChild(stage);
    }

	void Update () {
		//Console.WriteLine();
		
	}
}


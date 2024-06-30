using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
internal class StageNew : GameObject
{

    public List<LineSegment> lines;
    public List<Ball> balls;
    public List<Target> targets;
    Camera cam;
    Player player;
    Vec2 contLine;
    float camOffset;
    float yCamOffset;

    //Altar superAlt;

    Sprite endScreen = new Sprite("endScreen.png");

    //game's conditions
    public bool ballPuzzleComplete;
    bool bpChanges;

    bool targetsComplete;
    bool tChanges;

    Sound bgMusic = new Sound("sounds/background.wav", true);



    bool specialCam1;

    public StageNew()
    {
        bgMusic.Play();
        //superAlt = new Altar(new Vec2(0, 0), false);
        //AddChild(superAlt);

        // conditions setup
        ballPuzzleComplete = false;
        bpChanges = false;

        targetsComplete = false;
        tChanges = false;


        specialCam1 = false;


        lines = new List<LineSegment>();
        balls = new List<Ball>();
        targets = new List<Target>();

        cam = new Camera(0, 0, game.width, game.height);
        AddChild(cam);



        TiledLoader loader = new TiledLoader("Tiled/level1.tmx");
        loader.autoInstance = true;
        loader.rootObject = this;
        loader.addColliders = false;

        /*
        loader.LoadImageLayers(0);

        loader.LoadImageLayers(1);

        loader.LoadImageLayers(2);

        loader.LoadImageLayers(3);

        loader.LoadImageLayers(4);

        loader.LoadImageLayers(5);

                */
        //
        //
        //
        //AddParalex(1200, 0f, 0);
        loader.LoadImageLayers(0);

        AddParalex(600, 0.3f, 1);

        AddParalex(300, 0.25f, 4);

        AddParalex(500, 0.2f, 2);

        AddParalex(400, 0.15f, 5);

        AddParalex(400, 0.1f, 3);


        //loader.LoadImageLayers(6);




        loader.LoadObjectGroups(0);

        AddLadder(300, 1607);
        AddLadder(300, 1671);
        AddLadder(300, 1735);
        AddLadder(300, 1799);
        AddLadder(300, 1863);


        AddLadder(5955, 2381);
        AddLadder(5955, 2445);
        AddLadder(5955, 2509);
        AddLadder(5955, 2573);
        AddLadder(5955, 2637);
        AddLadder(5955, 2701);
        AddLadder(5955, 2765);
        AddLadder(5955, 2829);



        loader.LoadTileLayers(0);


        Plank plank = new Plank(this, 3700, 1200);
        AddChild(plank);




        cam.y = game.height / 2;
        cam.x = game.width / 2;


        AddLine(new Vec2(game.width, 0), new Vec2(0, 0));
        

        AddLine(new Vec2(0, 0), new Vec2(0, game.height));

        //AddLine(new Vec2(game.width, game.height), new Vec2(0, game.height - 20));


        /*
        AddLine(new Vec2(590, 545), new Vec2(490, 495));

        AddLine(new Vec2(490, 495), new Vec2(480, 491));

        AddLine(new Vec2(480, 491), new Vec2(460, 485));

        AddLine(new Vec2(460, 485), new Vec2(430, 479));

        AddLine(new Vec2(430, 479), new Vec2(390, 477));

        AddLine(new Vec2(390, 477), new Vec2(0, 477));
        */
        
        

        


        Cannon cannon = new Cannon(new Vec2(5301, 2844), this);
        AddChild(cannon);

        //AddLadder(300, 920);








        /*

        loader.LoadObjectGroups(0);
        loader.LoadImageLayers(1);
        */



        StartPuzzle startPuzzle = new StartPuzzle(new Vec2(3100, 488), cam, this);
        AddChild(startPuzzle);


        AddLine(new Vec2(285, 1604), new Vec2(0, 1604));
        AddLine(new Vec2(287, 1605), new Vec2(285, 1604));
        AddLine(new Vec2(288, 1607), new Vec2(287, 1605));



        AddLine(new Vec2(288, 1893), new Vec2(288, 1607));
        AddLine(new Vec2(291, 1896), new Vec2(288, 1893));
        AddLine(new Vec2(295, 1900), new Vec2(291, 1896));
        AddLine(new Vec2(811, 1900), new Vec2(295, 1900));
        AddLine(new Vec2(870, 1905), new Vec2(811, 1900));
        AddLine(new Vec2(955, 1911), new Vec2(870, 1905));
        AddLine(new Vec2(987, 1913), new Vec2(955, 1911));
        AddLine(new Vec2(1049, 1926), new Vec2(987, 1913));
        AddLine(new Vec2(1059, 1930), new Vec2(1049, 1926));
        AddLine(new Vec2(1092, 1960), new Vec2(1059, 1930));
        AddLine(new Vec2(1153, 1963), new Vec2(1092, 1960));
        AddLine(new Vec2(1195, 1966), new Vec2(1153, 1963));
        AddLine(new Vec2(1293, 1960), new Vec2(1195, 1966));
        AddLine(new Vec2(1293, 1828), new Vec2(1293, 1960));
        AddLine(new Vec2(1294, 1824), new Vec2(1293, 1828));
        AddLine(new Vec2(1297, 1820), new Vec2(1294, 1824));
        AddLine(new Vec2(1300, 1818), new Vec2(1297, 1820));
        AddLine(new Vec2(1307, 1816), new Vec2(1300, 1818));

        AddLine(new Vec2(1730, 1816), new Vec2(1307, 1816));


        Spring spring1 = new Spring(new Vec2(1700, 1816), false);
        AddChild(spring1);

        SpringLever springLever1 = new SpringLever(new Vec2(1207, 1500), spring1);
        AddChild(springLever1);

        camOffset = 0;
        yCamOffset = 0;

        AddLine(new Vec2(1739, 1813), new Vec2(1730, 1816));

        AddLine(new Vec2(1772, 1802), new Vec2(1739, 1813));
        AddLine(new Vec2(1784, 1795), new Vec2(1772, 1802));
        AddLine(new Vec2(1793, 1786), new Vec2(1784, 1795));
        AddLine(new Vec2(1802, 1772), new Vec2(1793, 1786));
        AddLine(new Vec2(1810, 1754), new Vec2(1802, 1772));
        AddLine(new Vec2(1814, 1734), new Vec2(1810, 1754));
        AddLine(new Vec2(1814, 1608), new Vec2(1814, 1734));
        AddLine(new Vec2(1815, 1596), new Vec2(1814, 1608));
        AddLine(new Vec2(1817, 1587), new Vec2(1815, 1596));
        AddLine(new Vec2(1820, 1580), new Vec2(1817, 1587));
        AddLine(new Vec2(1823, 1574), new Vec2(1820, 1580));    
        AddLine(new Vec2(1823, 1574), new Vec2(1823, 1574));

        contLine = new Vec2(1823, 1574);

        ContinueLine(new Vec2(1832, 1566)); 
        ContinueLine(new Vec2(1839, 1563));
        ContinueLine(new Vec2(1846, 1560));
        ContinueLine(new Vec2(1858, 1559));
        ContinueLine(new Vec2(1881, 1558));
        // paarse rechte lijn de eerste
        ContinueLine(new Vec2(3060, 1558));
        ContinueLine(new Vec2(3074, 1561));
        ContinueLine(new Vec2(3083, 1565));
        ContinueLine(new Vec2(3090, 1570));
        ContinueLine(new Vec2(3098, 1577));
        ContinueLine(new Vec2(3105, 1587));
        ContinueLine(new Vec2(3112, 1601));
        ContinueLine(new Vec2(3115, 1615));
        ContinueLine(new Vec2(3116, 1633));
        //lijn naar benede


        Wind wind1 = new Wind(new Vec2(3116, 1650), 10, 7, new Vec2(-0.7f, -1));
        AddChild(wind1);


        ContinueLine(new Vec2(3116, 2788));
        ContinueLine(new Vec2(3771, 2788));
        ContinueLine(new Vec2(3771, 1666));

        //ContinueLine(new Vec2(3771, 1657));
        ContinueLine(new Vec2(3773, 1666));
        ContinueLine(new Vec2(3775, 1663));
        ContinueLine(new Vec2(3778, 1660));
        ContinueLine(new Vec2(3786, 1658));
        ContinueLine(new Vec2(3847, 1658));
        ContinueLine(new Vec2(3851, 1659));
        ContinueLine(new Vec2(3853, 1661));
        ContinueLine(new Vec2(3855, 1665));
        ContinueLine(new Vec2(3855, 2788));
        ContinueLine(new Vec2(4857, 2788));

        ContinueLine(new Vec2(4875, 2789));
        ContinueLine(new Vec2(4888, 2793));
        ContinueLine(new Vec2(4899, 2800));
        ContinueLine(new Vec2(4903, 2806));
        ContinueLine(new Vec2(4906, 2811));
        ContinueLine(new Vec2(4908, 2821));
        ContinueLine(new Vec2(4908, 2872));
        ContinueLine(new Vec2(6303, 2872));
        ContinueLine(new Vec2(6349, 2854));
        ContinueLine(new Vec2(6382, 2837));
        ContinueLine(new Vec2(6405, 2822));
        ContinueLine(new Vec2(6435, 2806));
        ContinueLine(new Vec2(6463, 2790));
        ContinueLine(new Vec2(6463, 2790));
        ContinueLine(new Vec2(6472, 2787));
        ContinueLine(new Vec2(6501, 2785));
        ContinueLine(new Vec2(6596, 2783));
        ContinueLine(new Vec2(6624, 2786));
        ContinueLine(new Vec2(7759, 2786));

        //right bottom corner
        ContinueLine(new Vec2(7759, 2143));
        ContinueLine(new Vec2(7678, 2143));
        ContinueLine(new Vec2(7678, 2135));
        ContinueLine(new Vec2(7678, 1836));
        ContinueLine(new Vec2(7681, 1826));
        ContinueLine(new Vec2(7684, 1821));
        ContinueLine(new Vec2(7688, 1816));
        ContinueLine(new Vec2(7694, 1810));
        ContinueLine(new Vec2(7694, 1810));
        ContinueLine(new Vec2(7701, 1806));
        ContinueLine(new Vec2(7707, 1803));
        ContinueLine(new Vec2(7762, 1803));
        ContinueLine(new Vec2(7762, 100));

        contLine.SetXY(4264, 1669);

        ContinueLine(new Vec2(4264, 1628));
        ContinueLine(new Vec2(4267, 1606));
        ContinueLine(new Vec2(4272, 1594));
        ContinueLine(new Vec2(4277, 1587));
        ContinueLine(new Vec2(4286, 1578));
        ContinueLine(new Vec2(4298, 1571));
        ContinueLine(new Vec2(4314, 1566));
        ContinueLine(new Vec2(4325, 1564));

        ContinueLine(new Vec2(5247, 1564));
        ContinueLine(new Vec2(5261, 1566)); 
        ContinueLine(new Vec2(5269, 1570));
        ContinueLine(new Vec2(5279, 1581));
        ContinueLine(new Vec2(5288, 1594));



        ContinueLine(new Vec2(5607, 2041));
        //AddLine(new Vec2(5288, 1594), new Vec2(5607, 2041));



        ContinueLine(new Vec2(5615, 2045));
        ContinueLine(new Vec2(5624, 2048));
        ContinueLine(new Vec2(7421, 2048));
        ContinueLine(new Vec2(7431, 2046));
        ContinueLine(new Vec2(7431, 1827));
        ContinueLine(new Vec2(7433, 1817));
        ContinueLine(new Vec2(7436, 1812));
        ContinueLine(new Vec2(7442, 1806));
        ContinueLine(new Vec2(7448, 1802));
        ContinueLine(new Vec2(7458, 1799));

        //DRAAI DE LANGE SCHUINE LIJN OM ZODAT ONDERKANT BOVEN ZIT

        ContinueLine(new Vec2(7486, 1799));
        ContinueLine(new Vec2(7493, 1801));
        ContinueLine(new Vec2(7500, 1805));
        ContinueLine(new Vec2(7507, 1812));
        ContinueLine(new Vec2(7507, 1812));
        ContinueLine(new Vec2(7510, 1817));
        ContinueLine(new Vec2(7512, 1824));
        ContinueLine(new Vec2(7512, 2143));

        ContinueLine(new Vec2(5587, 2143));
        ContinueLine(new Vec2(5547, 2107));
        ContinueLine(new Vec2(5235, 1670));
        ContinueLine(new Vec2(4264, 1669));

        contLine.SetXY(5562, 2442);

        ContinueLine(new Vec2(5561, 2395));
        ContinueLine(new Vec2(5563, 2386));
        ContinueLine(new Vec2(5566, 2378));
        ContinueLine(new Vec2(5571, 2370));
        ContinueLine(new Vec2(5578, 2364));
        ContinueLine(new Vec2(5588, 2359));
        ContinueLine(new Vec2(5606, 2356));
        ContinueLine(new Vec2(5622, 2355));
        ContinueLine(new Vec2(5875, 2355));
        ContinueLine(new Vec2(5917, 2356));
        ContinueLine(new Vec2(5938, 2358));
        ContinueLine(new Vec2(5944, 2361));
        ContinueLine(new Vec2(5949, 2366));
        ContinueLine(new Vec2(5952, 2372));
        ContinueLine(new Vec2(5954, 2376));
        ContinueLine(new Vec2(5955, 2381));
        ContinueLine(new Vec2(5955, 2442));
        ContinueLine(new Vec2(5562, 2442));



        contLine.SetXY(1966,0);
        
        ContinueLine(new Vec2(1967, 554));
        ContinueLine(new Vec2(3915, 554));
        ContinueLine(new Vec2(3944, 557));
        ContinueLine(new Vec2(3961, 561));
        ContinueLine(new Vec2(3986, 567));
        ContinueLine(new Vec2(3995, 572));
        ContinueLine(new Vec2(4004, 579));
        ContinueLine(new Vec2(4012, 588));
        ContinueLine(new Vec2(4023, 600));
        ContinueLine(new Vec2(4027, 607));
        ContinueLine(new Vec2(4031, 614));
        ContinueLine(new Vec2(4034, 622));
        ContinueLine(new Vec2(4034, 634));
        ContinueLine(new Vec2(1970, 634));

        AddTarget(4023, 2327);
        AddTarget(4250, 2509);
        AddTarget(4638, 2493);




        AddAltar(2558, 1430);

        AddAltar(6251, 1920);

        AddAltar(5885, 2230);

        Shoot shoot = new Shoot(new Vec2(5685, 2260));
        AddChild(shoot);

        player = new Player(240, 1375, this);
        AddChild(player);

        Wind wind3 = new Wind(new Vec2(6900, 1708), 2, 6, new Vec2(1f, -0.7f));
        wind3.rotation = 45;
        AddChild(wind3);

       // Wind wind4 = new Wind(new Vec2(7024, 1708), 2, 6, new Vec2(1f, 0f));
        //wind4.rotation = 90;
        //AddChild(wind4);

        WeirdPlank plank2 = new WeirdPlank(this, 7200, 1805);
        AddChild(plank2);

        Pushable pushable2 = new Pushable(new Vec2(6164, 1954), this);
        AddChild(pushable2);

        loader.LoadImageLayers(7);

        AddAltar(130, 1475, true);

        AddAltar(1450, 1687, true);

        Pushable pushable = new Pushable(new Vec2(240, 1575), this, false);
        AddChild(pushable);


        endScreen.alpha = 0;
    }

    void Update()   
    {
        if (ballPuzzleComplete && !bpChanges)
        {
            endScreen.scale = 2f;
            AddChild(endScreen);
            bpChanges = true;
        }
        if (bpChanges)
        {
            if (endScreen.alpha < 1)
            {
                endScreen.alpha += 0.05f;
            }

            endScreen.x = cam.x - game.width/2;
            endScreen.y = cam.y - game.height/2;
        }

        if (targetsComplete && !tChanges)
        {
            tChanges = true;

            Wind wind2 = new Wind(new Vec2(3873, 1707), 5, 17, new Vec2(0, -1.5f));
            AddChild(wind2);
        }

        //superAlt.position = player.position;

        if (player.x > 5559 && player.x < 5954 && player.y > 2220 && player.y < 2334)
        {
            specialCam1 = true;
        }
        else
        {
            specialCam1 = false;
        }
        if (!specialCam1)
        {
            if (camOffset > 320)
            {
                camOffset--;
            }
            if (camOffset < -320)
            {
                camOffset++;
            }
            if (cam.scale > 1)
            {
                cam.scale -= 0.01f;
            }
            if (yCamOffset > 0)
            {
                yCamOffset -= 0.3f;
            }
            else if (cam.scale < 1)
            {
                cam.scale = 1;
            }
            //cam.scale += 0.0001f;
            if (player.activated)
            {
                if (player.x + camOffset > game.width / 2)
                {
                    cam.x = player.x + camOffset;
                    if (player.velocity.x > 0 && camOffset < 320)
                    {
                        camOffset++;
                        if (camOffset < 0)
                        {
                            camOffset++;
                        }
                    }
                    if (player.velocity.x < 0 && camOffset > -320)
                    {
                        camOffset--;
                        if (camOffset > 0)
                        {
                            camOffset--;
                        }
                    }
                }
                else
                {
                    cam.x = game.width / 2;

                }
            }
            else
            {
                //player.mainGhost.UpdatePos();
                if (player.mainGhost.x + camOffset > game.width / 2)
                {
                    cam.x = player.mainGhost.position.x + camOffset;
                    if (camOffset > 0)
                    {
                        camOffset--;
                    }
                    else if (camOffset < 0)
                    {
                        camOffset++;
                    }
                    else
                    {
                        camOffset = 0;
                    }
                }
                else
                {
                    camOffset = 0;
                    cam.x = game.width / 2;
                }
            }


            if (player.activated)
            {
                if (player.y > game.height / 4f * 3)
                {
                    cam.y = player.y - game.height / 5f + yCamOffset;
                }
                else
                {
                    cam.y = game.height / 2f + yCamOffset;
                }
            }
            else
            {
                if (player.mainGhost.y > game.height / 4f * 3)
                {
                    cam.y = player.mainGhost.y - game.height / 5f + yCamOffset;
                }
                else
                {
                    cam.y = game.height / 2f + yCamOffset;
                }
            }

        }
        else
        {
            cam.x = player.x + camOffset;
            cam.y = player.y - game.height / 5f + yCamOffset;
            if (cam.scale < 1.4f)
            {
                yCamOffset += 0.3f;
                cam.scale += 0.002f;
            }
            if (camOffset > -500)
            {
                camOffset -= 2;
            }

        }

        bool done = true;
        foreach (Target target in targets)
        {
            if (!target.hit)
            {
                done = false;
            }

        }
        if (done)
        {
            targetsComplete = true;
        }

    }


    void AddLine(Vec2 start, Vec2 end)
    {
        LineSegment lineSegment = new LineSegment(start, end, 0x00ffffff);
        AddChild(lineSegment);
        lines.Add(lineSegment);
    }

    void ContinueLine(Vec2 end, bool drawMe = true)
    {
        if (drawMe)
        {
            LineSegment lineSegment = new LineSegment(end, contLine, 0x00ff00ff);
            AddChild(lineSegment);
            lines.Add(lineSegment);
        }
        contLine = end;
    }

    void AddAltar(float _x, float _y, bool caveVersion = false)
    {
        
        Altar altar = new Altar(new Vec2(_x, _y), caveVersion);
        AddChild(altar);
    }


    void AddLadder(float _x, float _y)
    {
        Ladder ladder = new Ladder(new Vec2(_x, _y));
        AddChild(ladder);
    }

    void AddTarget(float  _x, float _y)
    {
        Target target = new Target(new Vec2(_x, _y));
        AddChild(target);
        targets.Add(target);
    }

    void AddParalex(float off, float mod, int bgNum)
    {
        Paralex paralex = new Paralex(-off, mod, bgNum, cam);
        AddChild(paralex);
    }

}


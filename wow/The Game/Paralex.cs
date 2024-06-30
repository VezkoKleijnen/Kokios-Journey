using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
internal class Paralex : GameObject
{
    float offset;
    float modifier;
    Camera cam;
    public Paralex(float _offset, float _modifier, int bgNum, Camera _cam)
    {
        offset = _offset;
        modifier = _modifier;
        cam = _cam;

        TiledLoader loader = new TiledLoader("Tiled/level1.tmx");
        loader.autoInstance = true;
        loader.rootObject = this;
        loader.addColliders = false;

        
        loader.LoadImageLayers(bgNum);

    }

    void Update()
    {
        x = cam.x * modifier + offset;
        
    }
}


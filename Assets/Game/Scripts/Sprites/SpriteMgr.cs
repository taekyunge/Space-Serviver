using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteMgr : Singleton<SpriteMgr>
{
    [SerializeField]
    private SpriteAtlas spriteAtlas;

    public Sprite GetSprite(string name)
    {
        return spriteAtlas.GetSprite(name);
    }
}

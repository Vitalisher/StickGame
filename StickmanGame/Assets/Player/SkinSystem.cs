using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    public int id;
    public Material material;
}

public class SkinSystem : MonoBehaviour
{
    public List<Skin> faces;
    public List<Skin> skinColor;

    public SkinnedMeshRenderer playerFace;
    public SkinnedMeshRenderer playerSkin;
    

    public void ChangeFace(int id)
    {
        foreach (var face in faces)
        {
            if (face.id == id)
            {
                playerFace.material = faces[id].material;
            }
        }

    }

    public void ChangeSkinColor(int id)
    {
        foreach (var skin in skinColor)
        {
            if (skin.id == id)
            {
                playerSkin.material = skinColor[id].material;
            }
        }

    }
}

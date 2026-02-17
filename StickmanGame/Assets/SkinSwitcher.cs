using System;
using UnityEngine;

public class SkinSwitcher : MonoBehaviour
{
    public SkinnedMeshRenderer targetRenderer;
    public SkinnedMeshRenderer[] skins;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSkin(0);
        }
    }

    public void SetSkin(int index)
    {
        var skin = skins[index];

        targetRenderer.sharedMesh = skin.sharedMesh;
        targetRenderer.sharedMaterials = skin.sharedMaterials;
    }
}

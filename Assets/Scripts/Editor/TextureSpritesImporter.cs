using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextureSpritesImporter : AssetPostprocessor
{

    void OnPreprocessTexture()
    {
        TextureImporter textureImporter = (TextureImporter)assetImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.alphaIsTransparency = true;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using BepInEx;
using UnityEngine;

namespace LordAshes
{
    public partial class FileAccessPlugin : BaseUnityPlugin
    {
        public static class Image
        {
            /// <summary>
            /// Loads an Texture2D from the source
            /// </summary>
            /// <param name="source">Source to read the assetBundle contents from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            /// <returns>Texture2D object with the source contents</returns>
            public static Texture2D LoadTexture(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(FileAccessPlugin.File.ReadAllBytes(source, cacheSettings));
                return tex;
            }

            /// <summary>
            /// Loads an Texture2D from the source
            /// </summary>
            /// <param name="source">Source to read the assetBundle contents from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            /// <returns>Texture2D object with the source contents</returns>
            public static Sprite LoadSprite(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                Texture2D tex = LoadTexture(source, cacheSettings);
                return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }
    }
}

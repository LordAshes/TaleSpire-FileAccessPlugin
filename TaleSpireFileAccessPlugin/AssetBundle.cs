using System;
using System.Collections.Generic;
using System.Linq;

using BepInEx;
using UnityEngine;

namespace LordAshes
{
    public partial class FileAccessPlugin : BaseUnityPlugin
    {
        public static class AssetBundle
        {

            /// <summary>
            /// Loads an asset bundle from the source
            /// </summary>
            /// <param name="source">Source to read the assetBundle contents from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static UnityEngine.AssetBundle Load(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                return UnityEngine.AssetBundle.LoadFromMemory(FileAccessPlugin.File.ReadAllBytes(source, cacheSettings));
            }
        }
    }
}

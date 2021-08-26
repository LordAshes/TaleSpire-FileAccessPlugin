using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LordAshes
{
    [BepInPlugin(Guid, "File Access Plug-In", Version)]
    public partial class FileAccessPlugin : BaseUnityPlugin
    {
        // Plugin info
        public const string Guid = "org.lordashes.plugins.fileaccess";
        public const string Version = "1.3.1.0";

        // Content directory
        private static string dirPlugin = BepInEx.Paths.PluginPath;
        private static string dirCommon = UnityEngine.Application.dataPath.Substring(0, UnityEngine.Application.dataPath.LastIndexOf("/")) + "/TaleSpire_CustomData";

        // Configuration
        private ConfigEntry<KeyboardShortcut> triggerKey;

        // Indication if common directory exists
        private static bool useDirCommon = false;

        // Filename Cache
        private static List<string> cache = new List<string>();
        private static CacheType cacheType = CacheType.CacheCustomData;

        void Awake()
        {
            // Check to see if the common TaleSpire_CustomData exists
            useDirCommon = System.IO.Directory.Exists(dirCommon);
            Debug.Log("File Access Plugin: "+dirCommon + " " + ((useDirCommon) ? "is" : "is not") + " present");

            // Read configuration
            triggerKey = Config.Bind("Hotkeys", "Dump Asset Catalog", new KeyboardShortcut(KeyCode.Slash, KeyCode.LeftControl));

            // Cache limited files list (those in CustomData sub-folder only)
            cacheType = Config.Bind("Settings", "Cache", CacheType.CacheCustomData).Value;
            File.SetCacheTypeInternal(cacheType);
        }

        void Update()
        {
            if(StrictKeyCheck(triggerKey.Value))
            {
                Debug.Log("Asset Catalog:");
                foreach(string item in File.Catalog())
                {
                    Debug.Log(item);
                }
            }
        }

        /// <summary>
        /// Identifies if the source is a url, drive and path or a path
        /// </summary>
        /// <param name="source">Source to be examined</param>
        /// <returns>URL protcol or empty stirng if local</returns>
        public static string GetProtocol(string source)
        {
            // No colon means this is a relative path
            if (!source.Contains(":")) { return ""; }
            // Colon in the second character position means this is a drive and absolute path
            if (source.Substring(1, 1) == ":") { return ""; }
            // URL protcol
            return source.Substring(0, source.IndexOf(":"));
        }

        public enum CacheType
        {
            NoCacheFullListing = 0,
            NoCacheCustomData,
            CacheFullListing,
            CacheCustomData,
            NoChange = 999
        }

        /// <summary>
        /// Method to properly evaluate shortcut keys. 
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public bool StrictKeyCheck(KeyboardShortcut check)
        {
            if (!check.IsUp()) { return false; }
            foreach (KeyCode modifier in new KeyCode[] { KeyCode.LeftAlt, KeyCode.RightAlt, KeyCode.LeftControl, KeyCode.RightControl, KeyCode.LeftShift, KeyCode.RightShift })
            {
                if (Input.GetKey(modifier) != check.Modifiers.Contains(modifier)) { return false; }
            }
            return true;
        }
    }
}

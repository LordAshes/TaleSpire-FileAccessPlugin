using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BepInEx;
using UnityEngine;

namespace LordAshes
{
    public partial class FileAccessPlugin : BaseUnityPlugin
    {
        public static class File
        {

            /// <summary>
            /// Appends all text as a string to the source
            /// </summary>
            /// <param name="source">Source to write the text to</param>
            /// <param name="content">Content to be written</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static void AppendAllText(string source, string content, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            Debug.LogWarning("Append Is Not Supported For Non Local File Sources. Effects Will Be Same As Write.");
                            wc.UploadString(source.Replace("\\","/"), content);
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x);
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            System.IO.File.AppendAllText(Find(source, cacheSettings)[0], content);
                        }
                        else
                        {
                            // Full path reference
                            System.IO.File.AppendAllText(source, content);
                        }
                    }
                    catch (Exception x)
                    {
                        Debug.Log(x);
                    }
                }
            }

            /// <summary>
            /// Read all text as a string from the source
            /// </summary>
            /// <param name="source">Source to read the text from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            /// <returns>String content of the source</returns>
            public static string ReadAllText(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            return wc.DownloadString(source.Replace("\\","/"));
                        }
                        catch (Exception x)
                        {
                            return x.ToString();
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            return System.IO.File.ReadAllText(Find(source, cacheSettings)[0]);
                        }
                        else
                        {
                            // Full path reference
                            return System.IO.File.ReadAllText(source);
                        }
                    }
                    catch (Exception x)
                    {
                        return x.ToString();
                    }
                }
            }

            /// <summary>
            /// Writes all text as a string to the source
            /// </summary>
            /// <param name="source">Source to write the text to</param>
            /// <param name="content">Content to be written</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static void WriteAllText(string source, string content, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            wc.UploadString(source.Replace("\\", "/"), content);
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x);
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            System.IO.File.WriteAllText(Find(source, cacheSettings)[0], content);
                        }
                        else
                        {
                            // Full path reference
                            System.IO.File.WriteAllText(source, content);
                        }
                    }
                    catch (Exception x)
                    {
                        Debug.Log(x);
                    }
                }
            }

            /// <summary>
            /// Appends all text as a an array of strings to the source
            /// </summary>
            /// <param name="source">Source to write the text to</param>
            /// <param name="content">String array of the content to be written</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static void AppendAllLines(string source, string[] content, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            Debug.LogWarning("Append Is Not Supported For Non Local File Sources. Effects Will Be Same As Write.");
                            wc.UploadString(source.Replace("\\", "/"), String.Join("\r\n", content));
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x);
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if(source.Substring(1,1)!=":")
                        {
                            // Resource reference
                            System.IO.File.AppendAllLines(Find(source, cacheSettings)[0], content);

                        }
                        else
                        {
                            // Full path refernece
                            System.IO.File.AppendAllLines(source, content);
                        }

                    }
                    catch (Exception x)
                    {
                        Debug.Log(x);
                    }
                }
            }

            /// <summary>
            /// Read all text as a an array of strings from the source
            /// </summary>
            /// <param name="source">Source to read the text from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            /// <returns>String array containing each line of the source</returns>
            public static string[] ReadAllLines(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            return wc.DownloadString(source.Replace("\\", "/").Replace("\r\n", "\n")).Split('\n');
                        }
                        catch (Exception x)
                        {
                            return new string[] { x.ToString(), x.InnerException.ToString() };
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            return System.IO.File.ReadAllLines(Find(source, cacheSettings)[0]);
                        }
                        else
                        {
                            // Full path reference
                            return System.IO.File.ReadAllLines(source);
                        }
                    }
                    catch (Exception x)
                    {
                        return new string[] { x.ToString(), x.InnerException.ToString() };
                    }
                }
            }

            /// <summary>
            /// Writes all text as a an array of strings to the source
            /// </summary>
            /// <param name="source">Source to write the text to</param>
            /// <param name="content">String array of the content to be written</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static void WriteAllLines(string source, string[] content, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            wc.UploadString(source.Replace("\\", "/"), String.Join("\r\n", content));
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x);
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            System.IO.File.WriteAllLines(Find(source, cacheSettings)[0], content);
                        }
                        else
                        {
                            // Full path reference
                            System.IO.File.WriteAllLines(source, content);
                        }
                    }
                    catch (Exception x)
                    {
                        Debug.Log(x);
                    }
                }
            }

            /// <summary>
            /// Reads all bytes of the source
            /// </summary>
            /// <param name="source">Source to read the text from</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            /// <returns>Byte array containing the source</returns>
            public static byte[] ReadAllBytes(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            return wc.DownloadData(source.Replace("\\","/"));
                        }
                        catch (Exception x)
                        {
                            Debug.Log("ReadAllBytes(URL) Exception: " + x);
                            return new byte[0];
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            return System.IO.File.ReadAllBytes(Find(source, cacheSettings)[0]);
                        }
                        else 
                        {
                            // Full path reference
                            return System.IO.File.ReadAllBytes(source);
                        }
                    }
                    catch (Exception x)
                    {
                        Debug.Log("ReadAllBytes(File) Exception: " + x);
                        return new byte[0];
                    }
                }
            }

            /// <summary>
            /// Writes all text as a an array of strings from the source
            /// </summary>
            /// <param name="source">Source to wite the text to</param>
            /// <param name="content">Byte array of the content to be written</param>
            /// <param name="cacheSettings">Cache settings when source is a local file</param>
            public static void WriteAllBytes(string source, byte[] content, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                string protocol = GetProtocol(source);
                if (protocol != "")
                {
                    // URL
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        try
                        {
                            wc.UploadData(source.Replace("\\", "/"), content);
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x);
                        }
                    }
                }
                else
                {
                    // File
                    try
                    {
                        if (source.Substring(1, 1) != ":")
                        {
                            // Resource reference
                            System.IO.File.WriteAllBytes(Find(source, cacheSettings)[0], content);
                        }
                        else
                        {
                            // Full path reference
                            System.IO.File.WriteAllBytes(source, content);
                        }
                    }
                    catch (Exception x)
                    {
                        Debug.Log(x);
                    }
                }
            }

            /// <summary>
            /// Method to check if the source exist
            /// </summary>
            /// <param name="source">String source to be check</param>
            /// <returns>True if the source exists within the plugin folders or the common folder</returns>
            public static bool Exists(string source)
            {
                if (GetProtocol(source.Replace("\\","/")) == "")
                {
                    // Load file
                    if (source.Substring(1, 1) != ":")
                    {
                        // Resource reference
                        return Find(source).Length > 0;
                    }
                    else
                    {
                        // Full path reference
                        return System.IO.File.Exists(source);
                    }
                }
                else
                {
                    // URL
                    try
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            wc.DownloadData(source.Replace("\\", "/"));
                        }
                        return true;
                    }
                    catch(Exception x)
                    {
                        Debug.Log("Exists(URL) returned " + x);
                        return false;
                    }
                }
            }

            /// <summary>
            /// Finds a file in the enumerated files list
            /// </summary>
            /// <param name="source"></param>
            /// <param name="cacheSettings"></param>
            /// <returns></returns>
            public static string[] Find(string source, CacheType cacheSettings = CacheType.NoChange)
            {
                if (cacheSettings == CacheType.NoChange) { cacheSettings = cacheType; }
                if ((cacheSettings == CacheType.NoCacheFullListing) || (cacheSettings == CacheType.NoCacheCustomData) || (cacheSettings != cacheType))
                {
                    // Update cache list
                    SetCacheType(cacheSettings);
                }
                System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(System.Text.RegularExpressions.Regex.Escape(source.Replace("\\","/")), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string[] find = cache.Where<string>(item => regEx.IsMatch(item)).ToArray();
                if (find.Length != 1)
                {
                    Debug.Log("FindFile('" + source + "'," + cacheSettings + ") found "+find.Length+" results");
                }
                return find;
            }

            /// <summary>
            /// Method to set the type of local file list cache
            /// </summary>
            /// <param name="cacheSettings">Cache settings</param>
            public static void SetCacheType(CacheType cacheSettings)
            {
                // Update cache list
                cache.Clear();
                Debug.Log("Updating file list cache with plugin contents");
                GetFolders(ref cache, dirPlugin, (cacheSettings == CacheType.CacheCustomData || cacheSettings == CacheType.NoCacheCustomData));
                Debug.Log(cache.Count + " items in the file list cache");
                if (useDirCommon)
                {
                    Debug.Log("Updating files list cache with common folder contents");
                    GetFiles(ref cache, dirCommon, false);
                    Debug.Log(cache.Count + " items in the file list cache");
                }
                cacheType = cacheSettings;
            }

            /// <summary>
            /// Method to read all of the files in the files cache list
            /// </summary>
            /// <returns>Array of full filenames</returns>
            public static string[] Catalog()
            {
                List<string> entries = new List<string>();
                foreach(string item in cache)
                {
                    string entry = "";
                    if (item.Contains("TaleSpire_CustomData"))
                    {
                        entry = item.Substring(item.IndexOf("TaleSpire_CustomData")) + " (" + item.Substring(0, item.IndexOf("TaleSpire_CustomData")) + ")";
                    }
                    else  if (item.Contains("CustomData"))
                    {
                        entry = item.Substring(item.IndexOf("CustomData")) + " (" + item.Substring(0, item.IndexOf("CustomData")) + ")";
                    }
                    else
                    {
                        entry = "|" + item;
                    }
                    entries.Add(entry);
                }
                entries.Sort();
                return entries.ToArray();
            }

            /// <summary>
            /// Enumerate all folders
            /// </summary>
            /// <param name="files">List to receive the files list</param>
            /// <param name="root">Root whose folders are all processed</param>
            /// <param name="limit">Limit search to the CustomData subfolder of each folder</param>
            private static void GetFolders(ref List<string> files, string root, bool limit = false)
            {
                foreach (string dirname in System.IO.Directory.EnumerateDirectories(root))
                {
                    GetFiles(ref files, dirname, limit);
                }
            }

            /// <summary>
            /// Enumerate files in the specified directory and either all sub directories or only the CustomData sub directory.
            /// </summary>
            /// <param name="files">Reference to the list containing the results</param>
            /// <param name="root">Root directory of the enumeration</param>
            /// <param name="limit">Indicates if only the CustomData folder should be enumerated</param>
            private static void GetFiles(ref List<string> files, string root, bool limit = false)
            {
                if (!limit)
                {
                    foreach (string filename in System.IO.Directory.EnumerateFiles(root))
                    {
                        files.Add(filename.Replace("\\","/"));
                    }
                    foreach (string dirname in System.IO.Directory.EnumerateDirectories(root))
                    {
                        GetFiles(ref files, dirname, false);
                    }
                }
                else
                {
                    if (System.IO.Directory.Exists(root + "/CustomData"))
                    {
                        GetFiles(ref files, root + "/CustomData", false);
                    }
                }
            }
        }
    }
}

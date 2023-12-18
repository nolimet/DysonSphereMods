using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace CreativeStuff;

public static class AssetManager
{
    public static Dictionary<string, AssetBundle> _loadedBundles = new();

    public static void GetLoadedBundles()
    {
    }

    public static async Task LoadBundle(string bundlePath)
    {
        if (_loadedBundles.ContainsKey(bundlePath))
            return;

        if (!File.Exists(bundlePath))
            return;

        var assetBundle = await AssetBundle.LoadFromFileAsync(bundlePath);
        _loadedBundles.Add(bundlePath, assetBundle);
    }
}
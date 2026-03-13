using ICities;
using HarmonyLib;
using UnityEngine;

namespace AssetImporterIndexDebug
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private Harmony harmony;

        public override void OnLevelLoaded(LoadMode mode)
        {
            Debug.Log("[AssetImporterIndexDebug] Initializing Harmony patches...");

            harmony = new Harmony("com.assetimporter.indexdebug");
            harmony.PatchAll();

            Debug.Log("[AssetImporterIndexDebug] Harmony patches applied.");
        }
    }
}

using HarmonyLib;
using System.Reflection;
using UnityEngine;
using ColossalFramework;

namespace AssetImporterFix
{
    [HarmonyPatch]
    public static class TextureLoaderFixPatch
    {
        static MethodBase TargetMethod()
        {
            var closure = typeof(AssetImporterTextureLoader)
                .GetNestedType("<LoadTextures>c__AnonStorey0", BindingFlags.NonPublic);

            return closure.GetMethod("<>m__0", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        static void Prefix(object __instance)
        {
            try
            {
                var type = __instance.GetType();
                var field = type.GetField("results", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (field == null)
                    return;

                var arr = field.GetValue(__instance) as System.Array;

                if (arr == null)
                    return;

                // 🔍 Log enum type and array length
                var elementType = arr.GetType().GetElementType();
                Debug.Log("[AssetImporterFix] ResultType enum type = " + elementType.FullName);
                Debug.Log("[AssetImporterFix] Results array length = " + arr.Length);

                for (int i = 0; i < arr.Length; i++)
                {
                    int enumValue = (int)arr.GetValue(i);

                    // 🔍 Log raw value + enum name
                    string enumName = System.Enum.GetName(elementType, enumValue) ?? "<UNKNOWN>";
                    Debug.Log($"[AssetImporterFix] Index {i}: Raw={enumValue}, Enum={enumName}");

                    if (enumValue >= 4)
                    {
                        Debug.Log("[AssetImporterFix] Invalid ResultType " + enumValue + " detected. Resetting to RGB.");
                        arr.SetValue(System.Enum.ToObject(arr.GetType().GetElementType(), 0), i);
                    }
                }

                // ✅ Shader assignment & emissive fix
                var modelField = type.GetField("model", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (modelField != null)
                {
                    var model = modelField.GetValue(__instance) as GameObject;
                    if (model != null)
                    {
                        Shader propShader = Shader.Find("Custom/Props/Prop");
                        if (propShader != null)
                        {
                            foreach (var renderer in model.GetComponentsInChildren<Renderer>(true))
                            {
                                var mat = renderer.sharedMaterial;
                                if (mat != null)
                                {
                                    // Apply shader
                                    mat.shader = propShader;

                                    // ✅ Detect if this is a PropInfo prefab
                                    var prefabInfo = model.GetComponent<PrefabInfo>();
                                    if (prefabInfo is PropInfo)
                                    {
                                        // Set emissive flags for props
                                        mat.SetInt("_RealEmissive", 1);
                                        mat.SetInt("_EmissiveIsBlack", 0);
                                        mat.SetInt("_BakedEmissive", 0);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("[AssetImporterFix] WARNING: Could not find Custom/Props/Prop shader.");
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("[AssetImporterFix] Prefix error: " + e);
            }
        }
    }
}

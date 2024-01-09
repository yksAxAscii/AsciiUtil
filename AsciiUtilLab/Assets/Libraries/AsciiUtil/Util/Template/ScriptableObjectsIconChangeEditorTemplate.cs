using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// #if UNITY_EDITOR
// using UnityEditor;
// [CustomEditor(typeof(ScriptableObjectのクラス名))]
// public class GridPanelEditor : Editor
// {
//     public override Texture2D RenderStaticPreview
//     (
//         string assetPath,
//         Object[] subAssets,
//         int width,
//         int height
//     )
//     {
//         var obj = target as ScriptableObjectのクラス名;
//         var icon = obj.変えたいアイコンの入った変数(Sprite);

//         if (icon == null)
//         {
//             return base.RenderStaticPreview(assetPath, subAssets, width, height);
//         }

//         var preview = AssetPreview.GetAssetPreview(icon);
//         var final = new Texture2D(width, height);

//         if (preview is not null)
//         {
//             EditorUtility.CopySerialized(preview, final);
//         }
//         return final;
//     }
// }
// #endif
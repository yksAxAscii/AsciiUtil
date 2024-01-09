using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using AsciiUtil.ScriptableSystem;

namespace AsciiUtil
{
    /// <summary>
    /// シーン管理をおこなうシステム
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableSystem/AsciiSceneSystem")]
    public class AsciiSceneSystem : ScriptableObject, IScriptableSystemable
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadAddtive(string scneeName)
        {
            SceneManager.LoadScene(scneeName, LoadSceneMode.Additive);
        }

        public void AddOnSceneLoadedAction(UnityAction action)
        {
            SceneManager.sceneLoaded += (scene, mode) => action.Invoke();
        }

        public void UnLoad(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        /// <summary>
        /// どのシーンから再生してもシステム用のシーンを自動で読み込みます
        /// </summary>
#if UNITY_EDITOR
        public static void LoadSystemScene()
        {
            //システムシーンが存在しなければ処理しない
            if (SceneManager.GetSceneByName("SystemScene") == null)
            {
                return;
            }
            //現在システムシーンが読み込まれているかチェック
            var sceneCount = SceneManager.sceneCount;
            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                //読み込まれていたら何もしない
                if (scene.name == "SystemScene")
                {
                    return;
                }
            }
            SceneManager.LoadScene("SystemScene", LoadSceneMode.Additive);
        }
#endif
    }
}


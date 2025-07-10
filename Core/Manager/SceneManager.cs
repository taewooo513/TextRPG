using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Core.Manager
{
    internal class SceneManager : Singleton<SceneManager>
    {
        Dictionary<string, Scene> scenes;
        Scene nowScene = null;
        Scene nextScene = null;

        public SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
        }

        public void AddScene(string key, Scene _scene)
        {
            if (scenes.ContainsKey(key))
            {
                Debug.WriteLine("중복키");
            }
            else
            {
                scenes.Add(key, _scene);
            }
        }

        public void ChangeScene(string key)
        {

            foreach (KeyValuePair<string, Scene> pair in scenes)
            {
                if (pair.Key.Equals(key))
                {
                    nextScene = pair.Value;
                }
            }
        }
        public void Update()
        {
            if (nextScene != null)
            {
                if (nowScene != null)
                {
                    nowScene.Release();
                }
                nextScene.Init();
                nowScene = nextScene;
                nextScene = null;
            }
            if (nowScene != null)
            {
                nowScene.Update();
            }
        }
    }
}

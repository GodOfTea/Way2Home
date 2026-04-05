using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            Debug.Log($"LoadScene {nextScene} & {SceneManager.GetActiveScene().name}");

            /* TODO: Раскоментировать перед билдом */
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(nextScene);

            while (!loadSceneAsync.isDone)
            {
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}
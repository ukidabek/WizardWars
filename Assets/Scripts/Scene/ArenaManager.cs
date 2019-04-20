using BaseGameLogic.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArenaManager : SingletonMonoBehaviour<ArenaManager>
{
    [SerializeField] private List<string> scenesNames = new List<string>();
    private Scene loadedArena;

    public UnityEvent OnSceneLoadedEvent = new UnityEvent();

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        AsyncOperation asyncOperation = null;
        if (scene.buildIndex > 0)
        {
            asyncOperation = SceneManager.UnloadSceneAsync(scene.name);
            yield return new WaitUntil(() => asyncOperation.isDone);
        }

        asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        loadedArena = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(loadedArena);
        OnSceneLoadedEvent.Invoke();
    }

    public void AddScene(string name)
    {
        scenesNames.Add(name);
    }

    public void LoadRandomArena()
    {
        int index = UnityEngine.Random.Range(0, scenesNames.Count);
        LoadedArena(index);
    }

    private void LoadedArena(int index)
    {
        StartCoroutine(LoadSceneCoroutine(scenesNames[index]));
    }
}

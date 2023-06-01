using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : Singleton<SceneLoader>
{
    private void Start()
    {
        LoadScene("Lobby");
    }

    public void LoadScene(string sceneName, float delay = 0)
    {
        StartCoroutine(LoadSceneIE(sceneName, delay));
    }
    private IEnumerator LoadSceneIE(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}


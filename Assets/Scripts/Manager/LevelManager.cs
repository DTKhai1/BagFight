using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour, IUpdateObserver
{
    public GameObject _loadingScreen;
    public Slider _loadingBar;
    public float _target;
    public async Task LoadLevel(string sceneName)
    {
        _loadingBar.value = 0f;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loadingScreen.SetActive(true);
        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        } while (scene.progress < 0.9f);
        await Task.Delay(1000); // Optional delay for loading screen
        scene.allowSceneActivation = true;
        _loadingScreen.SetActive(false);
    }
    public async void ChangeScene(string SceneName)
    {
        UpdateManager.RegisterUpdateObserver(this);
        await LoadLevel(SceneName);
        UpdateManager.UnregisterUpdateObserver(this);
    }
    public void ObservedUpdate()
    {
        _loadingBar.value = Mathf.MoveTowards(_loadingBar.value, _target, Time.deltaTime);
    }
}

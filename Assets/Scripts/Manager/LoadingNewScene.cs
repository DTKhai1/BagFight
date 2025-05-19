using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingNewScene : MonoBehaviour, IUpdateObserver
{
    public GameObject _loadingScreen;
    public Slider _loadingBar;

    public float _target;
    public static string _nextScene;
    private bool _loadingComplete;
    private void Start()
    {
        _loadingScreen?.SetActive(true);
        if (_loadingBar != null)
            _loadingBar.value = 0f;
        if (!string.IsNullOrEmpty(_nextScene))
        {
            LoadLevel(_nextScene);
        }
    }
    public async Task LoadLevel(string sceneName)
    {
        _loadingComplete = false;
        if (sceneName != SceneName.Play)
        {
            _loadingBar.value = 0f;
            _target = 0f;
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            do
            {
                await Task.Delay(100);
                _target = scene.progress;
            } while (scene.progress < 0.9f);
            scene.allowSceneActivation = true;
            _loadingComplete = true;
        }
        else
        {
            _loadingBar.value = 0f;
            _target = 0f;

            List<AsyncOperation> operations = new List<AsyncOperation>();

            var mainScene = SceneManager.LoadSceneAsync(sceneName);
            mainScene.allowSceneActivation = false;
            operations.Add(mainScene);
            var uiScene = SceneManager.LoadSceneAsync(SceneName.UI, LoadSceneMode.Additive);
            uiScene.allowSceneActivation = false;
            operations.Add(uiScene);

            var sceneOperations = operations.ToArray();
            float totalProgress = 0f;
            do
            {
                await Task.Delay(100);
                foreach (var op in sceneOperations)
                {
                    totalProgress += op.progress;
                    Debug.Log("progress: " + totalProgress);
                }
                _target = totalProgress / sceneOperations.Length;
                Debug.Log("target: " + _target);
            }
            while (_target < 0.9f);

            foreach (var op in sceneOperations)
            {
                op.allowSceneActivation = true;
            }
        }
    }
    public async void ChangeScene(string SceneName)
    {
        UpdateManager.RegisterUpdateObserver(this);
        await LoadLevel(SceneName);
        UpdateManager.UnregisterUpdateObserver(this);
    }
    public void ObservedUpdate()
    {
        _loadingBar.value = Mathf.MoveTowards(_loadingBar.value, _target, Time.deltaTime * 10);
    }
}

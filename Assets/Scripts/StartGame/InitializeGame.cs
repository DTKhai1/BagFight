//using UnityEngine;
//using UnityEngine.SceneManagement;
//using System.Threading.Tasks;

//public class GameC : MonoBehaviour
//{
//    public static GameC Instance { get; private set; }

//    private string currentSceneName;
//    private bool isLoading;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public async void ChangeScene(string sceneName)
//    {
//        if (isLoading || sceneName == currentSceneName) return;
//        isLoading = true;

//        try
//        {
//            // Store the target scene name
//            LoadingNewScene.nextScene = sceneName;

//            // Load the loading scene
//            SceneManager.LoadScene(SceneName.LoadingScene);

//            currentSceneName = sceneName;
//        }
//        finally
//        {
//            isLoading = false;
//        }
//    }
//}
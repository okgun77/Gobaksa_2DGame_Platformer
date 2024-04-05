using UnityEngine.SceneManagement;

public enum SceneNames { Intro = 0, SelectLevel, Game }

public static class Utils
{
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static void LoadScene(string _sceneName = "")
    {
        if (_sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    public static void LoadScene(SceneNames _sceneName)
    {
        // SceneNames 열거형으로 매개변수를 받아온 경우 ToString() 처리
        SceneManager.LoadScene(_sceneName.ToString());
    }
}

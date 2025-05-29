using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public void Launch()
    {
        SceneManager.LoadScene(2);
    }
}

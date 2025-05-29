using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("SceneLoader", 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SceneLoader()
    {
        SceneManager.LoadScene("Start menu");
    }
}

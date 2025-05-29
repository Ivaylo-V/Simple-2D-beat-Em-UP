using UnityEngine;

public class Quit : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // This will only show in the Unity Editor
    }
}

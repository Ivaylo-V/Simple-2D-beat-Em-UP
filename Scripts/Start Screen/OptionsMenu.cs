using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _optionsUI;


    public void OptionsClick()
    {
        _optionsUI.SetActive(true);
    }

    public void OptionsClickExit()
    {
        _optionsUI.SetActive(false);
    }
}

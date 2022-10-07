using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] private int gamelevel;
    [SerializeField] private TextMeshProUGUI textlevel;
    public void GameOver()
    {

        //gameoverScreen.SetActive(true);
    }
    private void Start()
    {
        if(textlevel != null)
        textlevel.text = gamelevel.ToString();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level "+ gamelevel.ToString());
    }

    public void LoadNextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        print(next);
        SceneManager.LoadScene("Level "+ next.ToString());

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject EndGameScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.SetActive(false);

            EndGameScene.SetActive(true);
            if (PlayerPrefs.GetInt("levelat") <= SceneManager.GetActiveScene().buildIndex)
                PlayerPrefs.SetInt("levelat", SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}





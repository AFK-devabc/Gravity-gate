using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    void Start()
    {
        int levelat = PlayerPrefs.GetInt("levelat");
        Debug.Log(levelat);
        for(int i = 0; i < levelat && i < levels.Length; i++)
            levels[i].interactable = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> Birds;
    public List<Pig> Pigs;
    public static GameManager _instance;
    private Vector3 originPos; // 初始位置
    public GameObject Win;
    public GameObject Lose;
    public GameObject[] Stars;

    private int starsNum = 0;
    public int levelNum = 16; // 总关数

    private void Awake()
    {
        _instance = this;
        if (Birds.Count > 0)
        {
            originPos = Birds[0].transform.position;
        }

    }
    void Start()
    {
        Initialized();
    }


    // 初始化小鸟
    private void Initialized()
    {
        for(int i = 0; i < Birds.Count; i++)
        {
            Birds[0].transform.position = originPos;
            Birds[i].enabled = (i == 0);
            Birds[i].sp.enabled = (i == 0);
            Birds[i].canMove = (i == 0);
        }
    }

    public void NextBird()
    {
        if (Pigs.Count > 0)
        {
            if (Birds.Count > 0)
            {
                // 下一只
                // Debug.Log("下一只");
                Initialized();
            }
            else
            {
                // 输了
                // Debug.Log("输了");
                Lose.SetActive(true);
            }
        }
        else
        {
            // 赢了
            // Debug.Log("赢了");
            Win.SetActive(true);
        }
    }

    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for (; starsNum <= Birds.Count; starsNum++)
        {

            if (starsNum >= Stars.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            Stars[starsNum].SetActive(true);
        }
        SaveData();
    }


    // 
    public void Replay()
    {
        SceneManager.LoadScene(2);
    }


    // 返回主界面
    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"), 0) < starsNum) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        int sum = 0;
        for (int i = 1; i <= levelNum; i++)
        {
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}

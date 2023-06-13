using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator Anim;
    public GameObject Button;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    // 重新游戏 
    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 暂停动画
    public void Pause()
    {
        Anim.SetBool("isPause", true); // 暂停动画
        Button.SetActive(false); // 隐藏按钮

        if (GameManager._instance.Birds.Count >0)
        {
            if (GameManager._instance.Birds[0].isReleased == false)
            {
                GameManager._instance.Birds[0].canMove = false;
            }
        }
    }

    // 继续动画
    public void Resume()
    {
        Time.timeScale = 1; // 继续窗口
        Anim.SetBool("isPause", false); // 继续动画

        if (GameManager._instance.Birds.Count > 0)
        {
            if (GameManager._instance.Birds[0].isReleased == false)
            {
                GameManager._instance.Birds[0].canMove = true;
            }
        }
    }


    // 返回菜单
    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0; // 暂停窗口
    }

    public void ResumeAnimEnd()
    {
        Button.SetActive(true);
    }



}

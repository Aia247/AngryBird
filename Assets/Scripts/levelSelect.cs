using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    private bool isSelect = false;
    public Sprite levelBG;
    public Sprite oneStar;
    public Sprite twoStars;
    public Sprite threeStars;
    private Image showImage;
    private Image starsImage;



    private void Awake()
    {
        showImage = GetComponent<Image>();
        starsImage = transform.GetChild(1).GetComponentInChildren<Image>();
    }


    void Start()
    {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else
        {
            // 判断关卡是否解锁
            int beforeNum = int.Parse(gameObject.name) - 1;
            if (PlayerPrefs.GetInt("level" + beforeNum.ToString()) > 0)
            {
                isSelect = true;
            }
        }
        if (isSelect)
        {
            showImage.overrideSprite = levelBG;
            transform.Find("Num").gameObject.SetActive(true);
            transform.Find("Stars").gameObject.SetActive(true);

            int count = PlayerPrefs.GetInt("level" + gameObject.name);// 获取当前关卡星星数
            if (count == 1)
            {
                starsImage.sprite = oneStar;
            }else if (count == 2)
            {
                starsImage.sprite = twoStars;
            }
            else if (count == 3)
            {
                starsImage.sprite = threeStars;
            }
        }
    }

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
            
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputField story;
    public GameObject storyInput;
    //public Transform mouseTrans;
    public Camera mainCam;
    public Transform contentTrans;
    public Transform barImg;

    public InputField nameInput;

    public int storynum;
    public int storynum_lacal;

    public string u_name;
    public List<string> u_text = new List<string>();

    public firebaseMng FM;

    
    void Start()
    {
        // Cursor.visible = false;
        if (!PlayerPrefs.HasKey("u_storynum")) //키가없다면
        {
            PlayerPrefs.SetInt("u_storynum", 0);
        }
        else //키있으면
        storynum = PlayerPrefs.GetInt("u_storynum");
        storynum_lacal = 0;
    }


    private void Update()
    {
        if(contentTrans.position.x<=-1000&&contentTrans.position.x>=-2000)
        {
            barImg.transform.localPosition = new Vector3(4, -11, 0);
        }else if(contentTrans.position.x <=0 && contentTrans.position.x >= -1000)
        {
            barImg.transform.localPosition = new Vector3(-227, -11, 0);

        }
        else
        {
            barImg.transform.localPosition = new Vector3(223, -11,  0);

        }
        //mouseTrans.position = Input.mousePosition;
    }


    public void OnclickPlus()
    {
        storyInput.SetActive(true);
        story.text = "";
    }
    
    public void OnclickSave()
    {
        string convertStroy = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(story.text));
        PlayerPrefs.SetString("Stroy", convertStroy);
        u_text.Add(story.text);
        storyInput.SetActive(false);
        FM.SetLocation();
        storynum++;
        storynum_lacal++;
        PlayerPrefs.SetInt("u_storynum", storynum);
    }

    public void OnInputName()
    {
        u_name = nameInput.text;
    }
}


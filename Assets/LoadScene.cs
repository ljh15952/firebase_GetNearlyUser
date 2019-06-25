using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LogoScene");
        
    }

    IEnumerator LogoScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("SampleScene");
    }

}

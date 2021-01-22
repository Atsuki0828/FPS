using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    int PlayerHP = 100;
    public static bool Amotext = false;
    public GameObject Exploson7;
    int Gunschange = 0;
    public static int changegun = 0;
    int gunsamonokori = 0;

    //-------------- 考えてね① ---------------------
    int timeCount;
    //-------------- 考えてね① ---------------------

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Gunschange == 0)
            {
                Gunschange = 1;
                changegun = 1;

            }
            else
            {
                Gunschange = 0;
                changegun = 0;
            }
        }

        //セミオートで打つ場合
        if (Gunschange == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SendMessage("Shot");
                //撃つ作業
            }
        }

        //フルオートで打つ場合
        if (Gunschange == 1)
        {
            //-------------- 考えてね② ---------------------
            timeCount += 1;
            if (Input.GetMouseButton(0))
            {
                if(timeCount % 5 == 0)
                {
                    SendMessage("AutoShot");
                }
            }
            //-------------- 考えてね② ---------------------
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SendMessage("Reload");
            //リロード作業
        }

        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Debug.DrawLine(transform.position, center, Color.red);
    }

    public void PlayerDamage()
    {
        PlayerHP -= 10;
        Debug.Log(PlayerHP);
        ScoreScript.HPcount -= 10;
        if (PlayerHP <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    void Shot()
    //撃つ作業説明文
    {
        int distance = 500;
        Vector3 center;
        center = new Vector3(Screen.width / 2 + Random.Range(-15, 15), Screen.height / 2 + Random.Range(-15, 15), 0);
        Ray ray = playerCamera.ScreenPointToRay(center);
        Debug.DrawLine(transform.position, center, Color.red);
        RaycastHit hitInfo;

        if (ScoreScript.Amocount > 0)
        {
            ScoreScript.Amocount -= 1;
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                Instantiate(Exploson7, hitInfo.point, Quaternion.identity);
                if (hitInfo.collider.tag == "Enemy")
                {
                    hitInfo.collider.SendMessage("Damage");
                }
                if (hitInfo.collider.tag == "HeadShot")
                {
                    hitInfo.collider.transform.root.gameObject.SendMessage("HeadDamage");
                }
            }
            else
            {
                Amotext = true;
            }
        }
    }

    //フルオートの場合に使用するメソッド
    void AutoShot()
    //撃つ作業説明文
    {
        int distance = 500;
        Vector3 center;
        center = new Vector3(Screen.width / 2 + Random.Range(-40, 40), Screen.height / 2 + Random.Range(-40, 40), 0);
        Ray ray = playerCamera.ScreenPointToRay(center);
        Debug.DrawLine(transform.position, center, Color.red);
        RaycastHit hitInfo;
        if (ScoreScript.Amocount > 0)
        {
            ScoreScript.Amocount -= 1;
            if (Physics.Raycast(ray, out hitInfo, distance))
            {
                Instantiate(Exploson7, hitInfo.point, Quaternion.identity);
                if (hitInfo.collider.tag == "Enemy")
                {
                    hitInfo.collider.SendMessage("Damage1");
                }
                if (hitInfo.collider.tag == "HeadShot")
                {
                        hitInfo.collider.transform.root.gameObject.SendMessage("HeadDamage1");
                }
            }

            else
            {
                Amotext = true;
            }
        }
    }

    void Reload()
    {
        //指定したスクリプトを無効化
        // FPS = FPS.GetComponent<FirstPersonController>();
        // FPS.enabled = false;

        //３秒後に無効にしたスクリプトを有効化
        // Invoke("Restart", 1.0f);
        if (ScoreScript.Allamocount >= 30)
        {
            gunsamonokori = ScoreScript.Amocount;
            ScoreScript.Amocount = 30;
            ScoreScript.Allamocount -= 30 - ScoreScript.Amocount;
        } else
        {
            ScoreScript.Amocount = ScoreScript.Amocount + ScoreScript.Allamocount;
            ScoreScript.Amocount = ScoreScript.Allamocount;
            ScoreScript.Allamocount = 0;
        }


    }


}

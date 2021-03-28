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
    int timeCount;
    //bool gunBool = false;
    public AudioClip sound1;
    AudioSource audioSource;
    int[] magazines = new int[] { 0, 30, 30 };
    int current_magazine = 0;
    

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log(magazines[current_magazine]);


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
        //セミオートで撃つ場合
        if (Gunschange == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SendMessage("Shot");
                //撃つ作業
            }
        }
        if (Gunschange == 1)
        {
            timeCount += 1;
            if (Input.GetMouseButton(0))
            {
                if (timeCount % 2 == 0)
                {
                    SendMessage("AutoShot");
                }
            }
            //撃つ作業
        
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SendMessage("Reload");
            //リロード作業
        }
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Debug.DrawLine(transform.position, center, Color.red);
        
        ScoreScript.Allamocount = magazines[0] + magazines[1] + magazines[2];
        magazines[current_magazine] = ScoreScript.Amocount;

        if (ScoreScript.Amocount == 0)
        {
            Amotext = true;
        }
        else
        {
            Amotext = false;
        }
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
        if (Gunschange == 0)
        {
            center = new Vector3(Screen.width / 2 + Random.Range(-15, 15), Screen.height / 2 + Random.Range(-15, 15), 0);
        }
        else
        {
            center = new Vector3(Screen.width / 2 + Random.Range(-40, 40), Screen.height / 2 + Random.Range(-40, 40), 0);
        }
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
                    if (Gunschange == 0)
                    {
                        hitInfo.collider.SendMessage("Damage");
                    }
                    else
                    {
                        hitInfo.collider.SendMessage("Damage1");
                    }

                }
                if (hitInfo.collider.tag == "HeadShot")
                {
                    if (Gunschange == 0)
                    {
                        hitInfo.collider.transform.root.gameObject.SendMessage("HeadDamage");
                    }
                    else
                    {
                        hitInfo.collider.transform.root.gameObject.SendMessage("HeadDamage1");
                    }
                }
            }
        }
    }
    void AutoShot()
    //撃つ作業説明文
    {
        int distance = 500;
        Vector3 center;
        center = new Vector3(Screen.width / 2 + Random.Range(-40, 40), Screen.height / 2 + Random.Range(-40, 40), 0);
        Ray ray = playerCamera.ScreenPointToRay(center);
        Debug.DrawLine(transform.position, center, Color.red);
        audioSource = GetComponent<AudioSource>();
        RaycastHit hitInfo;
        if (ScoreScript.Amocount > 0)
        {
            audioSource.PlayOneShot(sound1);
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
        }
    }
    void Reload()
    {

        //if (ScoreScript.Allamocount + ScoreScript.Amocount >= 30)
        //{
        //    ScoreScript.Allamocount -= 30 - ScoreScript.Amocount;
        //    ScoreScript.Amocount = 30;

        //} else
        //{

        //    ScoreScript.Amocount = ScoreScript.Amocount + ScoreScript.Allamocount;

        //    ScoreScript.Allamocount = 0;
        //}
        

        
        if (ScoreScript.Allamocount > 0)
        {
            if (current_magazine == 2)
            {
                current_magazine = 0;
            }
            else
            {
                current_magazine += 1;
            }
            if (magazines[current_magazine] == 0)
            { 
                current_magazine += 1;
            }
            if (magazines[current_magazine] == 0)
            {
                current_magazine += 1;
            }
        

        }
        ScoreScript.Amocount = magazines[current_magazine];

        
        
    }
            
        
}

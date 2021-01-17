using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    //private FirstPersonController FPS;
    public Camera playerCamera;
    int PlayerHP = 100;
    //public int GunsAmo = 30;
    public static bool Amotext = false;
    public GameObject Exploson7;
    int Gunschange = 0;
    public static int changegun = 0;
    int gunsamonokori = 0;

    bool gunBool = false;
    //RaycastHit Hit;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;


    }

    private IEnumerator Fullauto()
    {
        if (gunBool)
        {
            for (int i = 0; i < 1000; i++)
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

                    else
                    {
                        Amotext = true;
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
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
            if (Input.GetMouseButtonDown(0))
            {
                //StartCoroutine("Fullauto");
                gunBool = true;
                StartCoroutine("Fullauto");
                //撃つ作業
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
        if (Input.GetMouseButtonUp(0))
        {
            gunBool = false;
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
            //GunsAmo = 30;
            ScoreScript.Amocount = 30;
            ScoreScript.Allamocount -= 30 - ScoreScript.Amocount;
        } else
        {

            ScoreScript.Amocount = ScoreScript.Amocount + ScoreScript.Allamocount;
            ScoreScript.Amocount = ScoreScript.Allamocount;
            ScoreScript.Allamocount = 0;
        }
        

    }
            // void Restart()
            //  {
            //     FPS.enabled = true;
            //  }
        
}

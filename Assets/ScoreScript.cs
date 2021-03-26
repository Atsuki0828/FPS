using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scorelabel;
    public Text HPlabel;
    public Text Amolabel;
    public Text Allamolabel;
    public Text NoAmolabel;
    public Text ChangeGunlabel;
    public static int enemycount = 0;
    public static int HPcount = 100;
    public static int Amocount = 0;
    public static int Allamocount = 60;
    
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        scorelabel.text = enemycount.ToString();
        HPlabel.text = HPcount.ToString();
        Amolabel.text = Amocount.ToString();
        Allamolabel.text = Allamocount.ToString();
        if (PlayerController.Amotext == true)
        {
            NoAmolabel.text = "弾がない!!";
            if (Input.GetKey(KeyCode.R))
            {
                PlayerController.Amotext = false;
                NoAmolabel.text = "";
            }
        }
        if (PlayerController.changegun == 0)
        {
            ChangeGunlabel.text = "セミオート";
        }
        else
        {
            ChangeGunlabel.text = "フルオート";
        }
        
    }
}

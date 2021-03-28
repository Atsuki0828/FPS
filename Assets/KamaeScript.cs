using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KamaeScript : MonoBehaviour
{
    public GameObject[] parts;
    float m_fieldOfView;
    float d_fieldOfView;
    bool kamaehantei;
    // Start is called before the first frame update
    void Start()
    {
        m_fieldOfView = 60.0f;
        d_fieldOfView = 9.0f;
        kamaehantei = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.K))
        {
            kamaehantei = !kamaehantei;
            if (kamaehantei)
                SendMessage("Kamae");

            else
            {
                SendMessage("KamaeCancel");



            }

        }


    }
    void Kamae()
    {
        
        //for (int i=0; i < 14; i++) {
        //    parts[i].SetActive(false);
        //}
        Camera.main.fieldOfView = d_fieldOfView;
        


    }
    void KamaeCancel()
    {
        Camera.main.fieldOfView = m_fieldOfView;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TopKontrol : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public TextMeshProUGUI zaman, can, durum;
    private Rigidbody rg;
    public float Hiz = 1.5f;
    float zamanSayaci = 35;
    int canSayaci = 11;
    bool oyunDevam = true;
    bool oyunTamam = false;

    void Start()
    {
        rg = GetComponent<Rigidbody> ();
    }


    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if(!oyunTamam){ 
            durum.text = "Oyun Tamamlanamadý";
            btn.gameObject.SetActive(true);
        }
        
        if (zamanSayaci < 0)
        oyunDevam = false;
        
    }

    void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * Hiz);
        } else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision cls)
    {
        string objIsmi = cls.gameObject.name;
        if(objIsmi.Equals("bitis"))
        {
            oyunTamam = true;
            durum.text = "Oyun Tamamlandý. Tebrikler";
            btn.gameObject.SetActive(true);
        }
        else if(!objIsmi.Equals("labirent zemini") && !objIsmi.Equals("zemin"))
        {
            canSayaci -= 1;
            can.text = canSayaci + "";
            if (canSayaci == 0) oyunDevam = false;
        }
    }
}

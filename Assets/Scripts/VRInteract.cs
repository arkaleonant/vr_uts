using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRInteract : MonoBehaviour
{
    public Image imgGaze; //untuk menyimpan gambar loading pada cursor ketika gaze
    public float totalTime = 2; //waktu yg diperlukan utk full loading gaze
    bool gvrStatus; //boolean utk cek apakah sedang hover cursor atau tidak
    float gvrTimer = 0; //inisialisasi counter waktu
    bool tertangkap = false;
    public GameObject tangan;
	public GameObject barang;

    public int distanceOfRay = 70; //jarak raycast, atau titik cursor dari camera ke objek
    private RaycastHit _hit; //untuk menerima objek apa yang terkena raycast

    // Vector3 posisiBenda;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep; //agar layar HP tidak sleep
        // posisiBenda = _hit.transform.position;
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //mengeluarkan raycast dari titik tengah camera
                                                                               //Click Input
        if (Physics.Raycast(ray, out _hit, distanceOfRay))
        { //apabila ada sesuatu dari raycast
            if (Input.GetButtonDown("Fire1") && _hit.transform.CompareTag("Enemy"))
            { //apabila sesuatu itu adalah tag enemy dan kita menekan	tombol	
                Destroy(_hit.transform.gameObject); //destroy sesuatu itu
            }

            else if (Input.GetButtonDown("Fire1") && _hit.transform.CompareTag("Barang") && !tertangkap)
            {
                _hit.transform.SetParent(tangan.transform);
                _hit.transform.localPosition = new Vector3(0f, -0.672f, 0f);
                tertangkap = true;
                Debug.Log("Tertangkap: " + tertangkap);
            }

            else if (Input.GetButtonDown("Fire1") && tertangkap)
            {
                barang.transform.SetParent(null);
                barang.transform.localPosition = _hit.transform.position;
                tertangkap = false;
                Debug.Log("Tertangkap: " + tertangkap);
            }
        }

        
		//dibawah ini adalah fungsi untuk gaze input & teleport		
        if(gvrStatus){ //mengecek apakah cursor sedang hover
			gvrTimer += Time.deltaTime; //counter waktu secara real
			imgGaze.fillAmount = gvrTimer/totalTime; //mengisi & animasi fill pada gambar imggaze
		}
				
		//Gaze Input		
		if(Physics.Raycast(ray, out _hit, distanceOfRay)){ //apabila raycast mengenai sesuatu
			if(imgGaze.fillAmount == 1 && _hit.transform.CompareTag("Teleport")){ //apabila loading gaze sudah selesai dan objek sesuatu itu adalah tag teleport					
				_hit.transform.gameObject.GetComponent<Teleport>().TeleportPlayer(); //jalankan fungsi TeleportPlayer yang terdapat pada objek tersebut
				
			}
		}
		
    }

    //fungsi public dibawah ini utk dipakaikan pada eventsystem ketika cursor hover out dan hover in
    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }
}

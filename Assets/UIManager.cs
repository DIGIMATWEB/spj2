using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Networking;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Text;
using UnityEngine.Video;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform inicioScreen, regiterScreen, emailScreen, bannerScreen;// mainMenu, carShopMenu, powerShopMenu; esto era para el twin que no sirve en webgl

    [Header("ObjectsVisibility")]
    public GameObject menu1;
    public GameObject menu2;
    public GameObject menu3;
    public GameObject menu4;
    public GameObject menu5;


    [Header("Fields Login")]
    public TMP_InputField Usr;
    public TMP_InputField Pass;



    [Header("Fields Registro")]
    public TMP_InputField nombre;// campo de texto para introduci datos de nombre 
    public TMP_InputField apellido;
    public TMP_InputField pais;// campo de texto para introduci datos de nombre 
    public TMP_InputField email;
    public TMP_InputField contraseña;
    public Toggle terms;
    //[Header("VideoPlayerComponent")]

   // public RawImage imageVideoPlayer;
    //public VideoPlayer videoplayerC;
    //public GameObject videoRect;

    [Header("Home")]
    public Toggle onoff;
    public GameObject Background;
    public GameObject VideoPlayer1;
    public GameObject Instrucciones;
    public GameObject practica;
    public GameObject juego;
    public GameObject topPlayers;
    //public GameObject DisfrutaLaPelicula;

    // public GameObject instructionsBanner;

    private string dbname, dbapellido, dbpais, dbemail, dbcontraseña, dbterminos,dbUsr,dbPass;
    private bool isOn;

    void Start()
    {
        menu1.SetActive(true);
        //inicioScreen.DOAnchorPos(Vector2.zero, 0.2f);   doTween noirve con el menu
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loginButton()
    {
        if (Usr.text != "" && Pass.text != "")
        {
            dbUsr = Usr.text;
            dbPass = Pass.text;

            StartCoroutine("Login");
        }
      
        //inicioScreen.DOAnchorPos(new Vector2(-1400,0),0.25f);
        //bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void registerButton()
    {
        menu2.SetActive(true);
        menu1.SetActive(false);
        //inicioScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
        //regiterScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void sendRegisterButton()
    {
       // logValidation();
      

       if( nombre.text != "" && apellido.text!= "" && pais.text != "" && email.text != "" && contraseña.text != "" && terms.isOn != false)
        {
            //   Debug.Log("Fields void   " + nombre.text + " | " + apellido.text + " | " + pais.text + " | " + email.text + " | " + contraseña.text + " | " + terms.isOn);
            dbname = nombre.text;
            dbapellido = apellido.text;
            dbpais = pais.text;
            dbemail = email.text;
            dbcontraseña = contraseña.text;
           
            if (terms==true)
            {
                dbterminos = "Y";
            }
            
            StartCoroutine("myPost");
         //   StartCoroutine("emailPost");
           
        }
       
    }
    public void bannerScreenButton()
    {
        menu4.SetActive(true);
        menu3.SetActive(false);
       // emailScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
       // bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    IEnumerator myPost()
    {
        WWWForm form = new WWWForm();
        form.AddField("usuario", dbname);
        form.AddField("apellido", dbapellido);
        form.AddField("pais", dbpais);
        form.AddField("email", dbemail);
        form.AddField("pass", dbcontraseña);
        form.AddField("terms", dbterminos);
        UnityWebRequest www = UnityWebRequest.Post("https://anotaconspacejam.com/register.php", form);
       // www.chunkedTransfer = false;////ADD THIS LINE
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            menu5.SetActive(true);
            menu2.SetActive(false);
            
            //   regiterScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
            //  bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
        }
    }

    IEnumerator Login()
    {

        WWWForm form = new WWWForm();
        form.AddField("name", dbUsr);
        form.AddField("password", dbPass);
        UnityWebRequest www = UnityWebRequest.Post("https://anotaconspacejam.com/test/login.php", form);
        // www.chunkedTransfer = false;////ADD THIS LINE
       
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            Debug.Log("Form upload complete!");
            Debug.Log("POST successful!");
            StringBuilder sb = new StringBuilder();
            foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
            {
                sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
            }

            // Print Headers
            Debug.Log("  Header " + sb.ToString());

            // Print Body
            Debug.Log("  body "+www.downloadHandler.text);
           if(www.downloadHandler.text.Contains("NO Existe"))
            {
             

            }
            else
            {
                menu5.SetActive(true);
                menu1.SetActive(false);
            }


        }

    }
    IEnumerator emailPost()
    {

        WWWForm form = new WWWForm();
        form.AddField("email", dbUsr);
        form.AddField("asunto", dbPass);
        UnityWebRequest www = UnityWebRequest.Post("https://anotaconspacejam.com/test/email.php", form);
        // www.chunkedTransfer = false;////ADD THIS LINE

        yield return www.SendWebRequest();

    }
        public void logValidation()
    {
        Debug.Log("Fields void   " + nombre.text + " | " + apellido.text + " | " + pais.text + " | " + email.text + " | " + contraseña.text + " | " + terms.isOn);
    }



    public void instrucciones()
    {

        isOn = onoff.isOn;
        Debug.Log(isOn);
        if (isOn)
        {
            Instrucciones.SetActive(true);
            VideoPlayer1.GetComponentInChildren<VideoPlayer>().Stop();
            VideoPlayer1.SetActive(false);
            isOn = false;

        }
        else
        {
            Instrucciones.SetActive(false);
            VideoPlayer1.SetActive(true);
            VideoPlayer1.GetComponentInChildren<VideoPlayer>().Play();
            isOn = true;
        }
    }

    public void enjoytheMovie()
    {
        menu4.SetActive(true);
        menu5.SetActive(false);
    }

    public void topmyPlayers()
    {
        VideoPlayer1.GetComponentInChildren<VideoPlayer>().Stop();
        VideoPlayer1.SetActive(false);
        topPlayers.SetActive(true);
    }
    public void practiceInGame()
    {
        Instrucciones.SetActive(false);
        Background.SetActive(false);
        VideoPlayer1.GetComponentInChildren<VideoPlayer>().Stop();
        VideoPlayer1.SetActive(false);
        practica.SetActive(true);

    }



}

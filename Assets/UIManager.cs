using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Networking;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform inicioScreen, regiterScreen, emailScreen, bannerScreen;// mainMenu, carShopMenu, powerShopMenu;
   
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

    void Start()
    {
        inicioScreen.DOAnchorPos(Vector2.zero, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loginButton()
    {
        inicioScreen.DOAnchorPos(new Vector2(-1400,0),0.25f);
        bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void registerButton()
    {
        inicioScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
        regiterScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }

    public void sendRegisterButton()
    {
       // logValidation();
      

       if( nombre.text != "" && apellido.text!= "" && pais.text != "" && email.text != "" && contraseña.text != "" && terms.isOn != false)
        {
            //   Debug.Log("Fields void   " + nombre.text + " | " + apellido.text + " | " + pais.text + " | " + email.text + " | " + contraseña.text + " | " + terms.isOn);
            StartCoroutine("myPost");
        }
       
    }
    public void bannerScreenButton()
    {
        emailScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
        bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    IEnumerator myPost()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("nombrePrueba"));
        UnityWebRequest www = UnityWebRequest.Post("https://anotaconspacejam.com/register.php", formData);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
             regiterScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
             emailScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
        }
    }

        public void logValidation()
    {
        Debug.Log("Fields void   " + nombre.text + " | " + apellido.text + " | " + pais.text + " | " + email.text + " | " + contraseña.text + " | " + terms.isOn);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataInserter : MonoBehaviour
{
    public InputField NombreUsuario;// campo de texto para introduci datos de nombre 
    public InputField EmailUsuario;//...

    public string inputName;  //variable para igualar los datosregistrados en el input a strings
    public string inputMail;

    public Button myDataINputBUtton;// btton para el envio de informacion a la base de datos verificar data inserter

    private bool statusButton;

    string CreateUserURL= "https://databasechroma.000webhostapp.com/itemsData.php";
    // Start is called before the first frame update
    void Start()
    {
        
        myDataINputBUtton.onClick.AddListener(myButtonSendData);
        statusButton=false;
    }
    public void myButtonSendData()
    {
        statusButton = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || statusButton == true)
        {
            inputName = NombreUsuario.text;
            inputMail = EmailUsuario.text;
            string inputTelefono = "1456890";
            string inputJuego = "1";
            CreateUser(inputName, inputMail, inputTelefono, inputJuego);
            Debug.Log("datos subidos correctamente");
            statusButton = false;
            Invoke("eraseTextofINputfields", 3);
        
        }
    }
    public void CreateUser(string username, string mail,string telefono,string juego)
    {
        WWWForm form = new WWWForm();
        form.AddField("usernamePost", username);
        form.AddField("MailPost",mail);
        form.AddField("telefonoPost", telefono);
        form.AddField("juegoPost", juego);
        WWW www = new WWW(CreateUserURL, form);
    }

    public void eraseTextofINputfields()//eliminamo
    {
        NombreUsuario.text = "";
        EmailUsuario.text  = "";
    }
}

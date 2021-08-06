using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public RectTransform inicioScreen, regiterScreen, emailScreen, bannerScreen;// mainMenu, carShopMenu, powerShopMenu;
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
        regiterScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
        emailScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void bannerScreenButton()
    {
        emailScreen.DOAnchorPos(new Vector2(-1400, 0), 0.25f);
        bannerScreen.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
}

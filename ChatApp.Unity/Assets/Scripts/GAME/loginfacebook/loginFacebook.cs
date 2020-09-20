using Facebook.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginFacebook : MonoBehaviour
{
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");

            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            }
            );
        }
        else
            FB.ActivateApp();
    }


    #region login/logout

    public void FacebookLogin()
    {
        var permissions = new List<String>() { "public_profile", "email", "user_friends" };
        FB.LogInWithPublishPermissions(permissions);


    }   
    public void FacebookLogout()
    {
        FB.LogOut();
    }    

    #endregion




}

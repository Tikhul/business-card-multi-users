using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class Menu : MonoBehaviour
{
    public Text emailField;
    public Text passwordField;
    private string userID;
    //private Player data;
  
    //private string DATA_URL = "https://business-card-5b341.firebaseio.com/";
    //private DatabaseReference databaseReference;
    public bool entered = false;
    void Start()
    {
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        // Reference the database
        //databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith(task => {
             if (task.IsCanceled)
             {
                 Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                 GetErrorMessage((AuthError)e.ErrorCode);
                 return;
             }
             if (task.IsFaulted)
             {
                 Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                 GetErrorMessage((AuthError)e.ErrorCode);
                 return;
             }

             if (task.IsCompleted)
              {
                    
              }
         });

    }
    public void Login_Anonymous()
    {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }
            if (task.IsFaulted)
            {
                Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                GetErrorMessage((AuthError)e.ErrorCode);
                return;
            }

            if (task.IsCompleted)
            {
                entered = true;

                //StartCoroutine(StartApp());
                StartApp();
                //GoToApp();
                Debug.Log(entered);
                
            }
            //SceneManager.LoadScene(0);
        } ,TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void RegisterUser()
    {
        if (emailField.text.Equals("") && passwordField.text.Equals(""))
        {
            print("Enter an email and a password to register");
            return;
        }
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).
            ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    //StartApp();
                    return;
                }
            });

    }
    public void Logout()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();
      
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                break;
            case AuthError.EmailAlreadyInUse:
                break;
            case AuthError.InvalidEmail:
                break;
        }
        print(msg);
    }

   //Saving data to the database
   //public void SaveData()
   //{
      //userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
      //data = new Player(emailField.text, userID);
      //string jsonData = JsonUtility.ToJson(data);
      //databaseReference.Child("Users" + userID).SetRawJsonValueAsync(jsonData);
      //Debug.Log("Savedata started");
    //}

    public void StartApp()
    {
      Debug.Log("StartApp started");
      SceneManager.LoadSceneAsync(0);
   }

   


}

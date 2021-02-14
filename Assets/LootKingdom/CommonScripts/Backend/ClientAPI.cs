using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;

public class ClientAPI : MonoBehaviour {
    private string url = "localhost:3000/users";
    private static System.Random random = new System.Random();

    void Start() {
        User user = new User() {
            user_id = "87654321",
            email = "bennida.c@gmail.com",
            first_name = "bennida",
            last_name = "jang",
            user_name = "bjang"
        };
        
        StartCoroutine(GetAllUsers());
        // StartCoroutine(GetUserById("12345678"));

        // StartCoroutine(CreateUser(user));
        // StartCoroutine(GetAllUsers());

        // StartCoroutine(DeleteUserById("JR239PS0"));
        // StartCoroutine(GetAllUsers());
    }

    public IEnumerator GetAllUsers() {
        using(UnityWebRequest www = UnityWebRequest.Get($"{url}")){
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                if (www.isDone) {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                }
                else {
                    Debug.Log("Error! Couldn't get data.");
                }
            }
        }
    }

    public IEnumerator GetUserById(string id) {
        using(UnityWebRequest www = UnityWebRequest.Get($"{url}/{id}")){
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                if (www.isDone) {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(result);
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(result);

                    Debug.Log(users[0].user_id);
                    Debug.Log(users[0].email);
                    Debug.Log(users[0].user_name);
                    Debug.Log(users[0].first_name);
                    Debug.Log(users[0].last_name);
                }
                else {
                    Debug.Log("Error! Couldn't get data.");
                }
            }
        }
    }
    
    // public IEnumerator GetUserByName(User user) {
    //     using(UnityWebRequest www = UnityWebRequest.Get($"{url}/first_name/{user.first_name}/last_name/{user.last_name}")){
    //         yield return www.SendWebRequest();

    //         if (www.isNetworkError) {
    //             Debug.Log(www.error);
    //         }
    //         else {
    //             if (www.isDone) {
    //                 var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
    //                 Debug.Log(result);
    //             }
    //             else {
    //                 Debug.Log("Error! Couldn't get data.");
    //             }
    //         }
    //     }
    // }

    // Use "POST" to upload new data
    IEnumerator CreateUser(User user) {
        WWWForm form = new WWWForm();
        form.AddField("user_id", RandomString(8));
        form.AddField("email", user.email);
        form.AddField("first_name", user.first_name);
        form.AddField("last_name", user.last_name);
        form.AddField("user_name", user.user_name);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form)) {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // Use "PUT" to update data
    public IEnumerator DeleteUserById(string id){
        using(UnityWebRequest www = UnityWebRequest.Delete($"{url}/{id}")){
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                if (www.isDone) {
                    Debug.Log($"User with ID: {id} has been deleted.");
                }
                else {
                    Debug.Log("Error! Couldn't get data.");
                }
            }
        }
    }
    
    // public IEnumerator DeleteUserByUserName(string userName){
    //     using(UnityWebRequest www = UnityWebRequest.Delete($"{url}/user_name/{userName}")){
    //         yield return www.SendWebRequest();

    //         if (www.isNetworkError) {
    //             Debug.Log(www.error);
    //         }
    //         else {
    //             if (www.isDone) {
    //                 Debug.Log($"User with username: {userName} has been deleted.");
    //             }
    //             else {
    //                 Debug.Log("Error! Couldn't get data.");
    //             }
    //         }
    //     }
    // }

    public static string RandomString(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
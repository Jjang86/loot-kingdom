using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientAPI : MonoBehaviour {
    private string url = "localhost:3000/users";

    void Start() {
        StartCoroutine(GetAllUsers());
        // StartCoroutine(GetDeckById(1));
        // StartCoroutine(GetDeckByName("bennida"));

        // StartCoroutine(DeleteById(8));
        // StartCoroutine(DeleteByName("jeff"));
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

    public IEnumerator GetUserById(int id) {
        using(UnityWebRequest www = UnityWebRequest.Get($"{url}/{id}")){
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
    
    public IEnumerator GetUserByName(string userName) {
        using(UnityWebRequest www = UnityWebRequest.Get($"{url}/name/{userName}")){
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

    // Use "POST" to upload new data
    IEnumerator Post() {
        WWWForm form = new WWWForm();
        form.AddField("name", "jeff");
        form.AddField("content", "testinggg");

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
    public IEnumerator DeleteUserById(int id){
        using(UnityWebRequest www = UnityWebRequest.Delete($"{url}/{id}")){
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                if (www.isDone) {
                    Debug.Log($"Deck with ID: {id} has been deleted.");
                }
                else {
                    Debug.Log("Error! Couldn't get data.");
                }
            }
        }
    }
    
    public IEnumerator DeleteUserByUserName(string userName){
        using(UnityWebRequest www = UnityWebRequest.Delete($"{url}/name/{userName}")){
            yield return www.SendWebRequest();

            if (www.isNetworkError) {
                Debug.Log(www.error);
            }
            else {
                if (www.isDone) {
                    Debug.Log($"User with username: {userName} has been deleted.");
                }
                else {
                    Debug.Log("Error! Couldn't get data.");
                }
            }
        }
    }
}
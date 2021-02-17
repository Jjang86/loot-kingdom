using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

public class ClientAPI : MonoBehaviour {
    private string usersURL = "localhost:3000/users";
    private static System.Random random = new System.Random();

    void Start() {
    //     User user = new User() {
    //         user_id = "87654321",
    //         email = "bennida.c@gmail.com",
    //         first_name = "bennida",
    //         last_name = "jang",
    //         user_name = "bjang"
    //     };
    //     await GetAllUsers();
    //     Debug.Log("First");
    //     await GetUserById("12345678");
    //     Debug.Log("Second");
    //     await CreateUser(user);
    //     Debug.Log("Third");
    //     await GetAllUsers();
    //     Debug.Log("Fourth");
    //     await DeleteUserById("ZGHLLJIM");
    //     await GetAllUsers();
    }

    public async Task<List<User>> GetAllUsers() {
        using(UnityWebRequest www = UnityWebRequest.Get($"{usersURL}")){
            www.SendWebRequest(); 
            while (!www.isDone) {
                if (www.isNetworkError) {
                    Debug.Log(www.error);
                    break;
                }
                await Task.Delay(10);
            }
            if (www.isDone) {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                Debug.Log(result);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(result);
                return users;
            }
            else {
                Debug.Log("Error! Couldn't get data.");
                return new List<User>();
            }
        }
    }

    public async Task<User> GetUserById(string id) {
        using(UnityWebRequest www = UnityWebRequest.Get($"{usersURL}/{id}")){
            www.SendWebRequest();
            while (!www.isDone) {
                if (www.isNetworkError) {
                    Debug.Log(www.error);
                    break;
                }
                await Task.Delay(1);
            }
            if (www.isDone) {
                var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                Debug.Log(result);
                List<User> users = JsonConvert.DeserializeObject<List<User>>(result);
                return users[0];
            }
            else {
                Debug.Log("Error! Couldn't get data.");
                return new User();
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
    public async Task CreateUser(User user) {
        WWWForm form = new WWWForm();
        form.AddField("user_id", RandomString(8));
        form.AddField("email", user.email);
        form.AddField("first_name", user.first_name);
        form.AddField("last_name", user.last_name);
        form.AddField("user_name", user.user_name);

        using (UnityWebRequest www = UnityWebRequest.Post(usersURL, form)) {
            www.SendWebRequest();
            while (!www.isDone) {
                if (www.isNetworkError) {
                    Debug.Log(www.error);
                    return;
                }
                await Task.Delay(1);
            }
            Debug.Log("Form upload complete!");
        }
    }

    // Use "PUT" to update data
    public async Task DeleteUserById(string id){
        using(UnityWebRequest www = UnityWebRequest.Delete($"{usersURL}/{id}")){
            www.SendWebRequest();

            while (!www.isDone) {
                if (www.isNetworkError) {
                    Debug.Log(www.error);
                    break;
                }
                await Task.Delay(1);
            }
            if (www.isDone) {
                Debug.Log($"User with ID: {id} has been deleted.");
            }
            else {
                Debug.Log("Error! Couldn't get data.");
            
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
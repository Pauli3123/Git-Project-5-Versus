using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class Login : MonoBehaviour
{
	public TMP_InputField emailInput;
	public TMP_InputField passwordInput;


	private string loginUrl = "https://jouw-api-url.com/login";



	public void OnLoginButtonClicked()
	{
		string email = emailInput.text;
		string password = passwordInput.text;
		Debug.Log($"Login attempt: Email = {email}, Password = {password}");

		StartCoroutine(LoginLogic(email, password));
	}

	IEnumerator LoginLogic(string email, string password)
	{
	

		
		string json = JsonUtility.ToJson(new LoginRequest(email, password));

		UnityWebRequest request = new UnityWebRequest(loginUrl, "POST");
		byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
		request.uploadHandler = new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
        if (request.result == UnityWebRequest.Result.Success)
#else
		if (!request.isNetworkError && !request.isHttpError)
#endif
		{
			Debug.Log("Login success: " + request.downloadHandler.text);
			SceneManager.LoadScene("MainMenu"); 
		}
		else
		{
			Debug.Log("Login failed: " + request.error);
			
		}
		
	}

	[System.Serializable]
	public class LoginRequest
	{
		public string email;
		public string password;

		public LoginRequest(string email, string password)
		{
			this.email = email;
			this.password = password;
		}
	}
}

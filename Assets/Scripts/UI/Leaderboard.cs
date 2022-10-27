using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }
    
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    public void SendScore(string username, int score)
    {
        string DreamloLink = "http://dreamlo.com/lb/IwoiXVcTs0OnpbaQzgtuoQWak2GGPQm0CWofFBOIKYsg/add/" + username + "/" + score;
        StartCoroutine(GetRequest(DreamloLink));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
//using Vuforia;

public class TextBehaviourScript : MonoBehaviour
{
    // *Initalize fields*
    //As far as I use InputField from TextMeshProm, my type of object will be "TMP_InputField"
    TMP_InputField temperature;
    TMP_InputField humidity;
    TMP_InputField lighting;
    
    const string source = "https://api.thingspeak.com/channels/NumberOfChannel/fields/";
    const string key = "your key";

    private void Start()
    {
        temperature = GameObject.Find("T").GetComponent<TMPro.TMP_InputField>();
        humidity = GameObject.Find("H").GetComponent<TMPro.TMP_InputField>();
        lighting = GameObject.Find("L").GetComponent<TMPro.TMP_InputField>();
    }
    public void Click()
    {
        Get_Temperature();
        Get_Humidity();
        Get_Lighting();
    }
    public void Get_Temperature() => StartCoroutine(GetData(source + "1/last?key=" + key, temperature));
    public void Get_Humidity() => StartCoroutine(GetData(source + "2/last?key=" + key, humidity));
    public void Get_Lighting() => StartCoroutine(GetData(source + "3/last?key=" + key, lighting));

    //Function for get request
    public IEnumerator GetData(string requestUrl, TMP_InputField field)
    {
        Debug.Log("Getting Data");
        var request = UnityWebRequest.Get(requestUrl);

        yield return request.SendWebRequest();
        field.text = request.downloadHandler.text;

        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError(" Error: " + request.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(" HTTP Error: " + request.error);
                break;
            case UnityWebRequest.Result.Success:
                Debug.Log(":\nReceived: " + request.downloadHandler.text);
                break;
        }

    }
}


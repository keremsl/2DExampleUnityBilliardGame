using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace BilliardDemo {

    public class SunsetAPI : MonoBehaviour {
        SunsetModel sunsetModel = default;
        const string BASE_URL = "https://api.sunrise-sunset.org/json?";
        const string DUMMY_LOCATION = "lat=41.015137&lng=28.979530"; // location of istanbul

        async void Start () {
            string sunsetTxt;
            if (CheckConnection ())
                sunsetTxt = await GetAsyncSunsetData ();
            else
                sunsetTxt = "";

            //UIManager.Instance.SetWelcomeTxt (sunsetTxt);
        }

        public async Task<string> GetAsyncSunsetData () {
            var url = BASE_URL + DUMMY_LOCATION;

            using var www = UnityWebRequest.Get (url);

            www.SetRequestHeader ("Content-Type", "application/json");

            var operation = www.SendWebRequest ();

            while (!operation.isDone)
                await Task.Yield ();

            switch (www.result) {
                case UnityWebRequest.Result.Success:
                    sunsetModel = JsonConvert.DeserializeObject<SunsetModel> (www.downloadHandler.text);
                    if (!string.IsNullOrEmpty (sunsetModel.GetSunset ())) {
                        return sunsetModel.GetSunset ();
                    } else {
                        Debug.LogWarning ("Sunset could not be setted");
                    }
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogWarning ($"Data Processing Error: {www.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning ($"Protocol Error: {www.error}");
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogWarning ($"Connection Error: {www.error}");
                    break;
                default:
                    break;
            }

            return "";
        }

        bool CheckConnection () {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

}
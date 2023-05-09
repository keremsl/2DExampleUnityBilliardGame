using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;

public class SunsetAPITest {
    [UnityTest]
    public IEnumerator SunsetAPIConnectionTest () {
        const string BASE_URL = "https://api.sunrise-sunset.org/json?";
        const string DUMMY_LOCATION = "lat=41.015137&lng=28.979530"; // location of istanbul
        var url = BASE_URL + DUMMY_LOCATION;

        using var www = UnityWebRequest.Get (url);

        www.SetRequestHeader ("Content-Type", "application/json");
        www.timeout = 10;

        var operation = www.SendWebRequest ();

        yield return operation;

        Assert.AreEqual(operation.webRequest.responseCode,200);
        yield return null;

    }
}
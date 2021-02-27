using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.GameLift;
using Amazon;
using Amazon.GameLift.Model;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using System.Threading;

public class gameliftclienttest : MonoBehaviour
{
    AmazonGameLiftClient _client;

    void Start()
    {
        var config = new AmazonGameLiftConfig();
        //config.RegionEndpoint = RegionEndpoint.USEast1;
        config.ServiceURL = "http://localhost:9080";
        //config.ProxyHost = "localhost";
        //config.ProxyPort = 9080;

        _client = new AmazonGameLiftClient("asd", "asd", config);
        GetSessions();
    }

    public async void GetSessions()
    {
        Debug.Log("begin describing");
        DescribeGameSessionsRequest request = new DescribeGameSessionsRequest();
        request.FleetId = "asdasd";

        DescribeGameSessionsResponse response = await _client.DescribeGameSessionsAsync(request);
        Debug.Log(response.HttpStatusCode);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConfig : MonoBehaviour
{
    public string buildId = "";
	public string region = "";

  
}

public enum BuildType
{
	LOCAL_CLIENT,
	REMOTE_CLIENT,
	LOCAL_SERVER,
	REMOTE_SERVER
}

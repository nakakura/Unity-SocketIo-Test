using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.EventSystems;

public class JanusClient : MonoBehaviour 
{
	private SocketIOComponent socket_;
	// Use this for initialization
	void Start ()
	{
		GameObject go = GameObject.Find ("SocketIO");
		this.socket_ = go.GetComponent<SocketIOComponent> ();
		//webrtc-server.paas.jp-e1.cloudn-service.com
		//this.socket_.Connect ();
		print ("hogehoge");
		this.socket_.On ("open", TestOpen);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//print ("hogehoge");
	}

	public void TestOpen (SocketIOEvent e)
	{
		Debug.Log ("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void OnClick() {
		Debug.Log ("Button click!");
		Debug.Log (this.socket_);
	}
}


using UnityEngine;
using System.Collections;
using SocketIO;

public class JanusController{
	private SocketIOComponent socket_;

	public JanusController(){

		//webrtc-server.paas.jp-e1.cloudn-service.com
		//this.socket_.Connect ();
		GameObject socket = GameObject.Find ("SocketIO");
		this.socket_ = socket.GetComponent<SocketIOComponent> ();
		this.socket_.On ("open", OnOpen);
	}

	public void OnOpen(SocketIOEvent e)
	{
		Debug.Log("=====");
		Debug.Log ("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void bind(string ipAddress){
		Debug.Log("ipaddress");
	}
}

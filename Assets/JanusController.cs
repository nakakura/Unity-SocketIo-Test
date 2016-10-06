using UnityEngine;
using System.Collections;
using SocketIO;

[System.Serializable]
public class BrowserOptions{
	public string plugin;
	public string server;

	public BrowserOptions(string address){
		this.plugin = "janus.plugin.telexistence";
		this.server = string.Format("http://{0}:8088/janus", address);
	}

	public JSONObject JsonObject(){
		var json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("plugin", this.plugin);
		json.AddField("server", this.server);
		return json;
	}
}

[System.Serializable]
public class BindParameters{
		public BrowserOptions browserOptions;

		public BindParameters(string server){
			this.browserOptions = new BrowserOptions(server);
		}

		public JSONObject JsonObject(){
			var json = new JSONObject(JSONObject.Type.OBJECT);
			var innerJson = this.browserOptions.JsonObject();
			json.AddField("browserOptions", innerJson);
			return json;
		}
}

public class JanusController{
	private SocketIOComponent socket_;

	public JanusController(){
		//webrtc-server.paas.jp-e1.cloudn-service.com
		//this.socket_.Connect ();
		GameObject socket = GameObject.Find ("SocketIO");
		this.socket_ = socket.GetComponent<SocketIOComponent> ();
		this.socket_.On ("open", OnOpen);
		this.socket_.On("bind", (message)=>{
			Debug.Log("lambda");
			Debug.Log(message);
		});
	}

	public void OnOpen(SocketIOEvent e)
	{
		Debug.Log("=====");
		Debug.Log ("[SocketIO] Open received: " + e.name + " " + e.data);
	}

	public void bind(string ipAddress){
		Debug.Log("janus bind");
		var browserOptions = new BindParameters(ipAddress);
		var json = browserOptions.JsonObject();
		Debug.Log(json);
		this.socket_.Emit("bind", json);
	}
}

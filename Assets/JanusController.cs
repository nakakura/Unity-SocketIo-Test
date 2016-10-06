using UnityEngine;
using System;
using System.Collections;
using SocketIO;

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

public class LoginParameters{
	class PeerOptions{
		private string peerId_;

		public PeerOptions(string peerId){
			this.peerId_ = peerId;
		}

		public JSONObject JsonObject(){
			var json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("debug", 3);
			json.AddField("peerId", this.peerId_);
			json.AddField("secure", true);
			json.AddField("key", "bbe413aa-6934-11e4-bbe9-3b2b68acb506");
			return json;
		}
	}

	private PeerOptions options_;

	public LoginParameters(string peerId){
		this.options_ = new PeerOptions(peerId);
	}

	public JSONObject JsonObject(){
		var json = new JSONObject(JSONObject.Type.OBJECT);
		var innerJson = this.options_.JsonObject();
		json.AddField("peerOptions", innerJson);
		return json;
	}
}

public class JanusController{
	private SocketIOComponent socket_;

	public JanusController(){
		Debug.Log("JanusController constructor");
		GameObject socket = GameObject.Find ("SocketIO");

		this.socket_ = socket.GetComponent<SocketIOComponent> ();
		this.socket_.On ("connect", OnOpen_);
		this.socket_.On ("disconnect", OnDisconnect_);
	}

	private void OnDisconnect_(SocketIOEvent e){
		this.socket_.Off("bind", OnBind_);
		this.socket_.Off("register", OnLogin_);
	}

	private void OnOpen_(SocketIOEvent e)
	{
		this.socket_.On("bind", OnBind_);
		this.socket_.On("register", OnLogin_);
	}

	private void OnBind_(SocketIOEvent e){
		JSONObject obj = e.data;
		Debug.Log(obj);
	}

	public void Bind(string ipAddress){
		Debug.Log("janus bind");
		var browserOptions = new BindParameters(ipAddress);
		var json = browserOptions.JsonObject();
		this.socket_.Emit("bind", json);
	}

	private void OnLogin_(SocketIOEvent e){
		JSONObject obj = e.data;
		Debug.Log(obj);
	}

	public void Login(string peerId){
		var peerOptions = new LoginParameters(peerId);
		var json = peerOptions.JsonObject();
		Debug.Log(json);
		this.socket_.Emit("register", json);
	}

	public void CreateStream(string a_port, string v_port){

	}

	public void Call(string peerId){
	}
}

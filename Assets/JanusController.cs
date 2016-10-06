using UnityEngine;
using System;
using System.Collections;
using SocketIO;

public class BindParameters{
	class BrowserOptions{
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

	private BrowserOptions browserOptions_;

	public BindParameters(string server){
		this.browserOptions_ = new BrowserOptions(server);
	}

	public JSONObject JsonObject(){
		var json = new JSONObject(JSONObject.Type.OBJECT);
		var innerJson = this.browserOptions_.JsonObject();
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

public class CreateStreamParameters{
	class StreamOptions{
		private int aport_;
		private int vport_;

		public StreamOptions(int aport, int vport){
			this.aport_ = aport;
			this.vport_ = vport;
		}

		public JSONObject JsonObject(){
			var json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("aflag", true);
			json.AddField("aport", this.aport_);
			json.AddField("apt", 111);
			json.AddField("aformat", "opus/48000/2");
			json.AddField("vflag", true);
			json.AddField("vport", this.vport_);
			json.AddField("vpt", 100);
			json.AddField("vformat", "VP8/90000");
			return json;
		}
	}

	private StreamOptions options_;

	public CreateStreamParameters(int aport, int vport){
		this.options_ = new StreamOptions(aport, vport);
	}

	public JSONObject JsonObject(){
		var json = new JSONObject(JSONObject.Type.OBJECT);
		var innerJson = this.options_.JsonObject();
		json.AddField("streamOptions", innerJson);
		return json;
	}
}

public class CallParameters{
	class CallOptions{
		private string targetId_;

		public CallOptions(string targetId){
			this.targetId_ = targetId;
		}

		public JSONObject JsonObject(){
			var json = new JSONObject(JSONObject.Type.OBJECT);
			var innerJson = new JSONObject(JSONObject.Type.OBJECT);
			innerJson.AddField("audio", true);
			innerJson.AddField("video", true);
			innerJson.AddField("streamId", 0); //FIXME
			json.AddField("constraints", innerJson);
			json.AddField("targetId", this.targetId_);
			return json;
		}
	}

	private CallOptions options_;

	public CallParameters(string targetId){
		this.options_ = new CallOptions(targetId);
	}

	public JSONObject JsonObject(){
		var json = new JSONObject(JSONObject.Type.OBJECT);
		var innerJson = this.options_.JsonObject();
		json.AddField("callOptions", innerJson);
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
		this.socket_.Off("createStream", OnCreateStream_);
	}

	private void OnOpen_(SocketIOEvent e)
	{
		this.socket_.On("bind", OnBind_);
		this.socket_.On("register", OnLogin_);
		this.socket_.On("createStream", OnCreateStream_);
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

	private void OnCreateStream_(SocketIOEvent e){
		JSONObject obj = e.data;
		Debug.Log(obj);
	}

	public void CreateStream(int a_port, int v_port){
		var peerOptions = new CreateStreamParameters(a_port, v_port);
		var json = peerOptions.JsonObject();
		Debug.Log(json);
		this.socket_.Emit("createStream", json);
	}

	public void Call(string peerId){
	}
}

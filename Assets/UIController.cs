using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using SocketIO;

public class UIController: MonoBehaviour
{
	private JanusController controller_;
	// Use this for initialization
	void Start ()
	{
		Debug.Log("uicontroller initialization");
		this.controller_ = new JanusController();
	}



	// Update is called once per frame
	void Update ()
	{
		//print ("hogehoge");
	}

	public void OnClickBind() {
		Debug.Log ("bind click!");
		GameObject go = GameObject.Find ("JanusIpField");
		InputField field = go.GetComponent<InputField> ();
		Debug.Log (field.text);
		this.controller_.Bind(field.text);
	}

	public void OnClickLogin() {
		Debug.Log ("login click!");
		GameObject go = GameObject.Find ("PeerIdField");
		InputField field = go.GetComponent<InputField> ();
		this.controller_.Login(field.text);
	}

	public void OnClickCreateStream() {
		Debug.Log ("stream click!");
		GameObject a_go = GameObject.Find ("AudioPortField");
		InputField a_field = a_go.GetComponent<InputField> ();
		var a_port = a_field.text;

		GameObject v_go = GameObject.Find ("VideoPortField");
		InputField v_field = v_go.GetComponent<InputField> ();
		var v_port = v_field.text;
		this.controller_.CreateStream(int.Parse(a_port), int.Parse(v_port));
	}

	public void OnClickCall() {
		Debug.Log ("call click!");
		GameObject go = GameObject.Find ("TargetPeerIdField");
		InputField field = go.GetComponent<InputField> ();
		this.controller_.Call(field.text);
	}
}

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
		this.controller_.bind(field.text);
	}

	public void OnClickLogin() {
		Debug.Log ("login click!");
		GameObject go = GameObject.Find ("PeerIdField");
		InputField field = go.GetComponent<InputField> ();
		Debug.Log (field.text);
	}

	public void OnClickCreateStream() {
		Debug.Log ("stream click!");
		GameObject a_go = GameObject.Find ("AudioPortField");
		InputField a_field = a_go.GetComponent<InputField> ();
		Debug.Log (a_field.text);

		GameObject v_go = GameObject.Find ("VideoPortField");
		InputField v_field = v_go.GetComponent<InputField> ();
		Debug.Log (v_field.text);
	}

	public void OnClickCall() {
		Debug.Log ("call click!");
		GameObject go = GameObject.Find ("TargetPeerIdField");
		InputField field = go.GetComponent<InputField> ();
		Debug.Log (field.text);
	}
}

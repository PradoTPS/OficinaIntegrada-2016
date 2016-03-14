using UnityEngine;
using System.Collections;

namespace KeyboardInput{
	
	public enum KeyboardController{
		First = 0,
		Second = 1
	}

	public enum KeyboardAxis{
		Horizontal,
		Vertical
	}

	public enum KeyboardButton{
		Jump
	}

	public class KCI : MonoBehaviour {

		public static float GetAxisRaw(KeyboardAxis axis, KeyboardController player){
			return Input.GetAxisRaw ("Player" + ((int)player + 1).ToString() + axis.ToString());
		}

		public static bool GetButtonDown(KeyboardButton button, KeyboardController player){
			return Input.GetButtonDown ("Player" + ((int)player + 1).ToString() + button.ToString());
		}

		public static bool GetButtonUp(KeyboardButton button, KeyboardController player){
			return Input.GetButtonUp ("Player" + ((int)player + 1).ToString() + button.ToString());
		}
	}
}

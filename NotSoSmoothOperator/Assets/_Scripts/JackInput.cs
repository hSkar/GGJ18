using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhoneOp {
	public class JackInput : MonoBehaviour 
	{
		public Transform connectionPoint;
		public PhoneJack connectedJack;


		public bool ConnectJack(PhoneJack jack)
		{
			if(connectedJack != null) {
				connectedJack.Disconnect();
			}

			jack.transform.position = connectionPoint.position;
			jack.transform.rotation = connectionPoint.rotation;
			connectedJack = jack;
			return true;
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace PhoneOp {
	public class PhoneJack : MonoBehaviour 
	{
		public LineRenderer m_lineRenderer;
		public Transform jack;
		public Transform jackCableAttachmentPoint;
		public bool m_attachedToInput = false;
		Vector3 m_origin;
		Vector3 m_jackOriginPos;
		Quaternion m_jackOriginRot;
		Rigidbody rb;
		private bool m_isHeld = false;

		JackInput touchingJack;

		void OnAttachedToHand(Hand hand)
		{
			touchingJack = null;
			m_isHeld = true;
		}

		void OnDetachedFromHand(Hand hand)
		{
			m_isHeld = false;
			if(touchingJack != null) {
				touchingJack.ConnectJack(this);
				return;
			}

			ResetJack();
		}

		private void Awake()
		{
			rb = jack.GetComponent<Rigidbody>();
			m_lineRenderer.useWorldSpace = true;
			m_origin = transform.parent.position;
			m_jackOriginPos = jack.position;
			m_jackOriginRot = jack.rotation;
		}

		private void Update()
		{
			m_lineRenderer.SetPosition(1, m_origin);
			m_lineRenderer.SetPosition(0, jackCableAttachmentPoint.position);

			if(!m_isHeld && !rb.isKinematic) {
				rb.isKinematic = true;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.tag == "PhoneInput") {
				var input = other.GetComponentInParent<JackInput>();
				if(input != null) {
					touchingJack = input;
				}
			}	
		}

		private void OnTriggerExit(Collider other)
		{
			var input = other.GetComponentInParent<JackInput>();
			if(input != null) {
				if(input == touchingJack) {
					touchingJack = null;
				}
			}
		}

		public void Disconnect()
		{
			touchingJack = null;
			ResetJack();
		}

		public void Connect()
		{

		}

		public void ResetJack()
		{
			if (!m_attachedToInput) {
				jack.position = m_jackOriginPos;
				jack.rotation = m_jackOriginRot;
			}
		}
	}
}

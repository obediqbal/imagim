using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.StateMachineSystem
{
    public interface IMonoBehaviour
    {
        void Awake();
        void FixedUpdate();
        void LateUpdate();
        void OnAnimatorIK(int layerIndex);
        void OnAnimatorMove();
        void OnApplicationFocus(bool focus);
        void OnApplicationPause(bool pause);
        void OnApplicationQuit();
        void OnAudioFilterRead(float[] data, int channels);
        void OnBecameInvisible();
        void OnBecameVisible();
        void OnCollisionEnter(Collision collision);
        void OnCollisionEnter2D(Collision2D collision);
        void OnCollisionExit(Collision collision);
        void OnCollisionExit2D(Collision2D collision);
        void OnCollisionStay(Collision collision);
        void OnCollisionStay2D(Collision2D collision);
        void OnControllerColliderHit(ControllerColliderHit hit);
        void OnDestroy();
        void OnDisable();
        void OnDrawGizmos();
        void OnDrawGizmosSelected();
        void OnEnable();
        void OnGUI();
        void OnJointBreak(float breakForce);
        void OnJointBreak2D(Joint2D joint);
        void OnMouseDown();
        void OnMouseDrag();
        void OnMouseEnter();
        void OnMouseExit();
        void OnMouseOver();
        void OnMouseUp();
        void OnMouseUpAsButton();
        void OnParticleCollision(GameObject other);
        void OnParticleSystemStopped();
        void OnParticleTrigger();
        void OnParticleUpdateJobScheduled();
        void OnPostRender();
        void OnPreCull();
        void OnPreRender();
        void OnRenderImage(RenderTexture source, RenderTexture destination);
        void OnRenderObject();
        void OnTransformChildrenChanged();
        void OnTransformParentChanged();
        void OnTriggerEnter(Collider other);
        void OnTriggerEnter2D(Collider2D collision);
        void OnTriggerExit(Collider other);
        void OnTriggerExit2D(Collider2D collision);
        void OnTriggerStay(Collider other);
        void OnTriggerStay2D(Collider2D collision);
        void OnValidate();
        void OnWillRenderObject();
        void Reset();
        void Start();
        void Update();
    }
}
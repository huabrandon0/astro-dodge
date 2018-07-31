using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    [RequireComponent(typeof(Camera))]
    public class FovKick : MonoBehaviour
    {
        private Camera cam;
        private float origFov;
        private Vector3 origPos;

        [SerializeField] private float defaultFovInfluence = 3f;
        [SerializeField] private float defaultZInfluence = -0.1f;
        [SerializeField] private float defaultDuration = 0.1f;
        [SerializeField] private AnimationCurve animationCurve = AnimationCurve.Linear(0, 0, 1, 1);

        private void Awake()
        {
            this.cam = GetComponent<Camera>();
            if (!this.cam)
                Debug.LogError("Could not resolve Camera reference.");
        }

        private void Start()
        {
            this.origFov = this.cam.fieldOfView;
            this.origPos = this.cam.transform.localPosition;
        }

        public void Kick()
        {
            Kick(this.defaultFovInfluence, this.defaultZInfluence, this.defaultDuration);
        }

        public void Kick(float fovInfluence, float zInfluence, float duration)
        {
            StopAllCoroutines();
            StartCoroutine(KickHelper(fovInfluence, zInfluence, duration));
        }

        private IEnumerator KickHelper(float fovInfluence, float zInfluence, float duration)
        {
            this.cam.fieldOfView = this.origFov;
            this.cam.transform.localPosition = this.origPos;

            float startTime = Time.time;
            float currentTime = Time.time;

            while (Time.time < startTime + duration)
            {
                this.cam.fieldOfView = this.origFov + this.animationCurve.Evaluate((Time.time - startTime) / duration) * fovInfluence;
                this.cam.transform.localPosition = this.origPos + this.cam.transform.forward * (this.animationCurve.Evaluate((Time.time - startTime) / duration) * zInfluence);
                yield return null;
            }

            this.cam.fieldOfView = this.origFov;
            this.cam.transform.localPosition = this.origPos;
        }
    }
}
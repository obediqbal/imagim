using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DKH.ObserverSystem;
using DKH.InstancePooling;
using DKH.DeprecatedEntitySystem;

namespace DKH.CameraSystem
{
    public class GameMode : MonoBehaviour
    {
        public static GameMode inst;
        [SerializeField] Camera _camera;
        [SerializeField] Type type;
        Observable<Type> gamemode = new Observable<Type>(Type.Offensive);
        List<Transform> focusedTransforms = new List<Transform>();
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f);
        public enum Type
        {
            Offensive,
            Defensive,
            HackNSlash,
        }
        private void OnValidate()
        {
            if (type != gamemode.Value)
            {
                gamemode.Value = type;
            }
        }

        [System.Obsolete()]
        private void Awake()
        {
            if (inst == null)
            {
                inst = this;
            }
            else
            {
                Destroy(this);
            }
            //gamemode.AddObserver((type) =>
            //{
            //    focusedTransforms.Clear();
            //    switch (type)
            //    {
            //        case Type.Offensive:
            //            focusedTransforms
            //            .AddRange(
            //                InstancePool.FindInstances(InstancePool.Type.Tower)
            //                .Select((o) => o.transform)
            //            );
            //            break;
            //        case Type.Defensive:
            //            focusedTransforms
            //            .AddRange(
            //                InstancePool.FindInstances(InstancePool.Type.Tower)
            //                .Where((o) => (o.GetComponent<DeprecatedEntitySystem.Entity>().team.Value == 0))
            //                .Select((o) => o.transform)
            //            );
            //            break;
            //        case Type.HackNSlash:
            //            focusedTransforms
            //            .AddRange(
            //                InstancePool.FindInstances(InstancePool.Type.Player)
            //                .Where((o) => (o.GetComponent<DeprecatedEntitySystem.Entity>().team.Value == 0))
            //                .Select((o) => o.transform)
            //            );
            //            break;
            //        default:
            //            break;
            //    }
            //});
        }
        private void Start()
        {
            StartCoroutine(RecheckLoop());
        }
        private void FixedUpdate()
        {
            if (focusedTransforms.Count > 0)
            {
                Vector3 centerPos = Vector3.zero;
                for (int i = 0; i < focusedTransforms.Count; i++)
                {
                    centerPos += focusedTransforms[i].position;
                }
                centerPos /= focusedTransforms.Count;
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, centerPos - Vector3.forward * 10f, Time.deltaTime * 2f);
            }
        }
        IEnumerator RecheckLoop()
        {
            gamemode.CallObservers();
            yield return delay;
            StartCoroutine(RecheckLoop());
        }
    }
}

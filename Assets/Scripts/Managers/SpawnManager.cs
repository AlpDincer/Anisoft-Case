using Scripts.PoolSystem;
using UnityEngine;

namespace Scripts.Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public Transform parent;
        public GameObject Spawn(Vector3 pos, string spawnName)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = pos;
            return fx;
        }

        public GameObject Spawn(Vector3 pos, string spawnName, Vector3 vec)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = pos;
            fx.transform.localPosition += vec;
            return fx;
        }

        public GameObject Spawn(Transform parent, string spawnName)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = parent.position;
            fx.transform.SetParent(parent);
            return fx;
        }

        public GameObject Spawn(Transform parent, string spawnName, Vector3 vec)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = parent.position;
            fx.transform.SetParent(parent);
            fx.transform.localPosition += vec;
            return fx;
        }

        public GameObject Spawn(Transform parent, string spawnName, float sizeMultiplier)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = parent.position;
            fx.transform.SetParent(parent);
            fx.transform.localScale = fx.transform.localScale * sizeMultiplier;
            return fx;
        }

        public GameObject Spawn(Transform parent, string spawnName, float sizeMultiplier, Vector3 vec)
        {
            var fx = ObjectPooler.Instance.Spawn(spawnName, new Vector3());
            fx.transform.position = parent.position;
            fx.transform.SetParent(parent);
            fx.transform.localScale = fx.transform.localScale * sizeMultiplier;
            fx.transform.localPosition += vec;
            return fx;
        }
    }
}
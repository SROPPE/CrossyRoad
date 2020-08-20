using UnityEngine;
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        bool isSpawned = false;
        private void Awake()
        {
            if (FindObjectOfType<LoadScreen>() == null)
                SpawnObject();
        }

        private void SpawnObject()
        {
            var persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
            isSpawned = true;
        }
    }

using UnityEngine;

public class AudioSpawner : MonoBehaviour
{
    [SerializeField] private GameObject audioSoundObjectPrefab;

    [SerializeField] private float destroyDelay;

    public void SpawnAudioObject(Transform pos)
    {
        GameObject go = Instantiate(audioSoundObjectPrefab, pos.position, Quaternion.identity);
        Destroy(go, destroyDelay);
    }
}

using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageTracker : MonoBehaviour {
    [SerializeField] private ARTrackedImageManager mTrackedImageManager;
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject envPrefab;
    
    private AudioSource _audioSource;
    
    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void OnEnable() => mTrackedImageManager.trackedImagesChanged += OnChanged;

    private void OnDisable() => mTrackedImageManager.trackedImagesChanged -= OnChanged;

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (ARTrackedImage newImage in eventArgs.added) {
            switch (newImage.referenceImage.name) {
                case "puddle":
                    Instantiate(shipPrefab, newImage.transform, false);
                    break;
                case "boat":
                    Instantiate(envPrefab, newImage.transform, false);
                    break;
            }
            _audioSource.Play();
        }
    }
}
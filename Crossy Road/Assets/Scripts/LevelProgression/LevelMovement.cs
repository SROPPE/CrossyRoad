using UnityEngine;
using DG.Tweening;


public class LevelMovement : MonoBehaviour,ISaveable
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float offset = 0.1f;
    [SerializeField] private CameraSpotter cameraSpotter;

    private bool isUrgentUpdate = false;
    private PlayerController playerController;
    private PlayerMovement playerMovement;

    private Vector3 startPosition;
    private void Awake()
    {
        startPosition = transform.position;
        playerController = FindObjectOfType<PlayerController>();
        playerMovement = playerController.GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
  
        cameraSpotter.onSpotterTrigger += UrgentUpdateZPosition;
    }
    private void OnDisable()
    {

        cameraSpotter.onSpotterTrigger -= UrgentUpdateZPosition;
    }
    public void UrgentUpdateZPosition(Vector3 position)
    {
        isUrgentUpdate = true;
        transform.DOMoveZ(position.z, 0.2f).OnComplete(()=>isUrgentUpdate = false);
    }
    public void UrgentUpdateXPosition(Vector3 position)
    {
        isUrgentUpdate = true;
        transform.DOMoveX(position.x, 0.2f).OnComplete(() => isUrgentUpdate = false);
    }
    private void LateUpdate()
    {
        if (playerController.IsDead) return;
        if (isUrgentUpdate) return;
        transform.position = new Vector3(playerMovement.transform.position.x,transform.position.y,transform.position.z);
        transform.position = Vector3.Lerp
            (transform.position, transform.position + Vector3.forward, movementSpeed*Time.deltaTime);
    }

    public object CaptureState()
    {
        return new SerializableVector3(startPosition);
    }

    public void RestoreState(object state)
    {
        transform.position = ((SerializableVector3)state).ToVector();
    }
}

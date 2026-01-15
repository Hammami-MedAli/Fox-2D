using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraHeight;
    private float lookAheadX;
    private float lookAheadY;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + lookAheadX, player.position.y + cameraHeight, transform.position.z);
        lookAheadX = Mathf.Lerp(lookAheadX, (aheadDistance * player.localScale.x), cameraSpeed * Time.deltaTime);
        lookAheadY = Mathf.Lerp(lookAheadY, (cameraHeight * player.localScale.y), cameraSpeed * Time.deltaTime);
    }
}
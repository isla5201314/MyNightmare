using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float smoothing = 5f;
    Vector3 offset;
    private Transform target;

    // Start is called before the first frame update  
    void Start()
    {
        // Find the game object with the tag "Player"  
        GameObject player = GameObject.FindWithTag("Player");

        // If the player object is found, get its Transform component and set it as the target  
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No game object found with the tag 'Player'. Please make sure a game object with the tag 'Player' exists in the scene.");
            this.enabled = false; // Disable this script if no player is found  
            return;
        }

        offset = transform.position - target.position;
    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        if (target == null) return; // If no target is set (i.e., no player found), do nothing  
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

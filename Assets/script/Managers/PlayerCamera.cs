using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Player player1;
    [SerializeField] Player player2;

    public Transform whiteTransform;
    public Transform blackTransform;
    //GameObject playerS;
    //Player player;
    Volume volume;
    Vignette vignette;
    DepthOfField depthOfField;
    FilmGrain grain;

    [SerializeField] float speed = 20f;

    private void Start()
    {
        //if (TitleManager.saveData.IsWhiteUnlocked == true)
        //{
        //    playerS = player1;
        //}

        //else if (TitleManager.saveData.IsBlackUnlocked == true)
        //{
        //    playerS = player2;
        //}
        //target = playerS.GetComponent<Transform>();
        //player = playerS.GetComponent<Player>();
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out depthOfField);
        volume.profile.TryGet(out grain);
    }
    private void Update() 
    {

        if (TitleManager.saveData.IsWhiteUnlocked == true)
        {
            float intensity = (1 - player1.GetHpRatio()) * 0.75f;
            vignette.intensity.Override(intensity);
        }

        if (TitleManager.saveData.IsBlackUnlocked == true)
        {
            float intensity = (1 - player2.GetHpRatio()) * 0.75f;
            vignette.intensity.Override(intensity);
        }


        if (TitleManager.saveData.IsWhiteUnlocked == true)
        {
            if (player1 != null)
            {

                float playerX = player1.transform.position.x;
                float playerY = player1.transform.position.y;
                float cameraZ = transform.position.z;
                var targetPosition = new Vector3(playerX, playerY, cameraZ);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.unscaledDeltaTime);
                transform.position = targetPosition;
            }
        }

        if (TitleManager.saveData.IsBlackUnlocked == true)
        {
            if (player2 != null)
            {

                float playerX = player2.transform.position.x;
                float playerY = player2.transform.position.y;
                float cameraZ = transform.position.z;
                var targetPosition = new Vector3(playerX, playerY, cameraZ);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.unscaledDeltaTime);
                transform.position = targetPosition;
            }
        }

        //vignette.intensity.Override(0.5f - player.GetHpRatio());

        //if (target != null)
        //{

        //    float playerX = target.transform.position.x;
        //    float playerY = target.transform.position.y;
        //    float cameraZ = transform.position.z;
        //    var targetPosition = new Vector3(playerX, playerY, cameraZ);
        //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.unscaledDeltaTime);
        //    //transform.position = targetPosition;
        //}
    }

    public IEnumerator ShakeCoroutine()
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < 0.15f)
        {
            float x = Random.Range(-1f, 1f) * 0.4f;
            float y = Random.Range(-1f, 1f) * 0.4f;

            transform.position += new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = orignalPosition;
    }

    public IEnumerator Madness()
    {
        grain.intensity.Override(0.3f);
        yield return new WaitForSeconds(0.2f);
        grain.intensity.Override(0);
    }

    public void Scream()
    {
        StartCoroutine(Madness());
    }

    public void Blur()
    {
        depthOfField.aperture.Override(1);
    }

    public void Unblur()
    {
        depthOfField.aperture.Override(32);
    }

    public void ShakeCamera() 
    {
        StartCoroutine(ShakeCoroutine());
    }


}

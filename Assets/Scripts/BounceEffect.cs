using System.Collections;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float bounceHeight = 0.3f;// it decreases wiht every bounce
    public float bounceDuracion = 0.4f;
    public int bounceCount = 2;//Counts how much bounce is the item gonna make 


    public void StartBounce()
    {
        StartCoroutine(BounceHandler());

    }

    private IEnumerator BounceHandler()
    {
        Vector3 startPosition = transform.position;
        //i do it like this beacuse i need to manipulate the variable without changing the original values
        float localHeight = bounceHeight;
        float localDuration = bounceDuracion;

        for(int i = 0; i < bounceCount; i++)
        {
            yield return Bounce(transform, startPosition, localHeight, localDuration / 2);
            //Another coroutine to bounce
            localHeight *= 0.5f;
            localDuration *= 0.8f;

        }
        //bounce and go back to his place, avoid bounce around the map
        transform.position = startPosition;
    }

    private IEnumerator Bounce(Transform objectTransform, Vector3 start, float height, float duration)
    {
        Vector3  peak = start + Vector3.up * height;
        float elapsed = 0f;

        //Move upwards
        while(elapsed < duration)
        {
            objectTransform.position = Vector3.Lerp(start, peak, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        //Move Downwards

        while (elapsed < duration)
        {
            objectTransform.position = Vector3.Lerp(peak, start, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

    }
}

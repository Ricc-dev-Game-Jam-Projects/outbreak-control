using UnityEngine;

public class Plane : MonoBehaviour
{
    public RegionBehaviour Source;
    public RegionBehaviour Destination;

    public float Speed = 0.2f;
    public float LerpInterpolation = 0.1f;

    private bool Traveling = false;

    public void Travel(RegionBehaviour source, RegionBehaviour destination)
    {
        Source = source;
        Destination = destination;
        transform.position = source.gameObject.transform.position;
        Traveling = true;
    }

    private void Update()
    {
        if (Traveling)
        {
            transform.up = Vector2.Lerp(transform.up, (Destination.transform.position - transform.position), LerpInterpolation);
            transform.position = Vector2.MoveTowards(transform.position, Destination.transform.position, Speed * Time.deltaTime);
            
            if (Vector2.Distance(transform.position, Destination.gameObject.transform.position) <= 0.2f)
            {
                Traveling = false;
                gameObject.SetActive(false);
            }
        }

    }
}

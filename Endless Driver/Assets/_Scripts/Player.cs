
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public int collisionCount;

    public void Start()
    {
        collisionCount = 0;
    }

    public void Update()
    {
        if (collisionCount % 5 == 0)
        {
            GameFlowManager.Instance.targetTime = 1;
            GameFlowManager.Instance.playerScore = 0;
            GameFlowManager.Instance.flag = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            collisionCount++;
            var randomCount = Random.Range(0, 2);
            Debug.Log("Collided with car");
            GameFlowManager.Instance.WorldCurveTweener(randomCount);
        }
    }
}

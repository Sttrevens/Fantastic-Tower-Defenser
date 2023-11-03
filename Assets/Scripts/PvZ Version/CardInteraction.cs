using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardInteraction : MonoBehaviour
{
    public static CardInteraction instance;

    public GameObject cardPlaceholderUI;
    public GameObject[] skeletonCardPrefabs;
    public GameObject[] infiniteAmmoCardPrefabs;
    public GameObject[] bulletTimeCardPrefabs;

    public string currentCardName;
    private GameObject currentCardUI;  // To reference the instantiated UI card

    public PlayerShooting playerShooting;

    private float detectionRange = 1.0f; // Adjust the detection range as needed
    private LayerMask oneWayPlatformLayer;

    public Transform upSpawnPoint;
    public Transform downSpawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BulletHitCard(string cardTag)
    {
        GameObject selectedPrefab = null;
        switch (cardTag)
        {
            case "SkeletonCard":
                currentCardName = "SkeletonCard";
                selectedPrefab = skeletonCardPrefabs[Random.Range(0, skeletonCardPrefabs.Length)];
                break;
            case "InfiniteAmmoCard":
                currentCardName = "InfiniteAmmoCard";
                selectedPrefab = infiniteAmmoCardPrefabs[Random.Range(0, infiniteAmmoCardPrefabs.Length)];
                break;
            case "BulletTimeCard":
                currentCardName = "BulletTimeCard";
                selectedPrefab = bulletTimeCardPrefabs[Random.Range(0, bulletTimeCardPrefabs.Length)];
                break;
        }

        // Instantiate the card image as a child of the placeholder and store its reference
        if (selectedPrefab != null)
        {
            if (currentCardUI) Destroy(currentCardUI);
            currentCardUI = Instantiate(selectedPrefab, cardPlaceholderUI.transform);
        }
    }

    public void UseCard()
    {
        switch (currentCardName)
        {
            case "SkeletonCard":
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Check if the player is pressing W or S
                //if (Input.GetKey(KeyCode.W))
                //{
                //    Instantiate(playerShooting.unitPrefab, mousePosition, Quaternion.identity);
                //    if (currentCardUI)
                //    {
                //        Destroy(currentCardUI);
                //        currentCardName = "";
                //    }
                //}
                //else if (Input.GetKey(KeyCode.S))
                //{
                //    Instantiate(playerShooting.unitPrefab, mousePosition, Quaternion.identity);
                //    if (currentCardUI)
                //    {
                //        Destroy(currentCardUI);
                //        currentCardName = "";
                //    }
                //}
                Instantiate(playerShooting.unitPrefab, mousePosition, Quaternion.identity);
                if (currentCardUI)
                {
                    Destroy(currentCardUI);
                    currentCardName = "";
                }
                break;
            case "InfiniteAmmoCard":
                playerShooting.ActivateUnlimitedShooting();
                if (currentCardUI)
                {
                    Destroy(currentCardUI);
                    currentCardName = "";
                }
                break;
            case "BulletTimeCard":
                StartCoroutine(BulletTime());
                if (currentCardUI)
                {
                    Destroy(currentCardUI);
                    currentCardName = "";
                }
                break;
        }
    }

    private IEnumerator BulletTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(5f); // Use real time as the game time is slowed down
        Time.timeScale = 1f;
    }
}

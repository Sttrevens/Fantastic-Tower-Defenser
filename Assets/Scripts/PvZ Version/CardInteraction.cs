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

    public float cardAbilityDuration = 5.0f;

    public CardCoolDown[] cardCoolDowns;

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
                    foreach (CardCoolDown cardCoolDown in cardCoolDowns)
                    {
                        StartCoroutine(cardCoolDown.EnlargeAndDestroy(currentCardUI, cardAbilityDuration));
                        currentCardName = "";
                    }
                }
                break;
            case "BulletTimeCard":
                StartCoroutine(BulletTime());
                if (currentCardUI)
                {
                    foreach (CardCoolDown cardCoolDown in cardCoolDowns)
                    {
                        StartCoroutine(cardCoolDown.EnlargeAndDestroy(currentCardUI, cardAbilityDuration));
                        currentCardName = "";
                    }
                }
                break;
        }
    }

    private IEnumerator BulletTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(cardAbilityDuration); // Use real time as the game time is slowed down
        Time.timeScale = 1f;
    }

    //private IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    //{
    //    Image image = obj.GetComponent<Image>();
    //    SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
    //    float elapsedTime = 0;
    //    Color originalColor = image != null ? image.color : sprite.color;

    //    while (elapsedTime < duration)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
    //        if (image != null)
    //        {
    //            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    //        }
    //        else if (sprite != null)
    //        {
    //            sprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    //        }
    //        yield return null;
    //    }

    //    Destroy(obj);
    //}
}
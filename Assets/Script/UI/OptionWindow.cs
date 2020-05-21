using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public Image MuteButton;
    public Sprite[] MuteImages = new Sprite[2];

    public GameObject StartWindow;

    private void Start()
    {
        // Initialize the IAP module
        InAppPurchasing.InitializePurchasing();

#if UNITY_IOS && !UNITY_EDITOR
        // Restore purchases. This method only has effect on iOS.
        InAppPurchasing.RestorePurchases();
#endif
    }

    public void Opened()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            MuteButton.sprite = MuteImages[0];
        }
        else
        {
            MuteButton.sprite = MuteImages[1];
        }
    }

    public void pushCancel()
    {
        this.GetComponent<Animator>().SetBool("Open", false);
        StartWindow.GetComponent<Animator>().SetBool("Open", true);
    }

    public void pushMute()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            MuteButton.sprite = MuteImages[1];
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            MuteButton.sprite = MuteImages[0];
            StartWindow.GetComponent<StartWindow>().AS.PlayOneShot(StartWindow.GetComponent<StartWindow>().dicide);
        }
    }

    public void pushNoAds()
    {// Purchase a product using its name
     // EM_IAPConstants.Sample_Product is the generated name constant of a product named "Sample Product"
        InAppPurchasing.Purchase(EM_IAPConstants.Product_NoAds);
    }
    
    // Successful purchase handler
    void PurchaseCompletedHandler(IAPProduct product)
    {
        // Compare product name to the generated name constants to determine which product was bought
        switch (product.Name)
        {
            case EM_IAPConstants.Product_NoAds:
                Debug.Log("Sample_Product was purchased. The user should be granted it now.");
                PlayerPrefs.SetInt("noAds", 1);
                break;
                // More products here...
        }
    }

    // Failed purchase handler
    void PurchaseFailedHandler(IAPProduct product)
    {
        Debug.Log("The purchase of product " + product.Name + " has failed.");
    }
    
    // Subscribe to IAP restore events, these events are fired on iOS only.
    void OnEnable()
    {
        InAppPurchasing.RestoreCompleted += RestoreCompletedHandler;
        InAppPurchasing.RestoreFailed += RestoreFailedHandler;
    }

    // Successful restoration handler
    void RestoreCompletedHandler()
    {
        Debug.Log("All purchases have been restored successfully.");
    }

    // Failed restoration handler
    void RestoreFailedHandler()
    {
        Debug.Log("The purchase restoration has failed.");
    }

    // Unsubscribe
    void OnDisable()
    {
        InAppPurchasing.RestoreCompleted -= RestoreCompletedHandler;
        InAppPurchasing.RestoreFailed -= RestoreFailedHandler;
    }
}

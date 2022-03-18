using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to manage inventory UI
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Instance of inventory
    private Inventory inventory;
    
    // Colors when item is used
    public Color colorHovered = new Color(0.5137f, 0.4078431f,0.8235294f, 1f);
    public Color colorPressed = new Color(0.4627451f, 0.07450981f, 0.07058824f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("LeftHand Controller").GetComponent<Inventory>();

        inventory.batteryEvent.AddListener(UpdateUIBattery);
        inventory.flashEvent.AddListener(UpdateUIFlash);
        inventory.sonarEvent.AddListener(UpdateUISonar);
    }

    /// <summary>
    /// Item consuming animation
    /// </summary>
    /// <param name="sprite">Item sprite</param>
    /// <param name="anim">Item animator</param>
    private IEnumerator ConsumeItem(SpriteRenderer sprite, Animator anim)
    {
        var time = 0f;
        sprite.color = colorHovered;

        yield return new WaitForSeconds(0.3f);
        sprite.color = colorPressed;
        yield return new WaitForSeconds(0.25f);
        
        SetMissingItem(sprite, anim);
    }

    /// <summary>
    /// Change UI item appearance when item is consumed
    /// </summary>
    /// <param name="sprite">Item sprite</param>
    /// <param name="anim">Item animator</param>
    private void SetMissingItem(SpriteRenderer sprite, Animator anim)
    {
        sprite.color = new Color(1f, 1f, 1f, 0.3f);
        anim.enabled = false;
    }

    /// <summary>
    /// Change UI item appearance when item is owned
    /// </summary>
    /// <param name="sprite">Item sprite</param>
    /// <param name="anim">Item animator</param>
    private void SetOwnedItem(SpriteRenderer sprite, Animator anim)
    {
        sprite.color = new Color(1f, 1f, 1f, 1f);
        anim.enabled = true;
    }

    private void UpdateUIItem(bool status, string itemName)
    {
        var UIObject = GameObject.Find(itemName);
        var UISprite = UIObject.GetComponent<SpriteRenderer>();
        var UIAnim = UIObject.GetComponent<Animator>();
        
        if (!status)
        {
            StartCoroutine(ConsumeItem(UISprite, UIAnim));
            return;
        }
        
        SetOwnedItem(UISprite, UIAnim);
    }

    /// <summary>
    /// Update battery UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUIBattery(bool status)
    {
        UpdateUIItem(status, "Battery");
    }

    /// <summary>
    /// Update flash UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUIFlash(bool status)
    {
        UpdateUIItem(status, "Flash");
    }

    /// <summary>
    /// Update sonar UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUISonar(bool status)
    {
        UpdateUIItem(status, "Sonar");
    }
}

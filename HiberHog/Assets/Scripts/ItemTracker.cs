using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public PlayerScript player;

    public TMP_Text foodText;
    public TMP_Text toCollectText;

    public string foodTag, enemyTag, nestTag;

    public float spawnRadius = 1f;

    [Header("Items")]
    public int nestedFood = 0;
    public int foodGoal = 5;
    public int maxItems = 5;

    public List<GameObject> collectedItems = new List<GameObject>();

    private void Update()
    {
        foodText.text = "Food:" + collectedItems.Count + "/" + maxItems;
        toCollectText.text = "Collected:" + nestedFood + "/" + foodGoal;

        if (nestedFood >= foodGoal)
        {
            player.RestartScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == foodTag)
        {
            if (collectedItems.Count < maxItems)
            {
                int LayerFood = LayerMask.NameToLayer("Food");

                // add it to the collectedItems list and set it to inactive
                collectedItems.Add(other.gameObject);
                other.gameObject.layer = LayerFood;
                if (other.gameObject.GetComponent<Rigidbody>() == null)
                    other.gameObject.AddComponent<Rigidbody>();                
                other.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.tag == enemyTag)
        {
            if (player.isShielding == true)
            {
                //Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
                //enemyRb.AddForce(transform.up * player.knockBack);

                // I hate everything, but I hope this works :(
                other.gameObject.transform.Translate(Random.Range(-3, 3), 0, Random.Range(-3, 3));

            }
            else
            {
                // for every item in the list set its transform to be randomly near the player and active
                for (int i = 0; i < collectedItems.Count; i++)
                {
                    int LayerCollectedFood = LayerMask.NameToLayer("Collected Food");

                    collectedItems[i].transform.position =
                        new Vector3(Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius), transform.position.y + 1, Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius));
                    collectedItems[i].SetActive(true);
                    collectedItems[i].layer = LayerCollectedFood;
                }

                // reset the items list
                collectedItems.Clear();
            }
           
        }

        if (other.gameObject.tag == nestTag)
        {
            // take the amount of items in the list and add to nest item amount 
            nestedFood = collectedItems.Count;

            for (int j = 0; j < collectedItems.Count; j++)
            {
                Destroy(collectedItems[j]);
            }

            // reset the items list
            collectedItems.Clear();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}

/*
 * I think there are two ways I can do this...
 * 
 * 1: is to have an int for each item and when you hit into the item the int goes up
 * if an enemy creature collides with the player the int number of items is instantiated "dropped" randomly around the player, for each item
 * it would need a foreach loop for each item to be dropped, which is do-able but inefficient (I think anyways)
 * 
 * 2: is instead to have a public gameObject array (or list though idk if that would work) where each item collected is added to the array
 * if an enemy creature collides with the player each object in that array is just instantiated "dropped" and removed from the array (because I think it would also have to just remove each one)
 * it would also need a foreach loop for all items in the array to be dropped, though it would only be the one
 * however, I'm unsure if I'd need to use scriptable objects or if I could just use regular gameObjects (guess I'll have to find out :skull:)
 * 
 * either way both would require the dropped gameObject to have a new layer of "DroppedByPlayer" or something like that so the enemy creatures could pick them up
 * this would potentially also require any gameObject picked up by the player to have its layer changed back (or maybe not, depending on how it all works)
 * 
 * I want to try do 2 because it seems far more efficient than one 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    public string foodTag;
    public string enemyTag;

    public List<GameObject> collectedItems = new List<GameObject>();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            /*foreach (var i )
            {
                Instantiate(collectedItems, new Vector3(1, 1, 1), Quaternion.identity);
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == foodTag)
        {
            collectedItems.Add(other.gameObject);
            // add it to the collectedItems list
        }

        if (other.gameObject.tag == enemyTag)
        {
            // foreach loop to instantiate all the items randomly around the player
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
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

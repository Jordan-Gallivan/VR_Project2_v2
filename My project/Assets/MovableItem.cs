using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableItem : MonoBehaviour
{
    public bool itemIsSelected = false;
    public bool moveItem = false;
    public int movementPatternIndex;
    public Vector3[] movementPatterns =
    {
        new Vector3(0.0f, 0f, 0.0f),
        new Vector3(0.0f, 0f, 0.01f),
        new Vector3(0.1f, 0f, 0.01f)
    };

    // enum Movements
    // {
    //     forwardAndBackward,
    //     leftAndRight,
    //     tenThirtyToFourThirty,
    //     oneThirtyToSevenThirty
    // }
    //
    // public Movements movementPattern = new Movements();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemIsSelected && moveItem)
        {
            this.gameObject.transform.Translate(movementPatterns[movementPatternIndex]);
        }
    }
}

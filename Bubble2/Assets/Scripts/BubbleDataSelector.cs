using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDataSelector : PropertyAttribute
{
    public string collectionFieldName;

    public BubbleDataSelector(string collectionFieldName)
    {
        this.collectionFieldName = collectionFieldName;
    }
}

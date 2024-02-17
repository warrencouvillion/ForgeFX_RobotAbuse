using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using UnityEditor.SceneManagement;

public class EditorTests
{
    /**
     * Test that every object that can be dragged can be highligted
     */
    [Test]
    public void TestMovableObjectsHaveHighlight()
    {
        //Find all objects with a DragOnClick script
        var draggables = GameObject.FindObjectsOfType<DragOnClick>();
        foreach(var draggable in draggables)
        {
            var hilighter = draggable.gameObject.GetComponent<HighlightOnMouseOver>();
            Assert.NotNull(hilighter);
        }
        // Use the Assert class to test conditions
    }

    /**
     * Test that every object that has a DragOnClick also has a collider.
     */
    [Test]
    public void TestMovableObjectHaveCollider()
    {
        //Find all objects with a DragOnClick script
        var draggables = GameObject.FindObjectsOfType<DragOnClick>();
        foreach (var draggable in draggables)
        {
            var collider = draggable.gameObject.GetComponent<Collider>();
            Assert.NotNull(collider);
        }

    }


    
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

//[TextFixture]
public class RunTest
{
    InputTestFixture input = new InputTestFixture();

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestUFOLookAtTarget()
    {
        //Set the target at the origin.
        GameObject target = new GameObject();
        target.transform.position = new Vector3(0, 0, 0);

        //Create an object with a UFO movement
        GameObject ufo = new GameObject();
        ufo.AddComponent<UFOMovement>();
        //Position the ufo away from the origin
        ufo.transform.position = new Vector3(0, 10, 0);
        //Assert that the UFO is *not* looking at the target :w
        //(Testing the test)
        Vector3 lookAtVec = ufo.transform.position - target.transform.position;
        lookAtVec.Normalize();
        Assert.AreNotEqual(lookAtVec, ufo.transform.forward);

        //Now, simulate the <HOME> button to check that the ufo looks at the
        //target
        var keyboard = InputSystem.AddDevice<Keyboard>();

        


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}

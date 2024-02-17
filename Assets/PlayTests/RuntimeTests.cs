using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

//[TextFixture]
public class RunTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestUFOLookAtTarget()
    {
        //Set the target at the origin.
        GameObject target = new GameObject();
        target.transform.position = new Vector3(0, 0, 0);

        //Create mock input & time services
        var mockInput = Substitute.For<IInputInterface>();

        //Create an object with a UFO movement
        GameObject ufoObj = new GameObject();
        var ufoComp = ufoObj.AddComponent<UFOMovement>();
        
        //Position the ufoObj away from the origin
        ufoObj.transform.position = new Vector3(0, 10, 0);
        //Assert that the UFO is *not* looking at the target :w
        //(Testing the test)
        Vector3 lookAtVec = ufoObj.transform.position - target.transform.position;
        lookAtVec.Normalize();
        Assert.AreNotEqual(lookAtVec, ufoObj.transform.forward);

        





        yield return null;
    }
}

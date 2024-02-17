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
        //No movement on update.
        mockInput.GetAxis("Vertical").Returns(0);
        mockInput.GetAxis("Horizontal").Returns(0);
        mockInput.GetKey(KeyCode.LeftShift).Returns(false);
        mockInput.GetKey(KeyCode.RightShift).Returns(false);
        //But simulate <HOME> key being pressed
        mockInput.GetKeyDown(KeyCode.Home).Returns(true);

        //Create an object with a UFO movement
        GameObject ufoObj = new GameObject();
        var ufoComp = ufoObj.AddComponent<UFOMovement>();
        ufoComp.m_input = mockInput;
        ufoComp.m_target = target;
        
        //Position the ufoObj away from the origin
        ufoObj.transform.position = new Vector3(5, 0, 10);
        //Assert that the UFO is *not* looking at the target 
        //(Testing the test)
        Vector3 lookAtVec = target.transform.position - ufoObj.transform.position;
        lookAtVec.Normalize();
        Assert.AreNotEqual(lookAtVec, ufoObj.transform.forward);

        //Give it time to rotate.
        yield return new WaitForSeconds(1.0f);

        lookAtVec = target.transform.position - ufoObj.transform.position;
        lookAtVec.Normalize();
        //Check that the ufo is now looking at the target. If it is, it's 
        //forward vector should be pretty close to the lookAtVec calculated above.
        Assert.AreEqual(Vector3.Dot(lookAtVec, ufoObj.transform.forward), 1.0, 0.01);
    }
}

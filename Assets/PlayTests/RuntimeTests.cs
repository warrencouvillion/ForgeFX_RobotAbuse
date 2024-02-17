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
    const float tolerance = 0.02f;
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    [UnityTest]
    public IEnumerator TestUFOLookAtTarget()
    {
        //Set the target at the origin.
        GameObject target = new GameObject();
        target.transform.position = new Vector3(0, 0, 0);

        //Create mock input service
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
        //Because we don't expect all the floating point operations to yield 
        //exactly equal vectors, check the individual components.
        Assert.AreEqual(lookAtVec.x, ufoObj.transform.forward.x, 0.01);
        Assert.AreEqual(lookAtVec.y, ufoObj.transform.forward.y, 0.01);
        Assert.AreEqual(lookAtVec.z, ufoObj.transform.forward.z, 0.01);
    }

    [UnityTest]
    public IEnumerator TestUFOTranslation()
    {
        //Create mock input service
        var mockInput = Substitute.For<IInputInterface>();
        //No movement on update.
        mockInput.GetAxis("Vertical").Returns(1);
        mockInput.GetAxis("Horizontal").Returns(0);
        mockInput.GetKey(KeyCode.LeftShift).Returns(false);
        mockInput.GetKey(KeyCode.RightShift).Returns(false);
        mockInput.GetKeyDown(KeyCode.Home).Returns(false);

        var mockTime = Substitute.For<ITimeInterface>();
        mockTime.deltaTime.Returns(1);

        //Create an object with a UFO movement
        GameObject ufoObj = new GameObject();
        var ufoComp = ufoObj.AddComponent<UFOMovement>();
        ufoComp.m_input = mockInput;
        ufoComp.m_time = mockTime;
        ufoObj.transform.position = Vector3.zero;

        yield return null;


        //ufo should have moved 1 unit to the right
        Assert.AreEqual(ufoObj.transform.position.x, Vector3.forward.x, tolerance);
        Assert.AreEqual(ufoObj.transform.position.y, Vector3.forward.y, tolerance);
        Assert.AreEqual(ufoObj.transform.position.z, Vector3.forward.z, tolerance);
        
        mockInput.GetAxis("Vertical").Returns(0);
        mockInput.GetAxis("Horizontal").Returns(1);

        yield return null;

        var expectedPos = Vector3.forward + Vector3.right;
        Assert.AreEqual(ufoObj.transform.position.x, expectedPos.x, tolerance);
        Assert.AreEqual(ufoObj.transform.position.y, expectedPos.y, tolerance);
        Assert.AreEqual(ufoObj.transform.position.z, expectedPos.z, tolerance);
    }

    [UnityTest]
    public IEnumerator TestUFORotation()
    {
        //Create mock input service
        var mockInput = Substitute.For<IInputInterface>();
        //No movement on update.
        mockInput.GetAxis("Vertical").Returns(-1.0f);
        mockInput.GetAxis("Horizontal").Returns(0.0f);
        mockInput.GetKey(KeyCode.LeftShift).Returns(true);
        mockInput.GetKey(KeyCode.RightShift).Returns(false);
        mockInput.GetKeyDown(KeyCode.Home).Returns(false);

        var mockTime = Substitute.For<ITimeInterface>();
        mockTime.deltaTime.Returns(1);

        //Create an object with a UFO movement
        GameObject ufoObj = new GameObject();
        var ufoComp = ufoObj.AddComponent<UFOMovement>();
        ufoComp.m_input = mockInput;
        ufoComp.m_time = mockTime;
        ufoComp.m_rotationalSpeed = 1.0f;
        ufoObj.transform.position = Vector3.zero;
        ufoObj.transform.rotation = Quaternion.identity;

        yield return null;


        //ufo should have moved 1 unit to the right
        Assert.AreEqual(ufoObj.transform.eulerAngles.x, 1.0f, tolerance);
        Assert.AreEqual(ufoObj.transform.eulerAngles.y, 0.0f, tolerance);
        Assert.AreEqual(ufoObj.transform.eulerAngles.z, 0.0f, tolerance);
        
        mockInput.GetAxis("Vertical").Returns(0);
        mockInput.GetAxis("Horizontal").Returns(1);

        yield return null;
        Assert.AreEqual(ufoObj.transform.eulerAngles.x, 1.0f, tolerance);
        Assert.AreEqual(ufoObj.transform.eulerAngles.y, 1.0f, tolerance);
        Assert.AreEqual(ufoObj.transform.eulerAngles.z, 0.0f, tolerance);

    }
}

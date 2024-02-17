using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Interface to the Time methods and properties used by the application.
 * Allows mock values to be used for testing.
 */
public interface ITimeInterface 
{
    float deltaTime
    {
        get;
    }
}

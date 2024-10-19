using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFactory : MonoBehaviour
{
    public static void ConfigureBrick(Brick brick, BrickType type)
    {
        switch (type)
        {
            case BrickType.Weak:
                brick.brickLife = 1;
                break;
            case BrickType.Medium:
                brick.brickLife = 2;
                break;
            case BrickType.Strong:
                brick.brickLife = 3;
                break;
        }
    }

    public enum BrickType
    {
        Weak,   
        Medium, 
        Strong  
    }

}

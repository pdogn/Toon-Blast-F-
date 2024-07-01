using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockTypes
{
    Cube,
    Rocket,
    Bomp,
    LightingBox
}

public enum CubeTypes
{
    Red,
    Green,
    Yellow,
    Purple,
    Blue
}

public enum RocketTypes
{
    Vertical,
    Horizontal
}

public abstract class Block : MonoBehaviour
{
    public abstract BlockTypes blockType { get; }
    public abstract Vector3 spriteSize { get; }
    public Vector2 gridIndex;
    public Transform target;
    public abstract void SetupBlock();
    public abstract void DoTappedActions();
    public abstract void MoveToTarget(float arriveTime);
}

using UnityEngine;

public static class GameData
{
    public static float elapsed;
    public static Vector3 checkpointPosition;
    public static float checkpointYaw;
    public static float lavaHeight;

    public static void Set(Vector3 position, float yaw, float lavaH)
    {
        checkpointPosition = position;
        checkpointYaw = yaw;
        lavaHeight = lavaH;
    }

    public static void Reset() 
    {
        checkpointPosition = Vector3.zero;
        checkpointYaw = 0;
        lavaHeight = -5;
    }
}

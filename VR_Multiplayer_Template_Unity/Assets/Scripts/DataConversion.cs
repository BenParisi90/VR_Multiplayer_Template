using UnityEngine;

public static class DataConversion
{
    public static float[] Vector3ToFloatArray(Vector3 targetVector3)
    {
        return new float[]{targetVector3.x, targetVector3.y, targetVector3.z};
    }

    public static Vector3 FloatArrayToVector3(float[] targetArray)
    {
        return new Vector3(targetArray[0], targetArray[1], targetArray[2]);
    }
}

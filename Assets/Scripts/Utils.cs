using UnityEngine;

public class Utils {

    public static Vector3 RemoveYFromVector(Vector3 vector) {
        Vector3 newVector = vector * 1f;
        newVector.y = 0;
        return newVector;
    }

    public static Vector3 ClampVector(Vector3 vector, float maxMagnitude) {
        Vector3 newVector = vector * 1f;

        if (newVector.magnitude > maxMagnitude) {
            newVector = newVector.normalized * maxMagnitude;
        }

        return newVector;
    }

}

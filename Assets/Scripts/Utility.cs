using UnityEngine;

public static class Utility {

    public static float AngleBetweenObjects(Vector2 v1, Vector2 v2) {
        var v = new Vector2(v1.x - v2.x, v1.y - v2.y);
        var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 180;
        return angle;
    }

}
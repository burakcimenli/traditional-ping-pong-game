using UnityEngine;

public struct BorderMarkers {
    public Vector2 top;
    public Vector2 bot;
    public Vector2 right;
    public Vector2 left;

    public BorderMarkers(Vector2 top, Vector2 bot, Vector2 right, Vector2 left) {
        this.top = top;
        this.bot = bot;
        this.right = right;
        this.left = left;
    }

    public BorderMarkers(Transform holder) {
        top = Vector2.one * -1000;
        bot = Vector2.one * 1000;
        right = Vector2.one * -1000;
        left = Vector2.one * 1000;

        foreach (Transform marker in holder) {
            top = (marker.position.y > top.y) ? (Vector2)marker.position : top;
            bot = (marker.position.y < bot.y) ? (Vector2)marker.position : bot;
            right = (marker.position.x > right.x) ? (Vector2)marker.position : right;
            left = (marker.position.x < left.x) ? (Vector2)marker.position : left;
        }
    }

    public BorderMarkers(Transform[] borderMarkers) {
        top = Vector2.one * -1000;
        bot = Vector2.one * 1000;
        right = Vector2.one * -1000;
        left = Vector2.one * 1000;

        foreach (Transform marker in borderMarkers) {
            top = (marker.position.y > top.y) ? (Vector2)marker.position : top;
            bot = (marker.position.y < bot.y) ? (Vector2)marker.position : bot;
            right = (marker.position.x > right.x) ? (Vector2)marker.position : right;
            left = (marker.position.x < left.x) ? (Vector2)marker.position : left;
        }
    }
}

public struct CornerMarkers {
    public Vector2 left;
    public Vector2 right;

    public CornerMarkers(Transform cornerMarkerHolder) {
        Transform[] cornerMarkers = new Transform[] { cornerMarkerHolder.GetChild(0), cornerMarkerHolder.GetChild(1) };
        left = cornerMarkers[0].position.x > cornerMarkers[1].position.x ? cornerMarkers[1].position : cornerMarkers[0].position;
        right = left == (Vector2)cornerMarkers[1].position ? cornerMarkers[0].position : cornerMarkers[1].position;
    }
}

public struct VectorConstraint {
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public VectorConstraint(float minX, float maxX, float minY, float maxY) {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }

    public VectorConstraint(Transform border) {
        minY = 1000;
        maxY = -1000;
        maxX = -1000;
        minX = 1000;

        foreach (Transform marker in border) {
            maxY = (marker.position.y > maxY) ? marker.position.y : maxY;
            minY = (marker.position.y < minY) ? marker.position.y : minY;
            maxX = (marker.position.x > maxX) ? marker.position.x : maxX;
            minX = (marker.position.x < minX) ? marker.position.x : minX;
        }
    }

    public VectorConstraint(Transform[] borders) {
        minY = 1000;
        maxY = -1000;
        maxX = -1000;
        minX = 1000;

        foreach (Transform marker in borders) {
            maxY = (marker.position.y > maxY) ? marker.position.y : maxY;
            minY = (marker.position.y < minY) ? marker.position.y : minY;
            maxX = (marker.position.x > maxX) ? marker.position.x : maxX;
            minX = (marker.position.x < minX) ? marker.position.x : minX;
        }
    }

    public override string ToString() {
        return "minX : " + minX + ", maxX : " + maxX + ", minY : " + minY + ", maxY : " + maxY;
    }
}

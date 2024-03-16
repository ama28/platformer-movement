using UnityEngine;

public static class Rigidbody2DExt {

    public static void AddExplosionForce(this Rigidbody2D rb, bool setVelocityToZero, float explosionForce, Vector2 explosionPosition, float explosionRadius, Vector2 explosionDirModifier, float upwardsModifier = 0.0F, float maxAngularChangeInDegrees = 720.0F, ForceMode2D mode = ForceMode2D.Impulse) {
        Vector2 explosionDir = rb.position - explosionPosition;
        if (explosionDir.magnitude > explosionRadius) {
            return;
        }

        Vector2 explosionDirModified;
        float angularChange;
        float explosionDistance = (explosionDir.magnitude / explosionRadius);
        
        /* Scaling explosionDir */
        explosionDir.Normalize();
        explosionDirModified = Vector2.Scale(explosionDir, explosionDirModifier);

        /* Calculating angularChange */
        angularChange = maxAngularChangeInDegrees * (1 - explosionDistance);
        if (rb.position.x >= explosionPosition.x) {
            angularChange *= -1.0F;
        }

        /* Adding forces */
        rb.velocity = setVelocityToZero ? Vector2.zero : new Vector2(rb.velocity.x, 0);
        rb.angularVelocity = 0;
        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDirModified, mode);
        rb.AddTorque((angularChange * Mathf.Deg2Rad) * rb.inertia, ForceMode2D.Impulse);
    }
}
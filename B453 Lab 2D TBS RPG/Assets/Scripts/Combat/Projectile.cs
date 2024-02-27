using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Stats")]
    [Tooltip("The name of this projectile.")]
    [SerializeField] string projectileName;
    [Tooltip("The damage this projectile will inflict.")]
    [SerializeField] int damageDealt;
    [Tooltip("This value represents what fraction of a second of time will be added to the travel time per meter traveled.")]
    [SerializeField] float speedModifier;
    [Tooltip("The path the projectile will travel.")]
    [SerializeField] AnimationCurve travelPath;
    [Tooltip("Whether this is a parabolic path or not.")]
    [SerializeField] bool isParabolic;
    [Tooltip("The CombatActions scriptable object associated with this attack.")]
    [SerializeField] CombatActions combatActions;

    // Reference to the target.
    private CombatCharacter target;
    // The starting spawn location of this projectile.
    private Vector2 startLocation;
    // The total initial distance to the target.
    private float distanceToTarget;
    // The default travel time of the arrow in seconds before reaching the target. This represents the minimum duration of travel time possible for a projectile.
    private float travelTime = .25f;
    // How much time has elapsed since the projectile was fired.
    private float currentTime = 0;

    private void Start()
    {
        // Save the spawn location of the projectile.
        startLocation = transform.position;
    }
    private void Update()
    {
        // Increment the current time since spawning.
        currentTime += Time.deltaTime;
        // The normalizedTime below represents what percentage the projectile is through its total travel time.
        float normalizedTime = currentTime / travelTime;
        float yPos;
        float xPos;

        // Check to see if the travel time percentage is still less than or equal to 100% of the total time...
        if (normalizedTime <= 1.0f)
        {
            // Check to see if the projectile is a parabolic projectile...
            if (isParabolic)
            {
                // Calculate the percentage of how far the projectile has already traveled along the total distance needed to hit the target. (Not used)
                float currentDistance = Mathf.Lerp(0, distanceToTarget, normalizedTime);

                // Calculate how high the arrow needs to be at any moment in time. Notice we're adjusting the maximum height depending on the total distance too.
                // travelPath.Evaluate(normalizedTime) will return .5 when normalizedTime is 0.5f. This means when the arrow has completed 50% of its total travel time, the curve value is .5.
                // We then multiply this by the distanceToTarget divided by 2, which means the max height it ever reaches will be 25% the length of the total distance.
                // So, the further away our target is, the higher the arrow needs to shoot to be able to travel the required distance.
                float height = travelPath.Evaluate(normalizedTime) * (distanceToTarget / 2);

                // Calculates the new position the arrow needs to move to on this frame.
                // Notice we are linearly interpolating between the original spawn location, and the final location (which is the target). We're using the percentage of total travel time elapsed so far as the alpha value.
                // So basically we're finding a position on the line from the start location to the end location at X percent of the way along that line.
                // So, when normalizedTime is 0.5 meaning we're halfway thorugh our total travel time, the arrow should be halfway there.
                // So, if our start position was (0,0) and we're 50% through the travel time to position (10,0), newPosition would be (5,0) which is halfway there.
                Vector2 newPosition = Vector2.Lerp(startLocation, target.transform.position, normalizedTime);

                // Make sure the y value of our newPosition is equal to the height we calculated before.
                newPosition.y += height;

                // Calculate the velocity vectory of the arrow.
                Vector2 velocityVector = newPosition - (Vector2)transform.position;

                // Calcualte the angle the arrow should point. For those of you who hate trigonometry, arctangent calculates the angle from the positive x direction to the vector represented by (x, y).
                // So the normal math formula is angle = arctangent(y component of vector, x component of vector). The result will be in radians, which you can then multiply by 180 / PI. Which is what Mathf.Rad2Deg is.
                // The result will be the angle from the "horizon" or positive x axis upwards towards the vector you want to look at.
                float angle = Mathf.Atan2(velocityVector.y, velocityVector.x) * Mathf.Rad2Deg;

                // Rotate the arrow to face the direction of movement.
                // We're directly setting the x y and z rotation values of the projectile to point in the correct angle calculated at each frame.
                transform.rotation = Quaternion.Euler(0, 0, angle);

                // Finally, let's move the projectile to the new location.
                transform.position = newPosition;
            }
            // If it's NOT a parabolic path...
            else
            {
                // Just launch the projectile in the positive X direction while still taking into account travel time.
                transform.position = Vector2.Lerp(startLocation, target.transform.position, normalizedTime);
            }
        }
        // If the current travel time is more than 100% of the total time...
        else
        {
            // Deal damage to the target.
            target.TakeDamage(damageDealt);
            // End the turn of whoever is taking their turn right now.
            TurnManager.instance.EndTurn();
            // Destroy this projectile.
            Destroy(gameObject);
        }
    }

    // Grab a reference to the target, and use their position to determine how far the projectile needs to travel as well as how long it'll take to get there.
    public void SetTarget(CombatCharacter target)
    {
        // Set target.
        this.target = target;
        // Calculate initial distance to the target from shooting position.
        distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
        // Calculate the total travel time of the projectile. The number 50f below represents how many more seconds are added to the travel time per meter of distance.
        // So here, it'll add 1/50 = 0.02 more seconds per meter of travel distance onto the original travel time of 1 second.
        travelTime += distanceToTarget / speedModifier;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityMeshAgents
{
    public static class Agent
    {
        /// <summary>
        /// Applies the position and rotation of a given transform to a NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to which the transform should be applied.</param>
        /// <param name="targetTransform">The Transform whose position and rotation should be applied to the agent.</param>
        /// <exception cref="ArgumentNullException">Thrown if either argument is null.</exception>
        public static void ApplyTransform(NavMeshAgent navAgent, Transform targetTransform)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            if (targetTransform == null)
            {
                throw new ArgumentNullException(nameof(targetTransform), "Target Transform cannot be null.");
            }

            navAgent.transform.Translate(targetTransform.position, Space.World);
            navAgent.transform.Rotate(targetTransform.eulerAngles, Space.World);
        }
        /// <summary>
        /// Applies position and rotation vectors to a NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to which the vectors should be applied.</param>
        /// <param name="position">The position vector to apply.</param>
        /// <param name="rotation">The rotation vector to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void ApplyVectors(NavMeshAgent navAgent, Vector3 position, Vector3 rotation)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            // Optional: Additional checks can be added here if there are specific constraints on position or rotation

            navAgent.transform.Translate(position, Space.World);
            navAgent.transform.Rotate(rotation, Space.World);
        }
        /// <summary>
        /// Sets the destination for a NavMeshAgent with an offset.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to which Set a Destination.</param>
        /// <param name="destination">The destination to apply</param>
        /// <param name="offset">The offset to apply</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetDestinationWithOffset(NavMeshAgent navAgent, Vector3 destination, Vector3 offset)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }
            navAgent.SetDestination(destination + offset);
        }
        /// <summary>
        /// Toggle the NavMeshAgent's pathfinding on or off.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to control.</param>
        /// <param name="isEnabled">True to enable pathfinding, False to disable it.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided NavMeshAgent is null.</exception>
        public static void TogglePathfinding(NavMeshAgent navAgent, bool isEnabled)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }
            navAgent.enabled = isEnabled;
        }
        /// <summary>
        /// Warps the NavMeshAgent to a specified new position.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to warp.</param>
        /// <param name="newPosition">The new position to warp the agent to.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void WarpToPosition(NavMeshAgent navAgent, Vector3 newPosition)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.Warp(newPosition);
        }
        /// <summary>
        /// Calculates a path to a specified target for the NavMeshAgent and immediately assigns it to the agent's path.
        /// Use this method when you want the agent to start following the new path right away.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent whose path is being calculated and assigned.</param>
        /// <param name="target">The destination target for the path calculation.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void CalculateAndAssignPath(NavMeshAgent navAgent, Vector3 target)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.CalculatePath(target, navAgent.path);
        }
        /// <summary>
        /// Sets whether the provided NavMeshAgent should follow its path or stop.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to control.</param>
        /// <param name="shouldFollowPath">True to enable path following, False to stop the agent.</param>
        public static void TogglePathFollowing(NavMeshAgent navAgent, bool shouldFollowPath)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            navAgent.isStopped = !shouldFollowPath;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent is currently stopped.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for stopped state.</param>
        /// <returns>True if the agent is stopped, otherwise false.</returns>
        public static bool IsStopped(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }

            return navAgent.isStopped;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent is currently moving.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for movement.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        /// <returns>True if the agent is moving, otherwise false.</returns>
        public static bool IsMoving(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            return navAgent.velocity.sqrMagnitude > 0f;
        }
        /// <summary>
        /// Determines if the NavMeshAgent has reached its destination.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to check.</param>
        /// <returns>True if the agent has reached its destination, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static bool HasReachedDestination(NavMeshAgent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            bool isPathComplete = !agent.pathPending;
            bool isWithinStoppingDistance = agent.remainingDistance <= agent.stoppingDistance;
            bool isStationary = !agent.hasPath || agent.velocity.sqrMagnitude == 0f;

            return isPathComplete && isWithinStoppingDistance && isStationary;
        }
        /// <summary>
        /// Calculates a path for the NavMeshAgent to a specified target and outputs the path.
        /// This method is useful for when you need the path for custom processing or analysis before applying it to the agent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the path is calculated.</param>
        /// <param name="target">The destination target for the path calculation.</param>
        /// <param name="path">The calculated path as an output parameter. This is only valid if the method returns true.</param>
        /// <returns>True if the path calculation is successful, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static bool TryCalculateNavPath(NavMeshAgent navAgent, Vector3 target, out NavMeshPath path)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            path = new NavMeshPath();
            return navAgent.CalculatePath(target, path);
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent has a complete path.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for a complete path.</param>
        /// <returns>True if the agent has a complete path, otherwise false.</returns>
        public static bool HasCompletePath(NavMeshAgent navAgent)
        {
            return navAgent != null && navAgent.pathStatus == NavMeshPathStatus.PathComplete;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent has a partial path.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for a partial path.</param>
        /// <returns>True if the agent has a partial path, otherwise false.</returns>
        public static bool HasPartialPath(NavMeshAgent navAgent)
        {
            return navAgent != null && navAgent.pathStatus == NavMeshPathStatus.PathPartial;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent has an invalid path.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for an invalid path.</param>
        /// <returns>True if the agent has an invalid path, otherwise false.</returns>
        public static bool HasInvalidPath(NavMeshAgent navAgent)
        {
            return navAgent != null && navAgent.pathStatus == NavMeshPathStatus.PathInvalid;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent is currently on the NavMesh.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for NavMesh presence.</param>
        /// <returns>True if the agent is on the NavMesh, otherwise false.</returns>
        public static bool IsOnNavMesh(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }
            return navAgent.isOnNavMesh;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent has line of sight to the specified target position on the NavMesh.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for line of sight.</param>
        /// <param name="target">The target position to check line of sight to.</param>
        /// <returns>True if the agent has line of sight to the target, otherwise false.</returns>
        public static bool HasLineOfSightToTarget(NavMeshAgent navAgent, Vector3 target)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }

            NavMeshHit hit;
            return NavMesh.Raycast(navAgent.transform.position, target, out hit, NavMesh.AllAreas);
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent currently has a pending path calculation.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for pending path calculation.</param>
        /// <returns>True if a path calculation is pending, otherwise false.</returns>
        public static bool HasPendingPath(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }

            return navAgent.pathPending;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent is currently on an off-mesh link.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for off-mesh link presence.</param>
        /// <returns>True if the agent is on an off-mesh link, otherwise false.</returns>
        public static bool IsAgentOnOffMeshLink(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }

            return navAgent.isOnOffMeshLink;
        }
        /// <summary>
        /// Checks if the provided NavMeshAgent currently has a stale path.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to check for a stale path.</param>
        /// <returns>True if the agent has a stale path, otherwise false.</returns>
        public static bool HasStalePath(NavMeshAgent navAgent)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }

            return navAgent.isPathStale;
        }
        /// <summary>
        /// Sets the size of the provided NavMeshAgent by modifying its height and radius properties.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent whose size should be set.</param>
        /// <param name="height">The new height value for the agent.</param>
        /// <param name="radius">The new radius value for the agent.</param>
        public static void SetSize(NavMeshAgent navAgent, float height, float radius)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.height = height;
            navAgent.radius = radius;
        }
        /// <summary>
        /// Configures the obstacle avoidance settings for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent to configure obstacle avoidance for.</param>
        /// <param name="isEnabled">True to enable obstacle avoidance, False to disable it.</param>
        /// <param name="avoidanceRadius">The avoidance radius for the agent (optional, default is 0.5).</param>
        public static void ConfigureObstacleAvoidance(NavMeshAgent navAgent, bool isEnabled, float avoidanceRadius = 0.5f)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.obstacleAvoidanceType = isEnabled ? ObstacleAvoidanceType.HighQualityObstacleAvoidance : ObstacleAvoidanceType.NoObstacleAvoidance;
            navAgent.radius = avoidanceRadius;
        }
        /// <summary>
        /// Sets the stopping distance for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the stopping distance is being set.</param>
        /// <param name="stoppingDistance">The new stopping distance value for the agent.</param>
        public static void SetStoppingDistance(NavMeshAgent navAgent, float stoppingDistance)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.stoppingDistance = stoppingDistance;
        }
        /// <summary>
        /// Sets the velocity of the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the velocity is being set.</param>
        /// <param name="newVelocity">The new velocity vector for the agent.</param>
        public static void SetVelocity(NavMeshAgent navAgent, Vector3 newVelocity)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.velocity = newVelocity;
        }
        /// <summary>
        /// Sets the area mask for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the area mask is being set.</param>
        /// <param name="newAreaMask">The new area mask value for the agent.</param>
        public static void SetAreaMask(NavMeshAgent navAgent, int newAreaMask)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.areaMask = newAreaMask;
        }
        /// <summary>
        /// Sets the acceleration for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the acceleration is being set.</param>
        /// <param name="newAcceleration">The new acceleration value for the agent.</param>
        public static void SetAcceleration(NavMeshAgent navAgent, float newAcceleration)
        {
            if (navAgent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }

            navAgent.acceleration = newAcceleration;
        }
        /// <summary>
        /// Sets the auto-braking behavior for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the auto-braking behavior is being set.</param>
        /// <param name="enableAutoBraking">True to enable auto-braking, False to disable it.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void SetAutoBrakingEnabled(NavMeshAgent navAgent, bool enableAutoBraking)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.autoBraking = enableAutoBraking;
        }
        /// <summary>
        /// Sets the auto-repath behavior for the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent for which the auto-repath behavior is being set.</param>
        /// <param name="enableAutoRepath">True to enable auto-repath, False to disable it.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>"
        public static void SetAutoRepath(NavMeshAgent navAgent, bool enableAutoRepath)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.autoRepath = enableAutoRepath;
        }
        /// <summary>
        /// Sets the angular speed of the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent whose angular speed is to be set.</param>
        /// <param name="newAngularSpeed">The new angular speed to assign to the agent.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>"
        public static void SetAngularSpeed(NavMeshAgent navAgent, float newAngularSpeed)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.angularSpeed = newAngularSpeed;
        }
        /// <summary>
        /// Sets the speed of the provided NavMeshAgent.
        /// </summary>
        /// <param name="navAgent">The NavMeshAgent whose speed is to be set.</param>
        /// <param name="newSpeed">The new speed to assign to the agent.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void SetSpeed(NavMeshAgent navAgent, float newSpeed)
        {
            if (navAgent == null)
            {
                throw new ArgumentNullException(nameof(navAgent), "NavMeshAgent cannot be null.");
            }

            navAgent.speed = newSpeed;
        }
        //what other methods could we add here?
    }
}

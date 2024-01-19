using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityMeshAgents
{
    public static class AgentDo
    {
        /// <summary>
        /// Applies the position and rotation of a given transform to a NavMeshAgent.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to which the transform should be applied.</param>
        /// <param name="targetTransform">The Transform whose position and rotation should be applied to the agent.</param>
        /// <exception cref="ArgumentNullException">Thrown if either argument is null.</exception>
        public static void ApplyPositionAndRotationToAgent(NavMeshAgent agent, Transform targetTransform)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            if (targetTransform == null)
            {
                throw new ArgumentNullException(nameof(targetTransform), "Target Transform cannot be null.");
            }

            agent.transform.Translate(targetTransform.position, Space.World);
            agent.transform.Rotate(targetTransform.eulerAngles, Space.World);
        }
        /// <summary>
        /// Applies position and rotation vectors to a NavMeshAgent.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to which the vectors should be applied.</param>
        /// <param name="position">The position vector to apply.</param>
        /// <param name="rotation">The rotation vector to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void ApplyAgentVectors(NavMeshAgent agent, Vector3 position, Vector3 rotation)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            // Optional: Additional checks can be added here if there are specific constraints on position or rotation

            agent.transform.Translate(position, Space.World);
            agent.transform.Rotate(rotation, Space.World);
        }
        /// <summary>
        /// Sets the destination for a NavMeshAgent.
        /// </summary>
        /// <param name="agent">The NavMeshAgent whose destination is being set.</param>
        /// <param name="destination">The destination to set for the agent.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void SetAgentDestination(NavMeshAgent agent, Vector3 destination)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            // Optional: Add additional validation for destination if necessary

            agent.SetDestination(destination);
        }
        /// <summary>
        /// Sets the destination for a NavMeshAgent with an offset.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to which Set a Destination.</param>
        /// <param name="destination">The destination to apply</param>
        /// <param name="offset">The offset to apply</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetDestinationWithOffset(NavMeshAgent agent, Vector3 destination, Vector3 offset)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }
            agent.SetDestination(destination + offset);
        }
        /// <summary>
        /// Finds the closest navmesh edge to the NavMeshAgent and outputs the result.
        /// </summary>
        /// <param name="hit">The hit information for the closest edge. This is an output parameter.</param>
        /// <param name="agent">The NavMeshAgent for which the closest edge is being found.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void FindClosestNavMeshEdge(out NavMeshHit hit, NavMeshAgent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            agent.FindClosestEdge(out hit);
        }
        /// <summary>
        /// Toggle the NavMeshAgent's pathfinding on or off.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="isEnabled"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ToggleAgentPathfinding(NavMeshAgent agent, bool isEnabled)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }
            agent.enabled = isEnabled;
        }
        /// <summary>
        /// Stops the NavMeshAgent from moving and clears its current path.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to be reset.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void ResetAgentPath(NavMeshAgent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            agent.ResetPath();
        }
        /// <summary>
        /// Determines if the NavMeshAgent has reached its destination.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to check.</param>
        /// <returns>True if the agent has reached its destination, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static bool IsAgentAtDestination(NavMeshAgent agent)
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
        /// Warps the NavMeshAgent to a specified new position.
        /// </summary>
        /// <param name="agent">The NavMeshAgent to warp.</param>
        /// <param name="newPosition">The new position to warp the agent to.</param>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static void WarpAgentToPosition(NavMeshAgent agent, Vector3 newPosition)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            agent.Warp(newPosition);
        }
        /// <summary>
        /// Calculates a path for the NavMeshAgent to a specified target and outputs the path.
        /// </summary>
        /// <param name="agent">The NavMeshAgent for which the path is calculated.</param>
        /// <param name="target">The destination target for the path calculation.</param>
        /// <param name="path">The calculated path as an output parameter. This is only valid if the method returns true.</param>
        /// <returns>True if the path calculation is successful, false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the NavMeshAgent is null.</exception>
        public static bool CalculatePathToTarget(NavMeshAgent agent, Vector3 target, out NavMeshPath path)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            path = new NavMeshPath();
            return agent.CalculatePath(target, path);
        }
        public static bool SamplePosition(Vector3 sourcePosition, out Vector3 nearestPoint, float maxDistance, int areaMask)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(sourcePosition, out hit, maxDistance, areaMask))
            {
                nearestPoint = hit.position;
                return true;
            }
            nearestPoint = Vector3.zero;
            return false;
        }
        public static bool IsPathComplete(NavMeshAgent agent)
        {
            return agent != null && agent.pathStatus == NavMeshPathStatus.PathComplete;
        }
        public static bool IsPathPartial(NavMeshAgent agent)
        {
            return agent != null && agent.pathStatus == NavMeshPathStatus.PathPartial;
        }
        public static bool IsPathInvalid(NavMeshAgent agent)
        {
            return agent != null && agent.pathStatus == NavMeshPathStatus.PathInvalid;
        }
        public static bool IsAgentOnNavMesh(NavMeshAgent agent)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }
            return agent.isOnNavMesh;
        }
        public static bool IsAgentSightToTarget(NavMeshAgent agent, Vector3 target)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }
            NavMeshHit hit;
            return NavMesh.Raycast(agent.transform.position, target, out hit, NavMesh.AllAreas);
        }

        public static void SetAgentSize(NavMeshAgent agent, float height, float radius)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.height = height;
            agent.radius = radius;
        }
        public static void SetAgentHeightAndBaseOffset(NavMeshAgent agent, float height, float baseOffset)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.height = height;
            agent.baseOffset = baseOffset;
        }
        public static void SetObstacleAvoidance(NavMeshAgent agent, bool isEnabled, float avoidanceRadius = 0.5f)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.obstacleAvoidanceType = isEnabled ? ObstacleAvoidanceType.HighQualityObstacleAvoidance : ObstacleAvoidanceType.NoObstacleAvoidance;
            agent.radius = avoidanceRadius;
        }
        public static void SetAgentStoppingDistance(NavMeshAgent agent, float stoppingDistance)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.stoppingDistance = stoppingDistance;
        }
        public static void SetAgentVelocity(NavMeshAgent agent, Vector3 velocity)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.velocity = velocity;
        }
        public static void SetAgentAreaMask(NavMeshAgent agent, int areaMask)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.areaMask = areaMask;
        }
        public static void SetAgentAcceleration(NavMeshAgent agent, float acceleration)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.acceleration = acceleration;
        }
        public static void SetAgentAutoBraking(NavMeshAgent agent, bool autoBraking)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.autoBraking = autoBraking;
        }
        public static void SetAgentAutoRepath(NavMeshAgent agent, bool autoRepath)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.autoRepath = autoRepath;
        }
        public static void SetAgentAngularSpeed(NavMeshAgent agent, float angularSpeed)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.angularSpeed = angularSpeed;
        }
        public static void SetAgentSpeed(NavMeshAgent agent, float newSpeed)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.speed = newSpeed;
        }      
        //what other methods could we add here?
    }
}

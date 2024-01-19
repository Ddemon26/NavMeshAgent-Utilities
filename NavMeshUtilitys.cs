using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityMeshAgents
{
    public static class AgentDo
    {
        public static void ApplyAgentTransform(NavMeshAgent agent, Transform applyTransform)
        {
            if (agent == null || applyTransform == null)
            {
                Debug.LogError("NavMeshAgent or Transform is null.");
                return;
            }
            agent.transform.Translate(applyTransform.position, Space.World);
            agent.transform.Rotate(applyTransform.eulerAngles, Space.World);
        }
        public static void ApplyAgentVectors(NavMeshAgent agent, Vector3 position, Vector3 rotation)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.transform.Translate(position, Space.World);
            agent.transform.Rotate(rotation, Space.World);
        }
        public static void SetAgentDestination(NavMeshAgent agent, Vector3 destination)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.SetDestination(destination);
        }
        public static void SetDestinationWithOffset(NavMeshAgent agent, Vector3 destination, Vector3 offset)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.SetDestination(destination + offset);
        }
        public static void FindClosestNavMeshEdge(out NavMeshHit hit, NavMeshAgent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent), "NavMeshAgent cannot be null.");
            }

            agent.FindClosestEdge(out hit);
        }
        public static void ToggleAgentPathfinding(NavMeshAgent agent, bool isEnabled)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.enabled = isEnabled;
        }
        public static void ResetAgentPath(NavMeshAgent agent)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.ResetPath();
        }
        public static bool IsAgentAtDestination(NavMeshAgent agent)
        {
            return agent != null && !agent.pathPending &&
                   agent.remainingDistance <= agent.stoppingDistance &&
                   (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
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
        public static bool CalculatePathToTarget(NavMeshAgent agent, Vector3 target, out NavMeshPath path)
        {
            path = new NavMeshPath();
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return false;
            }
            return agent.CalculatePath(target, path);
        }
        public static void WarpAgentToPosition(NavMeshAgent agent, Vector3 newPosition)
        {
            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is null.");
                return;
            }
            agent.Warp(newPosition);
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

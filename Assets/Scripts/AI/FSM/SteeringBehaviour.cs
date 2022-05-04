using System;
using LivingEntities;
using PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.FSM
{
    [Flags]
    public enum SteeringForces
    {
        Seek = 1,
        Flee = 2,
        Arrive = 4,
        Pursuit = 8,
        Evade = 16,
        Wander = 32,
        ObstacleAvoidance = 64,
        FollowPath = 128,
        WallAvoidance = 256,
        OffsetPursuit = 512,
    }

    public class SteeringBehaviour
    {
        private const float _fleeDistanceSqr = 100;
        private readonly LivingEntity _owner;

        // Wander
        private Vector3 _wanderTarget;
        private float _wanderRadius;
        private float _wanderDistance;
        private float _wanderJitter;
        private float _wanderAngle;

        // FollowPath
        private Vector3 currentWaypoint;

        public SteeringBehaviour(LivingEntity owner)
        {
            _owner = owner;

            _wanderRadius = 2f;
            _wanderDistance = 1f;
            _wanderJitter = 1f;

            _wanderAngle = Random.Range(0f, 1f) * 2f * Mathf.PI;
            _wanderTarget = new Vector3(_wanderRadius * Mathf.Cos(_wanderAngle), 0,
                _wanderRadius * Mathf.Sin(_wanderAngle));
        }

        public SteeringForces SteeringForces { get; set; }

        public Vector3 Calculate(Vector3 targetPos)
        {
            return CalculateSimple(targetPos);
        }

        private Vector3 CalculateSimple(Vector3 targetPos)
        {
            Vector3 steeringForce = Vector3.zero;

            if (IsOn(SteeringForces.Seek))
                steeringForce += Seek(targetPos);
            if (IsOn(SteeringForces.Flee))
                steeringForce += Flee(targetPos);
            if (IsOn(SteeringForces.Arrive))
                steeringForce += Arrive(targetPos, 5f);
            if (IsOn(SteeringForces.Pursuit) && _owner.Target)
                steeringForce += Pursuit(_owner.Target);
            if (IsOn(SteeringForces.Evade) && _owner.Target)
                steeringForce += Evade(_owner.Target);
            if (IsOn(SteeringForces.Wander))
                steeringForce += 0.1f * Wander();
            if (IsOn(SteeringForces.ObstacleAvoidance))
                steeringForce += ObstacleAvoidance(targetPos);
            if (IsOn(SteeringForces.WallAvoidance))
                steeringForce += WallAvoidance();

            steeringForce = Vector3.ClampMagnitude(steeringForce, _owner.MaxForce);

            return steeringForce;
        }

        private Vector3 Seek(Vector3 targetPos)
        {
            if (!(_owner is Player))
            {
                Vector3 direction = (targetPos - _owner.transform.position).normalized;
            
                targetPos -= direction * 1f;
            
                // When Point is behind dont move
                if (Vector3.Dot(direction, _owner.transform.forward) < 0)
                {
                    _owner.ResetVelocity();
                    return Vector3.zero;
                }
            }
            
            Vector3 desiredVelocity =
                (targetPos - _owner.transform.position).normalized *
                _owner.MaxSpeed; // normalisierter vektor a = a / a.magnitude
            
            return desiredVelocity - _owner.Velocity;
        }

        private Vector3 Flee(Vector3 source)
        {
            Vector3 fromSource = _owner.transform.position - source;

            if (Vector3.SqrMagnitude(fromSource) > _fleeDistanceSqr)
                return Vector3.zero;

            Vector3 desiredVelocity = (_owner.transform.position - source).normalized * _owner.MaxSpeed;

            return desiredVelocity - _owner.Velocity;
        }

        private Vector3 Arrive(Vector3 targetPos, float decelarationDistance)
        {
            Vector3 ownerPos = _owner.transform.position;

            Vector3 toTarget = targetPos - ownerPos;
            float distanceSqr = toTarget.sqrMagnitude;
            float decelDistSqr = decelarationDistance * decelarationDistance;

            Vector3 desiredVelocity = (targetPos - ownerPos).normalized *
                                      _owner.MaxSpeed;

            if (distanceSqr < decelDistSqr)
                desiredVelocity = Vector3.Lerp(Vector3.zero, desiredVelocity, Mathf.Sqrt(distanceSqr) / decelDistSqr);

            return desiredVelocity - _owner.Velocity;
        }

        private Vector3 Pursuit(LivingEntity target)
        {
            if (Mathf.Abs(Vector3.Dot(target.HeadingDirection, _owner.HeadingDirection)) >= 0.95f)
                return Seek(target.transform.position);

            Vector3 toTarget = target.transform.position - _owner.transform.position;

            float lookAheadTime = toTarget.magnitude / _owner.MaxSpeed + target.Velocity.magnitude;

            return Seek(target.transform.position + target.Velocity * lookAheadTime);
        }

        private Vector3 Evade(LivingEntity pursuer)
        {
            Vector3 persuerPos = pursuer.transform.position;
            Vector3 toPursuer = persuerPos - _owner.transform.position;

            float lookAheadTime = toPursuer.magnitude / _owner.MaxSpeed + pursuer.Velocity.magnitude;

            return Flee(persuerPos + pursuer.Velocity * lookAheadTime);
        }

        private Vector3 Wander()
        {
            _wanderTarget += new Vector3(Random.Range(0f, 1f) * _wanderJitter * Time.deltaTime, 0,
                Random.Range(0f, 1f) * _wanderJitter * Time.deltaTime);
            _wanderTarget = _wanderTarget.normalized * _wanderRadius;

            Vector3 newTarget = _owner.transform.position + _owner.transform.forward * _wanderDistance + _wanderTarget;

            return newTarget - _owner.transform.position;
        }

        private Vector3 ObstacleAvoidance(Vector3 targetPos)
        {
            float detectionLength = _owner.Velocity.magnitude / _owner.MaxSpeed * 4f;

            GameObject closestHit = null;
            Vector3 hitPoint = Vector3.zero;
            Vector3 target = IsOn(SteeringForces.FollowPath) ? currentWaypoint : targetPos;
            float closestDistanceSqr = (target - _owner.transform.position).sqrMagnitude;
            RaycastHit[] hits = Physics.SphereCastAll(_owner.transform.position, _owner.transform.localScale.x,
                _owner.transform.forward, detectionLength, _owner.CollisionDetectionMask);

            if (hits.Length == 0) return Vector3.zero;

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == _owner.gameObject)
                    continue;

                float sqrDistance = Vector3.SqrMagnitude(hit.point - _owner.transform.position);
                if (sqrDistance < closestDistanceSqr)
                {
                    closestHit = hit.collider.gameObject;
                    closestDistanceSqr = sqrDistance;
                    hitPoint = hit.point;
                }
            }

            if (closestHit == null) return Vector3.zero;

            // Debug Lines
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != closestHit)
                {
                    Debug.DrawLine(_owner.transform.position, hit.collider.gameObject.transform.position, Color.gray,
                        Time.deltaTime);
                }
                else
                {
                    Debug.DrawLine(_owner.transform.position, hit.collider.gameObject.transform.position, Color.red,
                        Time.deltaTime);
                }
            }

            Vector3 localPos = _owner.transform.GetChild(0).InverseTransformPoint(hitPoint);
            float proximityMultiplier = 5f + (detectionLength - localPos.z) / detectionLength;

            float perpendicularForce = Mathf.Clamp((_owner.transform.localScale.x / 2f - localPos.x), 0.1f, 1) *
                                       proximityMultiplier;

            float brakeWeight = 0.2f;

            float brakeForce = (detectionLength - localPos.z) * brakeWeight;

            Vector3 force = new Vector3(perpendicularForce, 0, -brakeForce);
            force = _owner.transform.GetChild(0).TransformVector(force);

            return force;
        }

        private Vector3 Interpose(LivingEntity entityA, LivingEntity entityB)
        {
            Vector3 midPoint = (entityA.transform.position + entityB.transform.position) / 2f;

            float timeToReachMidPoint = Vector3.Distance(midPoint, _owner.transform.position) / _owner.MaxSpeed;

            Vector3 futureA = entityA.transform.position + entityA.Velocity * timeToReachMidPoint;
            Vector3 futureB = entityB.transform.position + entityB.Velocity * timeToReachMidPoint;

            midPoint = (futureA + futureB) / 2f;

            return Arrive(midPoint, 1f);
        }

        private Vector3 OffsetPursuit(LivingEntity leader, Vector3 localOffset)
        {
            Transform lt = leader.transform;
            Vector3 leaderPos = lt.position;

            Vector3 worldOffsetPosition = leaderPos + lt.forward * localOffset.z + lt.right * localOffset.x +
                                          lt.up * localOffset.y;

            Vector3 toOffset = worldOffsetPosition - _owner.transform.position;

            float lookAheadTime = toOffset.magnitude / (_owner.MaxSpeed + leader.Velocity.magnitude);

            return Arrive(worldOffsetPosition + leader.Velocity * lookAheadTime, 1f);
        }

        private Vector3 WallAvoidance()
        {
            Transform ot = _owner.transform;
            Ray[] feelers = new Ray[]
            {
                new Ray(ot.position, ot.forward),
                new Ray(ot.position, ot.forward + ot.right),
                new Ray(ot.position, ot.forward - ot.right)
            };

            float closestDistanceSqr = float.MaxValue;
            int indexClosestFeeler = -1;
            RaycastHit closestHit = new RaycastHit();

            for (int i = 0; i < feelers.Length; i++)
            {
                Ray ray = feelers[i];
                if (Physics.Raycast(ray, out RaycastHit hit, 2f, LayerMask.GetMask("Obstacle")))
                {
                    float sqrDistance = Vector3.SqrMagnitude(hit.point - ot.position);

                    if (sqrDistance < closestDistanceSqr)
                    {
                        closestDistanceSqr = sqrDistance;
                        indexClosestFeeler = i;
                        closestHit = hit;
                    }

                    Debug.DrawLine(ray.origin, ray.origin + ray.direction.normalized * 2f, Color.red, Time.deltaTime);
                }
                else
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction.normalized * 2f, Color.green, Time.deltaTime);
            }

            if (indexClosestFeeler == -1) return Vector3.zero;

            Vector3 feeler = (feelers[indexClosestFeeler].origin + feelers[indexClosestFeeler].direction.normalized) *
                             2f;
            Vector3 toPoint = closestHit.point - _owner.transform.position;
            Vector3 overshoot = feeler - toPoint;

            return closestHit.normal * (overshoot.magnitude * 0.1f);
        }

        #region SteeringForces

        #region SetOn

        public void SeekOn()
        {
            SteeringForces |= SteeringForces.Seek;
        }

        public void FleeOn()
        {
            SteeringForces |= SteeringForces.Flee;
        }

        public void ArriveOn()
        {
            SteeringForces |= SteeringForces.Arrive;
        }

        public void PursuitOn()
        {
            SteeringForces |= SteeringForces.Pursuit;
        }

        public void EvadeOn()
        {
            SteeringForces |= SteeringForces.Evade;
        }

        public void WanderOn()
        {
            SteeringForces |= SteeringForces.Wander;
        }

        public void FollowPathOn()
        {
            SteeringForces |= SteeringForces.FollowPath;
        }

        public void ObstacleAvoidanceOn()
        {
            SteeringForces |= SteeringForces.ObstacleAvoidance;
        }

        public void WallAvoidanceOn()
        {
            SteeringForces |= SteeringForces.WallAvoidance;
        }

        #endregion

        #region Setoff

        public void SeekOff()
        {
            if (IsOn(SteeringForces.Seek))
                SteeringForces ^= SteeringForces.Seek;
        }

        public void EvadeOff()
        {
            if (IsOn(SteeringForces.Evade))
                SteeringForces ^= SteeringForces.Evade;
        }

        public void FleeOff()
        {
            if (IsOn(SteeringForces.Flee))
                SteeringForces ^= SteeringForces.Flee;
        }

        public void WanderOff()
        {
            if (IsOn(SteeringForces.Wander))
                SteeringForces ^= SteeringForces.Wander;
        }

        public void ArriveOff()
        {
            if (IsOn(SteeringForces.Arrive))
                SteeringForces ^= SteeringForces.Arrive;
        }

        public void PursuitOff()
        {
            if (IsOn(SteeringForces.Pursuit))
                SteeringForces ^= SteeringForces.Pursuit;
        }

        public void ObstacleAvoidanceOff()
        {
            if (IsOn(SteeringForces.ObstacleAvoidance))
                SteeringForces ^= SteeringForces.ObstacleAvoidance;
        }

        public void FollowPathOff()
        {
            if (IsOn(SteeringForces.FollowPath))
                SteeringForces ^= SteeringForces.FollowPath;
        }

        public void WallAvoidanceOff()
        {
            if (IsOn(SteeringForces.WallAvoidance))
                SteeringForces ^= SteeringForces.WallAvoidance;
        }

        #endregion

        #region IsOn

        private bool IsOn(SteeringForces force)
        {
            return (SteeringForces & force) == force;
        }

        #endregion

        #endregion
    }
}
using ActiveStates;
using ActiveStates.AI;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpellCasting.AI
{
    public class AIBrain : MonoBehaviour
    {
        [SerializeField]
        private AIGesture[] gestureBehaviors;

        [SerializeField]
        public AIInputController AIInputController;

        [SerializeField]
        private Transform defaultAimPoint;

        [SerializeField]
        private TeamTargetType teamTargetType = TeamTargetType.OTHER;

        [SerializeField]
        private TeamComponent teamComponent;

        [SerializeField]
        private float searchDistance;
        [SerializeField]
        private float searchInterval = 1;

        [SerializeField]
        private ActiveStateMachine aiStateMachine;

        public AIGestureBehavior CurrentGesture { get; set; }

        private CharacterBody target;

        public Vector3 CurrentTargetPosition => target != null ? target.transform.position : defaultAimPoint.position;

        private float _searchTim = 1;

        void FixedUpdate()
        {
            Search();

            bool hasTarget = target != null && !target.CommonComponents.HealthComponent.Ded;
            if (hasTarget && aiStateMachine.CurrentState is not AITargetState)
            {
                TryRollGesture();

                if (CurrentGesture != null)
                {
                    aiStateMachine.setState(new ChaseTocombat { Brain = this, TargetBody = target });
                }
            }

            if((!hasTarget || CurrentGesture == null) && aiStateMachine.CurrentState is AITargetState)
            {
                aiStateMachine.setStateToDefault();
            }
        }

        private void TryRollGesture()
        {
            if(CurrentGesture != null)
                return;
            CurrentGesture = gestureBehaviors[UnityEngine.Random.Range(0, gestureBehaviors.Length)].GetBehavior() as AIGestureBehavior;
        }

        private void Search()
        {
            _searchTim -= Time.fixedDeltaTime;
            if (_searchTim <= 0)
            {
                _searchTim = searchInterval;
                target = CharacterBodyTracker.FindBodyByTeam(gameObject, teamComponent.TeamIndex, teamTargetType, searchDistance * searchDistance);
            }
        }
    }
}

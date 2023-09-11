using System;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.DI
{
    public class DependencyContainer : MonoBehaviour
    {
        [SerializeField] private Component[] dependencies;

        [SerializeField] private MonoBehaviour[] monoBehavioursRequesters;
        
        private void Awake()
        {
            foreach (MonoBehaviour monoBehaviour in monoBehavioursRequesters)
            {
                IRequester requester = (IRequester)monoBehaviour;
                requester.Init(this);
            }
        }
        
        public T GetComponentDependency<T>() where T : Component
        {
            foreach (Component dependency in dependencies)
            {
                if (dependency is T behaviour)
                {
                    return behaviour;
                }
            }

            throw new Exception(
                $"A Monobehaviour of type {typeof(T)} is being requested but the container does not contain such instance");
        }
        
        public void SetRequesters(MonoBehaviour[] requesters)
        {
            monoBehavioursRequesters = requesters;
        }
    }
}
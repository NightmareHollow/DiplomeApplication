using System.Collections;
using System.Collections.Generic;
using Game.Examinations.Infrastructure.Events;
using Game.Examinations.Temperature.TemperatureStorage;
using GameCore.CustomExtensions.Utilities;
using GameCore.RuntimeSystems.PubSubSystem;
using GameCore.Services.ServiceStructure;
using UnityEngine;

namespace Game.Examinations.Temperature.TemperatureControl
{
    public class TemperatureControlService : InstantMonoBehaviourService
    {
        [Header("References")]

        [SerializeField] private TemperatureCollectionConfigSo _temperatureCollectionConfig;

        private List<int> temperatureValues;
        private float finalTemperatureValue;

        private Coroutine examinationsRoutine;

        private bool activated;
        private bool initialized;

        private const string SoReadStreamName = "COM9";

        public float FinalTemperatureValue => finalTemperatureValue;

        public override void InitializeService()
        {
            if (initialized)
                return;
            
            InnerInitialize();
            SubscribeListeners();

            initialized = true;
        }

        private void SubscribeListeners()
        {
            PubSubService.RegisterListener<ExaminationsStartedEvent>(OnExaminationsStarted);
            PubSubService.RegisterListener<ExaminationsFinishedEvent>(OnExaminationsFinished);
        }

        private void OnExaminationsStarted()
        {
            if (activated)
                return;
            
            activated = true;
            
            ToDefaultState();
            examinationsRoutine = StartCoroutine(ExaminationsRoutine());
        }

        private void OnExaminationsFinished()
        {
            if (!activated)
                return;

            activated = false;
            ToDefaultState();
        }

        private IEnumerator ExaminationsRoutine()
        {
            float timer = _temperatureCollectionConfig.CheckTime;
            
            while (activated && timer > 0f)
            {
                AddExamination();

                yield return new WaitForSeconds(_temperatureCollectionConfig.CheckDelay);

                timer -= _temperatureCollectionConfig.CheckDelay;
            }
        }

        private void AddExamination()
        {
            
        }

        private void InnerInitialize()
        {
            int examinationsMaxCount = Mathf.RoundToInt(_temperatureCollectionConfig.CheckTime / 
                                                        _temperatureCollectionConfig.CheckDelay) + 1;

            temperatureValues = new List<int>(examinationsMaxCount);
            
            CreateRandomTemperature();
        }

        private void CreateRandomTemperature()
        {
            finalTemperatureValue = _temperatureCollectionConfig.GetRandomTemperature();
        }

        private void ToDefaultState()
        {
            this.DeactivateCoroutine(ref examinationsRoutine);
            
            temperatureValues.Clear();
        }
        
        private void OnDestroy()
        {
            ToDefaultState();
            
            PubSubService.UnregisterListener<ExaminationsStartedEvent>(OnExaminationsStarted);
            PubSubService.UnregisterListener<ExaminationsFinishedEvent>(OnExaminationsFinished);
        }
    }
}
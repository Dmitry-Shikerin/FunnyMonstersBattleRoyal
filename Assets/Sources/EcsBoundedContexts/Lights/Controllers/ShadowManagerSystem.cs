using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class ShadowManagerSystem : IProtoRunSystem
    {
        //private ShadowManagerConfig _config;
        private int _currentActiveShadows = 0;
        
        public void Run()
        {
            
        }
        
        private void Update()
        {
            // if (LightManager.m_Instance == null)
            // {
            //     return;
            // }

            // SortLights();
            // ActivateShadows();
        }
        
        public List<ComparableLight> m_RegisteredLights = new List<ComparableLight>();
        public List<ComparableLight> m_LightsPriorityList = new List<ComparableLight>();

        // private void RegisterLight(GameLight gamelight)
        // {
        //     // Ignore already registered lights
        //     if (m_RegisteredLights.Find(x => x.m_GameLight == gamelight) != null)
        //         return;
        //
        //     // Ignore lights not requesting shadows
        //     if (gamelight.m_RequestShadow != true)
        //         return;
        //
        //     ComparableLight lightItem = new ComparableLight();
        //     lightItem.m_GameLight = gamelight;
        //
        //     // Create the shadow controller component
        //     lightItem.m_ShadowController = gamelight.GetComponent<ShadowController>();
        //     
        //     if (lightItem.m_ShadowController == null)
        //     {
        //         lightItem.m_ShadowController = gamelight.gameObject.AddComponent<ShadowController>();
        //
        //         // Default value
        //         if (m_DefaultImportanceMode == DefaultImportanceMode.DEFAULT_VALUE)
        //         {
        //             lightItem.m_ShadowController.m_Importance = m_DefaultImportance;
        //         }
        //         else
        //         {
        //             if (gamelight.m_Light.type == LightType.Directional)
        //             {
        //                 lightItem.m_ShadowController.m_Importance = ShadowController.ImportanceMode.UNRESTRICTED;
        //             }
        //             else if (gamelight.m_Intensity >= m_HighMinLightIntensity && gamelight.m_Range >= m_HighMinLightRange)
        //             {
        //                 lightItem.m_ShadowController.m_Importance = ShadowController.ImportanceMode.HIGH;
        //             }
        //             else if (gamelight.m_Intensity >= m_MediumMinLightIntensity && gamelight.m_Range >= m_MediumMinLightRange)
        //             {
        //                 lightItem.m_ShadowController.m_Importance = ShadowController.ImportanceMode.MEDIUM;
        //             }
        //             else
        //             {
        //                 lightItem.m_ShadowController.m_Importance = ShadowController.ImportanceMode.LOW;
        //             }
        //         }
        //     }
        //
        //     m_RegisteredLights.Add(lightItem);
        // }

        // private void UnregisterLight(GameLight gamelight)
        // {
        //     if (gamelight != null)
        //     {
        //         m_RegisteredLights.RemoveAll(x => x.m_GameLight == gamelight);
        //     }
        // }
        //
        // private void SortLights()
        // {
        //     // Construct the lights priority list
        //     m_LightsPriorityList.Clear();
        //     foreach (ComparableLight item in m_RegisteredLights)
        //     {
        //
        //         // Ignore disabled game objects
        //         if (item.m_GameLight.gameObject.activeInHierarchy == false)
        //             continue;
        //
        //         // Ignore disabled lights
        //         if (item.m_GameLight.m_Light.enabled == false)
        //             continue;
        //
        //         // Ignore lights set as ignored
        //         if (item.m_ShadowController.m_Importance == ShadowController.ImportanceMode.IGNORE)
        //             continue;
        //
        //         // Update distance & settings
        //         //TODO раскоментировать
        //         //item.UpdateDistance(transform.position);
        //
        //         m_LightsPriorityList.Add(item);
        //     }
        //
        //     // Sort the priority list
        //     m_LightsPriorityList.Sort();
        // }
        //
        // private void ActivateShadows()
        // {
        //     // Enable the n first lights shadows
        //     // and diable the others
        //     _currentActiveShadows = 0;
        //     
        //     foreach (ComparableLight item in m_LightsPriorityList)
        //     {
        //
        //         if (item.m_ShadowController.m_Importance != ShadowController.ImportanceMode.UNRESTRICTED)
        //         {
        //             if (item.m_GameLight.m_Light.enabled == true && item.m_GameLight.gameObject.activeInHierarchy && _currentActiveShadows < _config.m_MaxActiveShadows && item.m_IsInRange)
        //             {
        //                 // Enable shadows
        //                 if (Time.timeSinceLevelLoad < 0.5f)
        //                 {
        //                     item.m_ShadowController.ForceEnabled(true);
        //                 }
        //                 else
        //                 {
        //                     item.m_ShadowController.SetEnabled(true);
        //                 }
        //
        //                 // Reduce Shadow Resolution
        //                 if (_config.m_ReduceInactiveLightIntensity && _currentActiveShadows >= _config.m_MaxActiveShadows - _config.m_ReduceResolutionOfLast)
        //                 {
        //                     item.m_ShadowController.SetResolutionQualityReduced(true);
        //                 }
        //                 else
        //                 {
        //                     item.m_ShadowController.SetResolutionQualityReduced(false);
        //                 }
        //             }
        //             else
        //             {
        //                 // Disable shadows
        //                 if (Time.timeSinceLevelLoad < 0.5f)
        //                 {
        //                     item.m_ShadowController.ForceEnabled(false);
        //                 }
        //                 else
        //                 {
        //                     item.m_ShadowController.SetEnabled(false);
        //                 }
        //             }
        //         }
        //
        //         // only count active ones (to let them the time to be enabled/disabled)
        //         if (item.m_GameLight.m_Light.enabled == true && item.m_GameLight.gameObject.activeInHierarchy && item.m_GameLight.m_Light.shadows != LightShadows.None)
        //         {
        //             _currentActiveShadows++;
        //         }
        //     }
        // }
        //
        // private void RestoreShadows()
        // {
        //     foreach (ComparableLight item in m_RegisteredLights)
        //     {
        //         item.m_ShadowController.ForceEnabled(true);
        //     }
        // }
    }
}
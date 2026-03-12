using System;
using Leopotam.EcsProto;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Domain
{
    [Serializable]
    public class ComparableLight : IComparable<ComparableLight>
    {
        public float m_DistanceToPlayer;
        public bool m_IsInRange;

        // public ShadowController m_ShadowController;
        // public GameLight m_GameLight;

        public void UpdateDistance(Vector3 pos)
        {
            //m_DistanceToPlayer = (m_GameLight.transform.position - pos).magnitude;
            //m_IsInRange = InRange();
        }

        private bool InRange(ProtoEntity entity)
        {
            //var m_ShadowController = entity.GetShadowController();
            
            // if (m_ShadowController.m_Importance == ShadowController.ImportanceMode.UNRESTRICTED)
            //     return true;
            
            // if (m_ShadowController.GetShadowRange() > m_DistanceToPlayer)
            //     return true;

            return false;
        }

        /// <summary>
        /// Compares to.
        /// -1 is better than 1
        /// </summary>
        public int CompareTo(ComparableLight lightItem)
        {
            // Priority to In Range lights
            if (m_IsInRange && !lightItem.m_IsInRange)
                return -1;

            if (m_IsInRange == false && lightItem.m_IsInRange)
                return 1;

            // Priority to light with a higher importance
            // if (m_ShadowController.m_Importance > lightItem.m_ShadowController.m_Importance)
            //     return -1;
            //
            // if (m_ShadowController.m_Importance < lightItem.m_ShadowController.m_Importance)
            //     return 1;
            //
            // // Priority to light with a higher priority
            // if (m_ShadowController.m_Priority > lightItem.m_ShadowController.m_Priority)
            //     return -1;
            //
            // if (m_ShadowController.m_Priority < lightItem.m_ShadowController.m_Priority)
            //     return 1;

            // Priority to the nearest light
            return m_DistanceToPlayer.CompareTo(lightItem.m_DistanceToPlayer);
        }
    }
}
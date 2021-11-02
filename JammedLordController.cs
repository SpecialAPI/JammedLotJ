using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace JammedLotJ
{
    public class JammedLordController : MonoBehaviour
    {
        public void Initialize(SuperReaperController controller)
        {
            m_controller = controller;
            m_cachedAttack = controller.BulletScript.scriptTypeName;
            tk2dBaseSprite tk2dBaseSprite = base.GetComponent<tk2dBaseSprite>();
            tk2dBaseSprite.usesOverrideMaterial = true;
            Material material = tk2dBaseSprite.renderer.material;
            material.shader = ShaderCache.Acquire("Brave/LitCutoutUberPhantom");
            material.SetFloat("_PhantomGradientScale", 0.75f);
            material.SetFloat("_PhantomContrastPower", 1.3f);
            controller.BulletScript.scriptTypeName = typeof(JammedCircleBurst12).AssemblyQualifiedName;
            controller.MinSpeed *= 1.5f;
            controller.MaxSpeed *= 1.5f;
            SetParticlesPerSecond(GetParticlesPerSecond() * 2);
        }

        public void Uninitialize()
        {
            m_controller.BulletScript.scriptTypeName = m_cachedAttack;
            tk2dBaseSprite tk2dBaseSprite = base.GetComponent<tk2dBaseSprite>();
            tk2dBaseSprite.usesOverrideMaterial = false;
            m_controller.MinSpeed /= 1.5f;
            m_controller.MaxSpeed /= 1.5f;
            SetParticlesPerSecond(GetParticlesPerSecond() / 2);
            Destroy(this);
        }

        private int GetParticlesPerSecond()
        {
            return (int)info.GetValue(m_controller);
        }

        private void SetParticlesPerSecond(int value)
        {
            info.SetValue(m_controller, value);
        }

        private string m_cachedAttack;
        private SuperReaperController m_controller;
        private static FieldInfo info = typeof(SuperReaperController).GetField("c_particlesPerSecond", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}

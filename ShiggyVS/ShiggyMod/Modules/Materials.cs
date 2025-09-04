using System.Collections.Generic;
using UnityEngine;

namespace ShiggyMod.Modules
{
    internal static class Materials
    {
        private static List<Material> cachedMaterials = new List<Material>();

        internal static Shader hotpoo = RoR2.LegacyResourcesAPI.Load<Shader>("Shaders/Deferred/HGStandard");

        public static Material CreateHopooMaterial(string materialName, float emPower = 1f)
        {
            Material tempMat = cachedMaterials.Find(mat =>
            {
                materialName.Replace(" (Instance)", "");
                return mat.name.Contains(materialName);
            });
            if (tempMat)
                return tempMat;

            tempMat = ShiggyAsset.mainAssetBundle.LoadAsset<Material>(materialName);

            if (!tempMat)
            {
                Log.Error("Failed to load material: " + materialName + " - Check to see that the material in your Unity project matches this name");
                return new Material(hotpoo);
            }

            return tempMat.SetHopooMaterial(emPower);
        }

        public static Material SetHopooMaterial(this Material tempMat, float emPower = 1f)
        {
            if (cachedMaterials.Contains(tempMat))
                return tempMat;

            float? bumpScale = null;
            Color? emissionColor = null;

            //grab values before the shader changes
            if (tempMat.IsKeywordEnabled("_NORMALMAP"))
            {
                bumpScale = tempMat.GetFloat("_BumpScale");
            }
            if (tempMat.IsKeywordEnabled("_EMISSION"))
            {
                emissionColor = tempMat.GetColor("_EmissionColor");
            }

            //set shader
            tempMat.shader = hotpoo;

            //apply values after shader is set
            tempMat.SetColor("_Color", tempMat.GetColor("_Color"));
            tempMat.SetTexture("_MainTex", tempMat.GetTexture("_MainTex"));
            tempMat.SetTexture("_EmTex", tempMat.GetTexture("_EmissionMap"));

            if (bumpScale != null)
            {
                tempMat.SetFloat("_NormalStrength", (float)bumpScale);
            }
            if (emissionColor != null)
            {
                tempMat.SetColor("_EmColor", (Color)emissionColor);
                tempMat.SetFloat("_EmPower", emPower);
            }

            //set this keyword in unity if you want your model to show backfaces
            //in unity, right click the inspector tab and choose Debug
            if (tempMat.IsKeywordEnabled("NOCULL"))
            {
                tempMat.SetInt("_Cull", 0);
            }
            //set this keyword in unity if you've set up your model for limb removal item displays (eg. goat hoof) by setting your model's vertex colors
            if (tempMat.IsKeywordEnabled("LIMBREMOVAL"))
            {
                tempMat.SetInt("_LimbRemovalOn", 1);
            }

            cachedMaterials.Add(tempMat);
            return tempMat;
        }

        /// <summary>
        /// Makes this a unique material if we already have this material cached (i.e. you want an altered version). New material will not be cached
        /// <para>If it was not cached in the first place, simply returns as it is already unique.</para>
        /// </summary>
        public static Material MakeUnique(this Material material)
        {

            if (cachedMaterials.Contains(material))
            {
                return new Material(material);
            }
            return material;
        }

        public static Material SetColor(this Material material, Color color)
        {
            material.SetColor("_Color", color);
            return material;
        }

        public static Material SetNormal(this Material material, float normalStrength = 1)
        {
            material.SetFloat("_NormalStrength", normalStrength);
            return material;
        }

        public static Material SetEmission(this Material material) => SetEmission(material, 1);
        public static Material SetEmission(this Material material, float emission) => SetEmission(material, emission, Color.white);
        public static Material SetEmission(this Material material, float emission, Color emissionColor)
        {
            material.SetFloat("_EmPower", emission);
            material.SetColor("_EmColor", emissionColor);
            return material;
        }
        public static Material SetCull(this Material material, bool cull = false, int cullValue = 0)
        {
            if (cull)
            {
                material.SetInt("_Cull", cullValue);
            }
            else
            {
                material.SetInt("_Cull", 0);

            }
            return material;
        }

        public static Material SetCutout(this Material material, bool on = true, float cutoff = 0.4f)
        {
            if (!material) return material;

            // Core alpha-test settings for HGStandard
            if (on)
            {
                // Keywords
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON"); // we want clip, not blend

                // Clip threshold
                material.SetFloat("_Cutoff", cutoff);

                // Opaque blend + depth write (clip happens before blending)
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);

                // Render queue for alpha test
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest; // 2450

                // Double-sided for hair cards
                //material.SetInt("_Cull", doubleSided ? 0 : 2);
            }
            else
            {
                // Back to opaque
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.SetInt("_ZWrite", 1);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry; // 2000
                material.SetInt("_Cull", 2);
            }

            return material;
        }
    }
}
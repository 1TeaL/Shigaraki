using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static ShiggyMod.Modules.Survivors.ShiggyMasterController;


namespace ShiggyMod.Modules.Quirks
{
    public static class QuirkIconThemes
    {
        // Brighter magenta/pink for actives
        // Hex approx: #EC37B9
        public static readonly Color ActiveBG = new Color(0.925f, 0.215f, 0.725f, 1f);

        // Near-black with a hint of blue for passives
        // Hex approx: #0B0E13
        public static readonly Color PassiveBG = new Color(0.043f, 0.055f, 0.075f, 1f);


        public static Color ForActive(bool isActive) => isActive ? ActiveBG : PassiveBG;

    }
    public static class QuirkIconProvider
    {
        private static readonly Dictionary<string, Sprite> bodyIconCache = new Dictionary<string, Sprite>();
        private static readonly Dictionary<Texture, Sprite> textureToSprite = new Dictionary<Texture, Sprite>();

        /// <summary>
        /// Returns a sprite built from a body's portrait icon.
        /// If background is provided, composites portrait over a colored square of size bgSize.
        /// </summary>
        public static Sprite GetBodyIconSprite(string bodyAddressOrName, Color? background = null, int bgSize = 128, float portraitScale = 0.92f)
        {
            if (string.IsNullOrEmpty(bodyAddressOrName)) return null;
            if (bodyIconCache.TryGetValue(bodyAddressOrName, out var cached)) return cached;

            GameObject bodyPrefab = TryLoadBodyPrefab(bodyAddressOrName);
            if (!bodyPrefab)
            {
                var idx = BodyCatalog.FindBodyIndex(bodyAddressOrName);
                if (idx != BodyIndex.None) bodyPrefab = BodyCatalog.GetBodyPrefab(idx);
            }
            if (!bodyPrefab) return null;

            var cb = bodyPrefab.GetComponent<CharacterBody>();
            if (!cb || !cb.portraitIcon) return null;

            Sprite icon;
            if (background.HasValue)
            {
                var composed = CompositePortraitOverBackground(cb.portraitIcon, background.Value, bgSize, portraitScale);
                icon = Texture2DToSprite(composed);
            }
            else
            {
                icon = TextureToSprite(cb.portraitIcon);
            }

            if (icon) bodyIconCache[bodyAddressOrName] = icon;
            return icon;
        }

        private static GameObject TryLoadBodyPrefab(string address)
        {
            try { return Addressables.LoadAssetAsync<GameObject>(address).WaitForCompletion(); }
            catch { return null; }
        }

        private static Sprite TextureToSprite(Texture tex)
        {
            if (!tex) return null;
            if (textureToSprite.TryGetValue(tex, out var sp)) return sp;

            var t2d = TextureToTexture2D(tex);
            if (!t2d) return null;

            sp = Texture2DToSprite(t2d);
            textureToSprite[tex] = sp;
            return sp;
        }

        private static Texture2D TextureToTexture2D(Texture tex)
        {
            if (tex is Texture2D ready) return ready;

            var rt = RenderTexture.GetTemporary(tex.width, tex.height, 0, RenderTextureFormat.ARGB32);
            var prev = RenderTexture.active;
            Graphics.Blit(tex, rt);
            RenderTexture.active = rt;

            var copy = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false, false);
            copy.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            copy.Apply();

            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(rt);
            return copy;
        }

        private static Sprite Texture2DToSprite(Texture2D t2d)
        {
            return Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
        }

        private static Texture2D CompositePortraitOverBackground(Texture portrait, Color bgColor, int size, float portraitScale)
        {
            var bg = new Texture2D(size, size, TextureFormat.RGBA32, false, false);
            var fill = new Color32((byte)(bgColor.r * 255f), (byte)(bgColor.g * 255f), (byte)(bgColor.b * 255f), (byte)(bgColor.a * 255f));
            var pixels = new Color32[size * size];
            for (int i = 0; i < pixels.Length; i++) pixels[i] = fill;
            bg.SetPixels32(pixels);
            bg.Apply();

            var src = TextureToTexture2D(portrait);
            if (!src) return bg;

            int inner = Mathf.RoundToInt(size * Mathf.Clamp01(portraitScale));
            var resized = ResizeTexture(src, inner, inner);

            int x = (size - inner) / 2;
            int y = (size - inner) / 2;
            PasteTexture(bg, resized, x, y);

            bg.Apply();
            return bg;
        }

        private static Texture2D ResizeTexture(Texture2D src, int w, int h)
        {
            var rt = RenderTexture.GetTemporary(w, h, 0, RenderTextureFormat.ARGB32);
            var prev = RenderTexture.active;
            Graphics.Blit(src, rt);
            RenderTexture.active = rt;

            var dst = new Texture2D(w, h, TextureFormat.RGBA32, false, false);
            dst.ReadPixels(new Rect(0, 0, w, h), 0, 0);
            dst.Apply();

            RenderTexture.active = prev;
            RenderTexture.ReleaseTemporary(rt);
            return dst;
        }

        private static void PasteTexture(Texture2D dst, Texture2D src, int x, int y)
        {
            var sp = src.GetPixels32();
            int w = src.width, h = src.height;
            for (int j = 0; j < h; j++)
                for (int i = 0; i < w; i++)
                {
                    int dx = x + i, dy = y + j;
                    if ((uint)dx >= dst.width || (uint)dy >= dst.height) continue;

                    var c = sp[j * w + i];
                    if (c.a == 0) continue;
                    dst.SetPixel(dx, dy, c);
                }
        }
    }

    public static class QuirkIconBank
    {
        public enum Theme { Active, Passive }

        private struct Entry
        {
            public string Body;
            public Theme Theme;
            public int Size;
            public float Scale;
        }

        // Manual registrations and built cache
        private static readonly Dictionary<QuirkId, Entry> registrations = new Dictionary<QuirkId, Entry>();
        private static readonly Dictionary<QuirkId, Sprite> cache = new Dictionary<QuirkId, Sprite>();

        public static void Register(QuirkId id, string bodyAddressOrName, Theme theme, int size = 128, float scale = 0.92f)
        {
            registrations[id] = new Entry { Body = bodyAddressOrName, Theme = theme, Size = size, Scale = scale };
        }

        /// <summary> Use this directly in SkillDef creation lines. </summary>
        public static Sprite Get(QuirkId id)
        {
            if (cache.TryGetValue(id, out var s)) return s;
            if (registrations.TryGetValue(id, out var r))
            {
                var bg = QuirkIconThemes.ForActive(r.Theme == Theme.Active);
                var sp = QuirkIconProvider.GetBodyIconSprite(r.Body, bg, r.Size, r.Scale);
                if (sp) cache[id] = sp;
                return sp;
            }
            return null;
        }

        /// Optional: build all registered (call in LateSetup for safety)
        public static void BuildAll()
        {
            foreach (var kv in registrations)
                if (!cache.ContainsKey(kv.Key))
                    Get(kv.Key);
        }

        /// Optional: fill null icons for registered quirks (late safety pass)
        public static void ApplyToRegisteredSkillDefsIfNull()
        {
            foreach (var kv in registrations)
            {
                QuirkRecord rec;
                if (!QuirkRegistry.TryGet(kv.Key, out rec) || rec.Skill == null) continue;
                if (rec.Skill.icon != null) continue;

                var s = Get(kv.Key);
                if (s) rec.Skill.icon = s;
            }
        }

        public static void CreateSpriteIcons()
        {
            // ===== Level 1 — PASSIVES (non-elite) =====
            QuirkIconBank.Register(QuirkId.AlphaConstruct_BarrierPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MinorConstructBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Beetle_StrengthPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Beetle.BeetleBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Pest_JumpPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_FlyingVermin.FlyingVerminBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Vermin_SpeedPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Vermin.VerminBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Gup_SpikyBodyPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Gup.GupBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.HermitCrab_MortarPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_HermitCrab.HermitCrabBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Larva_AcidJumpPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_AcidLarva.AcidLarvaBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.LesserWisp_HastePassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Wisp.WispBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.LunarExploder_LunarBarrierPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarExploder.LunarExploderBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.MiniMushrum_HealingAuraPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MiniMushroom.MiniMushroomBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.RoboBallMini_SolusBoostPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallMiniBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.VoidBarnacle_VoidMortarPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidBarnacle.VoidBarnacleBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.VoidJailer_GravityPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidJailer.VoidJailerBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.ImpBoss_BleedPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ImpBoss.ImpBossBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.StoneTitan_StoneSkinPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Titan.TitanBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.MagmaWorm_BlazingAuraPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_MagmaWorm.MagmaWormBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.OverloadingWorm_LightningAuraPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ElectricWorm.ElectricWormBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Vagrant_OrbPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vagrant.VagrantBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Child_EmergencyTeleportPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Child.ChildBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Acrid_PoisonPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Croco.CrocoBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Commando_DoubleTapPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Commando.CommandoBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Captain_MicrobotsPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Captain.CaptainBody_prefab, QuirkIconBank.Theme.Passive);
            QuirkIconBank.Register(QuirkId.Loader_ScrapBarrierPassive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Loader.LoaderBody_prefab, QuirkIconBank.Theme.Passive);

            // ===== Level 1 — ACTIVES (exclude Shiggy_* and Deku_OFAActive) =====
            QuirkIconBank.Register(QuirkId.Vulture_WindBlastActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Vulture.VultureBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.BeetleGuard_SlamActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleGuard.BeetleGuardBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Bison_ChargeActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bison.BisonBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Bell_SpikedBallActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bell.BellBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.ClayApothecary_MortarActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_ClayGrenadier.ClayGrenadierBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.ClayTemplar_MinigunActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBruiser.ClayBruiserBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.ElderLemurian_FireBlastActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LemurianBruiser.LemurianBruiserBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.GreaterWisp_SpiritBoostActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_GreaterWisp.GreaterWispBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Imp_BlinkActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Imp.ImpBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Jellyfish_HealActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Jellyfish.JellyfishBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Lemurian_FireballActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Lemurian.LemurianBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.LunarGolem_SlideResetActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarGolem.LunarGolemBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.LunarWisp_MinigunActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_LunarWisp.LunarWispBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Parent_TeleportActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Parent.ParentBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.StoneGolem_LaserActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Golem.GolemBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.VoidReaver_PortalActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Nullifier.NullifierBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.BeetleQueen_SummonActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_BeetleQueen.BeetleQueen2Body_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Grandparent_SunActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Grandparent.GrandParentBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Grovetender_ChainActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Gravekeeper.GravekeeperBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.ClayDunestrider_TarBoostActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_ClayBoss.ClayBossBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.SolusControlUnit_KnockupActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_RoboBallBoss.RoboBallBossBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.XIConstruct_BeamActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_MajorAndMinorConstruct.MajorConstructBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.VoidDevastator_MissilesActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidMegaCrab.VoidMegaCrabAllyBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Scavenger_ThqwibActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Scav.ScavBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Artificer_FlamethrowerActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Artificer_IceWallActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Artificer_LightningOrbActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Mage.MageBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Bandit_LightsOutActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Bandit2.Bandit2Body_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Engineer_TurretActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Engi.EngiBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Huntress_FlurryActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Huntress.HuntressBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Merc_DashActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Merc.MercBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.MULT_PowerStanceActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.MULT_PowerStanceCancelActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Toolbot.ToolbotBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Railgunner_CryoActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_Railgunner.RailgunnerBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.REX_MortarActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_Base_Treebot.TreebotBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.VoidFiend_CleanseActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC1_VoidSurvivor.VoidSurvivorBody_prefab, QuirkIconBank.Theme.Active);
            QuirkIconBank.Register(QuirkId.Halcyonite_GreedActive, RoR2BepInExPack.GameAssetPathsBetter.RoR2_DLC2_Halcyonite.HalcyoniteBody_prefab, QuirkIconBank.Theme.Active);

        }

    }



}

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
		public static void RegisterFromRegistryData()
		{
			foreach (var e in QuirkRegistryData.All)
			{
				if (e.Category == QuirkCategory.Utility) continue;

				// pick a body source (prefer a body path if present)
				var body = (e.BodyPaths != null && e.BodyPaths.Length > 0) ? e.BodyPaths[0]
						 : (e.BodyNames != null && e.BodyNames.Length > 0) ? e.BodyNames[0]
						 : null;

				if (string.IsNullOrEmpty(body)) continue;

				var theme = (e.Category == QuirkCategory.Passive) ? QuirkIconBank.Theme.Passive : QuirkIconBank.Theme.Active;
				QuirkIconBank.Register(e.Id, body, theme);
			}
		}


	}



}

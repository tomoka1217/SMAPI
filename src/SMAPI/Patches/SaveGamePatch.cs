using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Harmony;
using StardewModdingAPI.Framework.Patching;
using StardewValley;

namespace StardewModdingAPI.Patches
{
    /// <summary>A Harmony patch for <see cref="SaveGame.Save"/> to detect when the game is saving.</summary>
    internal class SaveGamePatch : IHarmonyPatch
    {
        /*********
        ** Private methods
        *********/
        /// <summary>A callback to invoke before <see cref="SaveGame.Save"/> runs.</summary>
        private static Action OnSaving;

        /// <summary>A callback to invoke before <see cref="SaveGame.Save"/> runs.</summary>
        private static Action OnSaved;


        /*********
        ** Accessors
        *********/
        /// <summary>A unique name for this patch.</summary>
        public string Name => $"{nameof(SaveGamePatch)}";


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="onSaving">A callback to invoke before <see cref="SaveGame.Save"/> runs.</param>
        /// <param name="onSaved">A callback to invoke after <see cref="SaveGame.Save"/> runs.</param>
        public SaveGamePatch(Action onSaving, Action onSaved)
        {
            SaveGamePatch.OnSaving = onSaving;
            SaveGamePatch.OnSaved = onSaved;
        }


        /// <summary>Apply the Harmony patch.</summary>
        /// <param name="harmony">The Harmony instance.</param>
        public void Apply(HarmonyInstance harmony)
        {
            MethodInfo method = AccessTools.Method(typeof(SaveGame), nameof(SaveGame.Save));
            MethodInfo prefix = AccessTools.Method(this.GetType(), nameof(SaveGamePatch.Prefix));
            MethodInfo postfix = AccessTools.Method(this.GetType(), nameof(SaveGamePatch.Postfix));

            harmony.Patch(method, prefix: new HarmonyMethod(prefix));
            harmony.Patch(method, postfix: new HarmonyMethod(postfix));
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method to call before <see cref="SaveGame.Save"/>.</summary>
        /// <returns>Returns whether to execute the original method.</returns>
        /// <remarks>This method must be static for Harmony to work correctly. See the Harmony documentation before renaming arguments.</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Argument names are defined by Harmony.")]
        private static bool Prefix()
        {
            SaveGamePatch.OnSaving();
            return true;
        }

        /// <summary>The method to call after <see cref="SaveGame.Save"/>.</summary>
        /// <remarks>This method must be static for Harmony to work correctly. See the Harmony documentation before renaming arguments.</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Argument names are defined by Harmony.")]
        private static void Postfix()
        {
            SaveGamePatch.OnSaved();
        }
    }
}

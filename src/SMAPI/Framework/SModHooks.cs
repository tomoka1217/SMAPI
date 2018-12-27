using System;
using StardewValley;
using xTile.Dimensions;

namespace StardewModdingAPI.Framework
{
    /// <summary>Invokes callbacks for mod hooks provided by the game.</summary>
    internal class SModHooks : ModHooks
    {
        /*********
        ** Properties
        *********/
        /// <summary>A callback to invoke before <see cref="Game1.newDayAfterFade"/> runs.</summary>
        private readonly Action<Action> BeforeNewDayAfterFade;

        /// <summary>A callback to invoke before <see cref="GameLocation.checkAction"/> runs.</summary>
        private readonly Func<GameLocation, Location, Farmer, Func<bool>, bool> BeforeCheckAction;

        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="beforeNewDayAfterFade">A callback to invoke before <see cref="Game1.newDayAfterFade"/> runs.</param>
        /// <param name="beforeCheckAction">A callback to invoke before <see cref="GameLocation.checkAction"/> runs.</param>
        public SModHooks(Action<Action> beforeNewDayAfterFade, Func<GameLocation, Location, Farmer, Func<bool>, bool> beforeCheckAction)
        {
            this.BeforeNewDayAfterFade = beforeNewDayAfterFade;
            this.BeforeCheckAction = beforeCheckAction;
        }

        /// <summary>A hook invoked when <see cref="Game1.newDayAfterFade"/> is called.</summary>
        /// <param name="resume">Resume the vanilla logic.</param>
        public override void OnGame1_NewDayAfterFade(Action resume)
        {
            this.BeforeNewDayAfterFade(resume);
        }

        /// <summary>A hook invoked when <see cref="GameLocation.checkAction"/> is called.</summary>
        /// <param name="location">The location being checked.</param>
        /// <param name="tileLocation">The tile coordinate being checked.</param>
        /// <param name="viewport">The current viewport.</param>
        /// <param name="who">The player checking for an action.</param>
        /// <param name="resume">Resume the default logic.</param>
        public override bool OnGameLocation_CheckAction(GameLocation location, Location tileLocation, Rectangle viewport, Farmer who, Func<bool> resume)
        {
            return this.BeforeCheckAction(location, tileLocation, who, resume);
        }
    }
}

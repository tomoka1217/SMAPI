using System;
using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewModdingAPI.Events
{
    /// <summary>Event arguments for an <see cref="IPlayerEvents.CheckedForAction"/> event.</summary>
    public class CheckedForActionEventArgs : EventArgs
    {
        /*********
        ** Properties
        *********/
        /// <summary>The underlying field for <see cref="ActionPropertyValue"/>.</summary>
        private readonly Lazy<string> ActionPropertyValueImpl;

        /*********
        ** Accessors
        *********/
        /// <summary>The player who checked for an action.</summary>
        public Farmer Player { get; }

        /// <summary>The tile checked.</summary>
        public Vector2 Tile { get; }

        /// <summary>The value of the <c>Action</c> tile property, if any.</summary>
        public string ActionPropertyValue => this.ActionPropertyValueImpl.Value;

        /// <summary>The current cursor position. This may differ from <see cref="Tile"/>, due to how the game selects the target tile for actions in some cases.</summary>
        public ICursorPosition Cursor { get; }

        /// <summary>Whether the affected player is the local one.</summary>
        public bool IsLocalPlayer => this.Player.IsLocalPlayer;

        /// <summary>Whether the game performed an action in response to the check. Note that the game sometimes handles input without marking it handled (e.g. when activating a TV or fireplace).</summary>
        public bool WasHandled { get; }


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="player">The player for whom the action was checked.</param>
        /// <param name="tile">The tile checked.</param>
        /// <param name="cursorPosition">The current cursor position.</param>
        /// <param name="actionPropertyValue">The value of the <c>Action</c> tile property, if any.</param>
        /// <param name="wasHandled">Whether the game performed an action in response to the check.</param>
        internal CheckedForActionEventArgs(Farmer player, Vector2 tile, ICursorPosition cursorPosition, Lazy<string> actionPropertyValue, bool wasHandled)
        {
            this.Player = player;
            this.Tile = tile;
            this.Cursor = cursorPosition;
            this.ActionPropertyValueImpl = actionPropertyValue;
            this.WasHandled = wasHandled;
        }
    }
}

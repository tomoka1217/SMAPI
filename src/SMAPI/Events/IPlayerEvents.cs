using System;

namespace StardewModdingAPI.Events
{
    /// <summary>Events raised when the player data changes.</summary>
    public interface IPlayerEvents
    {
        /// <summary>Raised before the game checks for an action in response to a player input. That includes activating an interactive object, opening a chest, putting an item in a machine, etc. NOTE: this event is currently only raised for the current player.</summary>
        event EventHandler<CheckingForActionEventArgs> CheckingForAction;

        /// <summary>Raised after the game checks for an action in response to a player input. That includes activating an interactive object, opening a chest, putting an item in a machine, etc. NOTE: this event is currently only raised for the current player.</summary>
        event EventHandler<CheckedForActionEventArgs> CheckedForAction;

        /// <summary>Raised after items are added or removed to a player's inventory. NOTE: this event is currently only raised for the current player.</summary>
        event EventHandler<InventoryChangedEventArgs> InventoryChanged;

        /// <summary>Raised after a player skill level changes. This happens as soon as they level up, not when the game notifies the player after their character goes to bed.  NOTE: this event is currently only raised for the current player.</summary>
        event EventHandler<LevelChangedEventArgs> LevelChanged;

        /// <summary>Raised after a player warps to a new location. NOTE: this event is currently only raised for the current player.</summary>
        event EventHandler<WarpedEventArgs> Warped;
    }
}

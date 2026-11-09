// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Server.Botany
{
    /// <summary>
    /// Anything that can be used to cross-pollinate plants.
    /// </summary>
    [RegisterComponent]
    public sealed partial class BotanySwabComponent : Component
    {
        [DataField("swabDelay")]
        public float SwabDelay = 2f;

        /// <summary>
        /// SeedData from the first plant that got swabbed.
        /// </summary>
        public SeedData? SeedData;

        /// <summary>
        /// Capibara - EntityUid of the plant holder the pollen was taken from.
        /// Used by the genome breeding system for gene transfer.
        /// </summary>
        public EntityUid? SourcePlantHolder;
    }
}
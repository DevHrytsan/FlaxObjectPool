using System;
using FlaxEngine;

namespace FlaxObjectPool
{
    /// <summary>
    /// The Flax Object pool  plugin.
    /// </summary>
    /// <seealso cref="FlaxEngine.GamePlugin" />
    public class FlaxObjectPool : GamePlugin
    {
        /// <inheritdoc />
        public FlaxObjectPool()
        {
            _description = new PluginDescription
            {
                Name = "FlaxObjectPool",
                Category = "Other",
                Author = "DevHrytsan",
                AuthorUrl = null,
                HomepageUrl = null,
                RepositoryUrl = "https://github.com/DevHrytsan/FlaxObjectPool",
                Description = "Optimization technique used to improve the instantiate-destroy cycle of your project",
                Version = new Version(0, 0, 1),
                IsAlpha = false,
                IsBeta = false,
            };
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <inheritdoc />
        public override void Deinitialize()
        {
            // Use it to cleanup data

            base.Deinitialize();
        }
    }
}

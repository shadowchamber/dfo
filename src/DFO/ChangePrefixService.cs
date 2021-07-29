// <copyright file="ChangePrefixService.cs" company="Shadowchamber">
// Copyright (c) Shadowchamber. All rights reserved.
// </copyright>

namespace DFO
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Change prefix service class.
    /// </summary>
    public class ChangePrefixService : IChangePrefixService
    {
        private readonly ILogger<ChangePrefixService> logger;
        private ChangePrefixOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePrefixService"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        public ChangePrefixService(ILogger<ChangePrefixService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Executes command.
        /// </summary>
        /// <param name="options">options.</param>
        /// <param name="stoppingToken">cancellation token.</param>
        /// <returns>async task.</returns>
        public async Task ExecuteAsync(ChangePrefixOptions options, CancellationToken stoppingToken)
        {
            this.logger.LogInformation($"Changing prefix from {options.FromPrefix} to {options.ToPrefix}");

            this.options = options;

            try
            {
                await this.UpdateDescriptorAsync().ConfigureAwait(false);
                await this.UpdateObjectsAsync().ConfigureAwait(false);
                await this.UpdateSolutionAsync().ConfigureAwait(false);
                this.UpdateProjectDir();
                await this.UpdateProjectFileAsync().ConfigureAwait(false);
                this.UpdateObjDir();
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, exception.Message);
            }
        }

        /// <summary>
        /// Updates project file.
        /// </summary>
        /// <returns>async task.</returns>
        private async Task UpdateProjectFileAsync()
        {
            string projFile = Path.Combine(this.options.Path, $"Projects\\{this.options.DestinationPrefix}\\{this.options.SourcePrefix}.rnrproj");
            string projFileNew = Path.Combine(this.options.Path, $"Projects\\{this.options.DestinationPrefix}\\{this.options.DestinationPrefix}.rnrproj");

            string descrContent = await File.ReadAllTextAsync(projFile).ConfigureAwait(false);

            descrContent = descrContent.Replace(this.options.SourcePrefix, this.options.DestinationPrefix);
            descrContent = descrContent.Replace(this.options.FromPrefix, this.options.ToPrefix);

            await File.WriteAllTextAsync(projFile, descrContent).ConfigureAwait(false);

            File.Move(projFile, projFileNew);
        }

        private void UpdateProjectDir()
        {
            string projDir = Path.Combine(this.options.Path, $"Projects\\{this.options.SourcePrefix}");

            string projDirNew = Path.Combine(this.options.Path, $"Projects\\{this.options.DestinationPrefix}");

            Directory.Move(projDir, projDirNew);
        }

        private void UpdateObjDir()
        {
            string objDir = Path.Combine(this.options.Path, $"{this.options.SourcePrefix}");

            string objDirNew = Path.Combine(this.options.Path, $"{this.options.DestinationPrefix}");

            Directory.Move(objDir, objDirNew);
        }

        private async Task UpdateSolutionAsync()
        {
            string slnFile = Path.Combine(this.options.Path, $"{this.options.SourcePrefix}.sln");

            string descrContent = await File.ReadAllTextAsync(slnFile).ConfigureAwait(false);

            descrContent = descrContent.Replace(this.options.SourcePrefix, this.options.DestinationPrefix);

            await File.WriteAllTextAsync(slnFile, descrContent).ConfigureAwait(false);

            string slnFileNew = Path.Combine(this.options.Path, $"{this.options.DestinationPrefix}.sln");

            File.Move(slnFile, slnFileNew);
        }

        private async Task UpdateObjectsAsync()
        {
            string objDir = Path.Combine(this.options.Path, $"{this.options.SourcePrefix}");

            await this.DirSearchAsync(objDir).ConfigureAwait(false);
        }

        private async Task UpdateObjFileAsync(string file)
        {
            string filenameNoExt = Path.GetFileNameWithoutExtension(file);
            string ext = Path.GetExtension(file);
            string filedir = Path.GetDirectoryName(file);

            string newFilename = filenameNoExt.Replace(this.options.FromPrefix, this.options.ToPrefix).Replace(this.options.SourcePrefix, this.options.DestinationPrefix);
            string newFile = Path.Combine(filedir, newFilename + ext);

            File.Move(file, newFile);

            await this.UpdateContentAsync(newFile).ConfigureAwait(false);
        }

        private async Task UpdateContentAsync(string file)
        {
            string descrContent = await File.ReadAllTextAsync(file).ConfigureAwait(false);

            descrContent = descrContent.Replace(this.options.FromPrefix, this.options.ToPrefix);
            descrContent = descrContent.Replace(this.options.SourcePrefix, this.options.DestinationPrefix);

            await File.WriteAllTextAsync(file, descrContent).ConfigureAwait(false);
        }

        private async Task DirSearchAsync(string sDir)
        {
            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    await this.UpdateObjFileAsync(f).ConfigureAwait(false);
                }

                await this.DirSearchAsync(d).ConfigureAwait(false);
            }
        }

        private async Task UpdateDescriptorAsync()
        {
            var descriptorFile = Path.Combine(this.options.Path, $"Descriptor\\{this.options.SourcePrefix}.xml");

            if (!File.Exists(descriptorFile))
            {
                throw new Exception($"failed to load descriptor file {descriptorFile}");
            }

            string descrContent = await File.ReadAllTextAsync(descriptorFile).ConfigureAwait(false);

            descrContent = descrContent.Replace(this.options.SourcePrefix, this.options.DestinationPrefix);

            await File.WriteAllTextAsync(descriptorFile, descrContent).ConfigureAwait(false);

            var descriptorFileNew = Path.Combine(this.options.Path, $"Descriptor\\{this.options.DestinationPrefix}.xml");

            File.Move(descriptorFile, descriptorFileNew);
        }
    }
}

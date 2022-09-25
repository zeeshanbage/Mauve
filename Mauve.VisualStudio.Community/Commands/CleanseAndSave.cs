using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

using Mauve.VisualStudio.Community.Core;

using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using Task = System.Threading.Tasks.Task;

namespace Mauve.VisualStudio.Community.Commands
{
    internal sealed class CleanseAndSave : CommandBase<CleanseAndSave>
    {

        #region Constructor

        public CleanseAndSave(AsyncPackage package, OleMenuCommandService commandService) :
            base(0x0100, new Guid("7a54043a-2ca9-4ec1-8313-712c01c181ad"), package, commandService)
        { }

        #endregion

        #region Protected Methods

        protected override void Run()
        {
            string currentFile = GetActiveDocumentName();
            if (!string.IsNullOrWhiteSpace(currentFile))
            {
                // Notify that we've finished our work.
                Alert("Cleanse and save complete.");
            }
        }

        #endregion

    }
}

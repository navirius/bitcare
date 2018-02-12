using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;

namespace eSoft.T4.Core.FileBlocksManager
{
    public class VSFilePartsManager : FilePartsManager
    {
        // T4 template project item object from DTE
        private EnvDTE.ProjectItem  TemplateProjectItem { get; set; }

        // DTE
        private EnvDTE.DTE Dte { get; set; }

        // CheckOut action (can be overrided)
        private Action<String> CheckOutAction { get; set; }

        // Project synchronization action
        private Action ProjectSyncAction { get; set; }

        // Gets default project namespace
        public override String DefaultProjectNamespace
        {
            get { return TemplateProjectItem.ContainingProject.Properties.Item("DefaultNamespace").Value.ToString(); }
        }

        // Gets custom tool namespace for specific file
        public override String GetCustomToolNamespace(string fileName)
        {
            return Dte.Solution.FindProjectItem(fileName).Properties.Item("CustomToolNamespace").Value.ToString();
        }

        // Constructor
        public VSFilePartsManager(ITextTemplatingEngineHost host, StringBuilder template)  : base(host, template)
        {
            // Host Service Provider
            var hostServiceProvider = (IServiceProvider)host;

            if (hostServiceProvider == null)
                throw new ArgumentNullException("Could not obtain IServiceProvider");

            // DTE
            Dte = (EnvDTE.DTE)hostServiceProvider.GetService(typeof(EnvDTE.DTE));

            if (Dte == null)
                throw new ArgumentNullException("Could not obtain DTE from host");

            // T4 template item
            TemplateProjectItem = Dte.Solution.FindProjectItem(host.TemplateFile);

            // Default checkout action - just checkout using DTE
            CheckOutAction = (fileName) => Dte.SourceControl.CheckOutItem(fileName);

            // Default project sync action
            ProjectSyncAction = () => DefaultProjectSyncActionBody();
        }

        // Default project synchronization action implementation
        private void DefaultProjectSyncActionBody()
        {
            // Dictionary for project files items by name
            var projectFiles = new Dictionary<String, EnvDTE.ProjectItem>();

            // Original output file extension
            string originalFilePrefix = Path.GetFileNameWithoutExtension(TemplateProjectItem.get_FileNames(0)) + ".";

            // Create dictionary for existing template output items
            foreach (EnvDTE.ProjectItem projectItem in TemplateProjectItem.ProjectItems)
                projectFiles.Add(projectItem.get_FileNames(0), projectItem);

            // Remove unused items from the project
            foreach (string existingFileName in projectFiles.Keys)
                if (!GeneratedFileNames.Contains(existingFileName) && !(Path.GetFileNameWithoutExtension(existingFileName) + ".").StartsWith(originalFilePrefix))
                    projectFiles[existingFileName].Delete();

            // Add missing files to the project
            foreach (string fileName in GeneratedFileNames)
                if (!projectFiles.ContainsKey(fileName))
                    TemplateProjectItem.ProjectItems.AddFromFile(fileName);
        }

        // Checkout when necessary
        private void CheckoutFileIfRequired(String fileName)
        {
            // Source Control from DTE
            var sc = Dte.SourceControl;

            // Do we have to  checkout the file?
            if (sc != null && sc.IsItemUnderSCC(fileName) && !sc.IsItemCheckedOut(fileName))
                CheckOutAction.EndInvoke(CheckOutAction.BeginInvoke(fileName, null, null));
        }

        // Generate output with files synchronization
        public override void GenerateOutput(bool splitIntoMultipleFiles = false, bool sortFileOrder = false, bool sortBlockOrder = false)
        {
            if (TemplateProjectItem.ProjectItems == null)
                return;

            // Generate output
            base.GenerateOutput(splitIntoMultipleFiles, sortFileOrder, sortBlockOrder);

            // Synchronize project files
            ProjectSyncAction.EndInvoke(ProjectSyncAction.BeginInvoke(null,null));
        }

        // Create file doing checkout action when necessary
        protected override void CreateFile(String fileName, String content)
        {
            if (HasDifferentContent(fileName, content))
            {
                CheckoutFileIfRequired(fileName);
                File.WriteAllText(fileName, content);
            }
        }
    }
}

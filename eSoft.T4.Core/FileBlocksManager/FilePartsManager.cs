using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;

namespace eSoft.T4.Core.FileBlocksManager
{
    public class FilePartsManager
    {
        private const string m_DefaultName = "___Default___5B608D06-6FC8-4913-BC98-3F96B05D7627___";

        private ITextTemplatingEngineHost m_Host;
        private StringBuilder m_Template;

        // All the files we want to generate
        private Dictionary<string, FileBlocks> m_FilesDictionary;

        // We register files, when we create them to reconstruct there in the same order
        private List<string> m_OrderedFiles;

        // All generated files (paths)
        public List<String> GeneratedFileNames { get; set; }

        // Pending blocks
        private Stack<FileBlockInfo> PendingBlocks;

        // Text start position for current block
        private int m_CurrentBlockStartPos;

        // Current file block part
        private FileBlockPart CurrentBlockPart { get; set; }

        // Create proper file parts manager
        public static FilePartsManager Create(ITextTemplatingEngineHost engineHost, StringBuilder template)
        {
            // Is it VStudio?
            if (engineHost is IServiceProvider)
                return new VSFilePartsManager(engineHost, template);

            // Default host (external)
            return new FilePartsManager(engineHost, template);
        }

        // Constructor
        public FilePartsManager(ITextTemplatingEngineHost host, StringBuilder template)
        {
            m_Host = host;
            m_Template = template;
            m_FilesDictionary = new Dictionary<string, FileBlocks>();
            m_OrderedFiles = new List<string>();
            PendingBlocks = new Stack<FileBlockInfo>();
            GeneratedFileNames = new List<string>();

            m_CurrentBlockStartPos = 0;

            // By default we register all the text for default template output
            SwitchToDefaultFileAndBlock();
        }

        public void SwitchToFile(string fileName)
        {
            SwitchToFileBlock(fileName, m_DefaultName);
        }

        public void SwitchToBlock(string blockName = m_DefaultName)
        {
            SwitchToFileBlock(CurrentBlockPart.Parent.Parent.FileName, blockName);
        }

        public void SwitchToDefaultFile()
        {
            SwitchToFile(m_DefaultName);
        }

        public void SwitchToDefaultBlock()
        {
            SwitchToBlock(m_DefaultName);
        }

        public void SwitchToDefaultFileAndBlock()
        {
            SwitchToFileBlock(m_DefaultName, m_DefaultName);
        }

        public void EndCurrentBlock()
        {
            int stringLength= m_Template.Length - m_CurrentBlockStartPos;
            
            // No output generated for block?
            if (stringLength == 0)
                return;

            // No any block yet?
            if (CurrentBlockPart == null)
                return;

            // Prepare output text for the current block part
            CurrentBlockPart.Content = m_Template.ToString(m_CurrentBlockStartPos, stringLength);

            // Modify pointer
            m_CurrentBlockStartPos = m_Template.Length;
        }

        public void SwitchToFileBlock(string fileName, string blockName)
        {
            EndCurrentBlock();

            // Put current one file block on the stack...
            if (CurrentBlockPart!=null)
                PendingBlocks.Push(CurrentBlockPart.Parent);

            // Create object for provided file name
            FileBlocks fileBlocks=new FileBlocks(fileName);

            // If it exists already just use it...
            if (m_FilesDictionary.ContainsKey(fileName))
                fileBlocks = m_FilesDictionary[fileName];
            else
            {
                m_FilesDictionary.Add(fileName, fileBlocks);
                m_OrderedFiles.Add(fileName);
            }

            // Create a holder for a new one piece of the text/code
            CurrentBlockPart = fileBlocks.GetBlock(blockName).GetNewBlockPart();
        }

        public void RestorePreviousBlock()
        {
            EndCurrentBlock();

            // Store it as a current one piece of the text/code
            CurrentBlockPart = PendingBlocks.Pop().GetNewBlockPart();
        }

        public virtual void GenerateOutput(bool splitIntoMultipleFiles = false, bool sortFileOrder = false, bool sortBlockOrder=false)
        {
            // If we want to leave it as it is then don't process the files
            if (!splitIntoMultipleFiles)
                return;

            // Should we sort generated files?
            if (sortFileOrder)
                m_OrderedFiles.Sort();

            // Reverse files order to insert into project in proper order
            m_OrderedFiles.Reverse();

            // Template file output path
            String outputPath = Path.GetDirectoryName(m_Host.TemplateFile);

            // Generate output for each file
            foreach(string orderedFileName in m_OrderedFiles)
            {
                FileBlocks fileBlocks = m_FilesDictionary[orderedFileName];
                fileBlocks.GenerateOutput(sortBlockOrder);

                // All the files except for default one
                if (orderedFileName != m_DefaultName)
                {
                    String fileName = Path.Combine(outputPath, fileBlocks.FileName);
                    GeneratedFileNames.Add(fileName);
                    CreateFile(fileName, fileBlocks.Output);
                }
            }

            // Leave default output with default file content
            m_Template.Clear();
            m_Template.Append(m_FilesDictionary[m_DefaultName].Output);
        }

        protected virtual void CreateFile(String filePath, String newContent)
        {
            if (HasDifferentContent(filePath, newContent))
                File.WriteAllText(filePath, newContent);
        }

        protected bool HasDifferentContent(String filePath, String newContent)
        {
            return ! (File.Exists(filePath) && File.ReadAllText(filePath) == newContent);
        }

        // VStudio integration stuff - CustomToolNamespace
        public virtual String GetCustomToolNamespace(String fileName)
        {
            return null;
        }

        // VStudio integration stuff - CustomToolNamespace
        public virtual String DefaultProjectNamespace
        {
            get { return null; }
        }
    }
}

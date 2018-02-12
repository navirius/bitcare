using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Core.FileBlocksManager
{
    public class FileBlocks
    {
        public string FileName { get; set; }
        public Dictionary<string, FileBlockInfo> Blocks { get; set; }
        public List<string> OrderedBlocks { get; set; }

        // Output content after generation
        public string Output { get; set; }

        // Constructor
        public FileBlocks(string fileName)
        {
            FileName = fileName;
            Blocks = new Dictionary<string, FileBlockInfo>();
            OrderedBlocks = new List<string>();
        }

        public FileBlockInfo GetBlock(string blockName)
        {
            // Create object for provided block name
            FileBlockInfo fileBlockInfo = new FileBlockInfo(this, blockName);

            // Look for proper one block on the list
            if (Blocks.ContainsKey(blockName))
                fileBlockInfo = Blocks[blockName];
            else
            {
                Blocks.Add(blockName, fileBlockInfo);
                OrderedBlocks.Add(blockName);
            }

            // Return result
            return fileBlockInfo;
        }

        public void GenerateOutput(bool sortBlockOrder)
        {
            // Should we sort block order?
            if (sortBlockOrder)
                OrderedBlocks.Sort();

            StringBuilder result = new StringBuilder();
            OrderedBlocks.ForEach(blockName => result.Append(Blocks[blockName].GetOutputContent()));
            Output = result.ToString();
        }
    }
}

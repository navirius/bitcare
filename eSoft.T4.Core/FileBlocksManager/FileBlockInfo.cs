using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Core.FileBlocksManager
{
    public class FileBlockInfo
    {
        public FileBlocks Parent { get; set; }

        public string BlockName { get; set; }
        public List<FileBlockPart> FileBlockParts { get; set; }

        public FileBlockInfo(FileBlocks parent, string blockName)
        {
            Parent = parent;
            BlockName = blockName;

            FileBlockParts = new List<FileBlockPart>();
        }

        public FileBlockPart GetNewBlockPart()
        {
            // Create new block part
            FileBlockPart fileBlockPart = new FileBlockPart(this);

            // Store it on the list
            FileBlockParts.Add(fileBlockPart);

            // Return result
            return fileBlockPart;
        }

        public  string GetOutputContent()
        {
            StringBuilder result = new StringBuilder();
            FileBlockParts.ForEach(part => result.Append(part.Content));
            return result.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSoft.T4.Core.FileBlocksManager
{
    public class FileBlockPart
    {
        public FileBlockInfo Parent { get; set; }
        public string Content { get; set; }

        public FileBlockPart(FileBlockInfo parent)
        {
            Parent = parent;
            Content = "";
        }
    }
}

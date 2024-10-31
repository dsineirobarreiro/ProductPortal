using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaMad.Model.TagServiceNS
{
    public class TagBlock
    {
        public List<Etiqueta> Tags { get; private set; }
        public bool ExistMoreTags { get; private set; }

        public TagBlock(List<Etiqueta> tags, bool existMoreTags)
        {
            this.Tags = tags;
            this.ExistMoreTags = existMoreTags;
        }
    }
}

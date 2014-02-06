using System;
using System.Collections;
using System.Collections.Generic;

namespace RogueLoise
{
    public class CreatureAttribute
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public int BaseValue { get; set; }
    }

    public class AttributeModificator
    {
        public IDictionary<string, int> Attributes { get; set; }

        public int Mod { get; set; }

        public string Key { get; set; }

        public long TimeLast { get; set; }
    }
}
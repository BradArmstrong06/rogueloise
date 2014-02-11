using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RogueLoise
{
    public class CreatureAttribute
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public int BaseValue { get; set; }

        public CreatureAttribute Clone()
        {
            return new CreatureAttribute
            {
                Key = Key,
                Name = Name,
                BaseValue = BaseValue
            };
        }

    }

    public class AttributeModificator
    {
        public int Mod { get; set; }

        public string Key { get; set; }

        public string AttributeKey { get; set; }

        public double TimeLast { get; set; }

        public AttributeModificator Clone()
        {
            return new AttributeModificator
            {
                Key = Key,
                AttributeKey = AttributeKey,
                Mod = Mod,
                TimeLast = TimeLast
            };
        }
    }
}
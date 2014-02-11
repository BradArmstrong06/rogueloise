using System.Collections.Generic;

namespace RogueLoise
{
    public class ObjectDictionary
    {
        private Dictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();

        public void Add(GameObject obj)
        {
            _objects.Add(obj.Key, obj); //todo exc catch
        }

        public void Add(GameObject obj, string key)
        {
            obj.Key = key;
            _objects.Add(key, obj); //todo exc catch
        }

        public GameObject this[string key]
        {
            get
            {
                return Get(key);
            }
        }

        public GameObject Get(string key)
        {
            GameObject obj;
            if (_objects.TryGetValue(key, out obj))
            {
                return obj.Clone();
            }
            return null;
        }
    }
}
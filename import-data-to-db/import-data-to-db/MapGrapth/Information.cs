using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Information
    {
        public string Name { get; }
        public int Count => _datas.Count;
        private List<KeyValuePair<string, string>> _datas;

        public string this[int index]
        {
            get
            {
                return _datas[index].Value;
            }
        }

        public string this[string key]
        {
            get
            {
                for (int loop = 0; loop < Count; loop++)
                {
                    if (_datas[loop].Key == key) return _datas[loop].Value;
                }

                throw new Exception("Key is not exist");
            }
        }

        public bool Contains(string key)
        {
            for (int loop = 0; loop < Count; loop++)
            {
                if (_datas[loop].Key == key) return true;
            }

            return false;
        }

        public void Add(string key, string value)
        {
            _datas.Add(new KeyValuePair<string, string>(key, value));
        }

        public Information(string name)
        {
            Name = name;
            _datas = new List<KeyValuePair<string, string>>();
        }
    }
}

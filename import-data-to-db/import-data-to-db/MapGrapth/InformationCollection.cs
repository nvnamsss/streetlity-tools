using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class InformationCollection : IEnumerable
    {
        public int Count => _infos.Count;
        public Information this[int index]
        {
            get
            {
                return _infos[index];
            }
        }
        private List<Information> _infos;
        public InformationCollection()
        {
            _infos = new List<Information>();
        }

        public void Add(Information info)
        {
            _infos.Add(info);
        }

        public IEnumerator GetEnumerator()
        {
            return _infos.GetEnumerator();
        }
    }
}

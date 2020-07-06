using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class InformationCollection : IEnumerable
    {
        public event EventHandler<Information> Added;
        public int Count => _infos.Count;
        public Information this[int index]
        {
            get
            {
                return _infos[index];
            }
        }

        public List<Information> this[string name]
        {
            get
            {
                List<Information> rs = new List<Information>();
                foreach (Information info in _infos)
                {
                    if (info.Name == name) rs.Add(info);
                }
                return rs;
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
            Added?.Invoke(this, info);
        }

        public IEnumerator GetEnumerator()
        {
            return _infos.GetEnumerator();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class Breadcrumbs
    {
        private string _url;
        public string Breadcrumb { get; set; }
        public bool Active { get; set; }
        public int ItemNumber { get; set; }

        public string Url
        {
            get
            {
                if (_url == null)
                    _url = "#";
                return _url;
            }
            set => _url = value;
        }
    }
}
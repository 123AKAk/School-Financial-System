using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace adolphus
{
    [Serializable]
    public class aencrypt : INotifyPropertyChanged
    {
        private XElement ency;
        internal XElement aEncy { get { return ency; } }
        internal aencrypt(XElement ency)
        {
            this.ency = ency;
        }
        public string CreatedDateTime
        {
            get { return ency.Element("createdatetime").Value; }
            set
            {
                ency.Element("createdatetime").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CreatedDateTime"));
                }
            }
        }
        public string Description
        {
            get { return ency.Element("description").Value; }
            set
            {
                ency.Element("description").Value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Description"));
                }
            }
        }

        public string DoneDateTime
        {
            get { return ency.Element("donedatetime") == null ? "" : ency.Element("donedatetime").Value; }
            set
            {
                if (ency.Element("donedatetime") == null)
                {
                    ency.Add(new XElement("donedatetime"));
                }

                ency.Element("donedatetime").Value = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DoneDateTime"));
                }
            }
        }

        public bool IsDone { get { return (ency.Element("donedatetime") != null); } }

        public override string ToString()
        {
            string result;
            if (DoneDateTime != "")
            {
                DateTime doneAt = DateTime.Parse(DoneDateTime);
                result = String.Format("{0} - Done at: {1:yyyy-MM-dd hh:mm}", Description, doneAt);
            }
            else
            {
                result = Description;
            }
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
    public class encrypt : INotifyPropertyChanged
    {
        [NonSerialized]
        private XDocument encypList;
        [NonSerialized]
        private bool showDoneItems;
        [NonSerialized]
        private List<ToDoItem> items;

        public List<ToDoItem> Items
        {
            get
            {
                if (showDoneItems)
                {
                    return items;
                }
                else
                {
                    return (from i in items
                            where i.DoneDateTime == ""
                            select i).ToList();
                }
            }
        }

        public bool ShowDoneItems
        {
            get { return showDoneItems; }
            set
            {
                showDoneItems = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowDoneItems"));
                    PropertyChanged(this, new PropertyChangedEventArgs("Items"));
                }
            }
        }

        public encrypt()
        {
            AppSettingsReader asr = new AppSettingsReader();
            string listFileName = (string)asr.GetValue("alistFileName", typeof(string));
            if (File.Exists(listFileName))
            {
                encypList = XDocument.Load(listFileName);
                items = (from e in encypList.Root.Elements()
                         select new ToDoItem(e)).ToList();
            }
            else
            {
                encypList = new XDocument();
                encypList.Add(new XElement("encypList"));
                items = new List<ToDoItem>();
            }

        }

        public void Additem(string description)
        {
            XElement item = new XElement("filename");
            XElement createAt = new XElement("createdatetime");
            createAt.Value = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms");
            item.Add(createAt);
            XElement desc = new XElement("description");
            desc.Value = description;
            item.Add(desc);

            encypList.Root.Add(item);
            items.Add(new ToDoItem(item));

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }

        public void DeleteItem(ToDoItem item)
        {
            items.Remove(item);
            item.ToDo.Remove();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save()
        {
            AppSettingsReader asr = new AppSettingsReader();
            string listFileName = (string)asr.GetValue("alistFileName", typeof(string));
            encypList.Save(listFileName);
        }
    }
}

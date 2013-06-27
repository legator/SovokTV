using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovokTV.Model
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase()
        {
        }

        public void RaisePropertyChanged(string name)
        {
            bool flag = this.PropertyChanged == null;
            if (!flag)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            bool flag;
            bool flag1 = !object.Equals(storage, value);
            if (flag1)
            {
                storage = value;
                this.RaisePropertyChanged(propertyName);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

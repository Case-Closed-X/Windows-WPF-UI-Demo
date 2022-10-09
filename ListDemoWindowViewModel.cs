using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI_Demo
{
    internal class ListDemoWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ModelsItem> listBoxItems;

        public ObservableCollection<ModelsItem> ListBoxItems
        {
            get { return listBoxItems; }
            set { 
                listBoxItems = value; 
                OnPropertyChanged(); 
            }
        }

        public ListDemoWindowViewModel()
        {
            ListBoxItems = new();
            ListBoxItems.Add(new ModelsItem() { Title = "随便写点什么", Completion = 80 });
            ListBoxItems.Add(new ModelsItem() { Title = "再随便写点啥啥啥啥啥啥啥", Completion = 60 });
            ListBoxItems.Add(new ModelsItem() { Title = "随便随便随便随便", Completion = 40 });
            ListBoxItems.Add(new ModelsItem() { Title = "BlahBlahBlah", Completion = 10 });
        }
    }
    public class ModelsItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Notes
{
    public class Note : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }
        private string title;
        private string content;
        private DateTime? dateCreate;
        private DateTime? dateUpdate;
        private string author;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        public DateTime? DateCreate
        {
            get => dateCreate;
            set
            {
                dateCreate = value;
                OnPropertyChanged(nameof(DateCreate));
            }
        }
        public DateTime? DateUpdate
        {
            get => dateUpdate;
            set
            {
                dateUpdate = value;
                OnPropertyChanged(nameof(DateUpdate));
            }
        }
        public string Author
        {
            get => author;
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

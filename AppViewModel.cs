using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Notes
{
    public class AppViewModel : INotifyPropertyChanged
    {
        NotesContext db;
        private INoteService noteService;
        private Note selectedNote;
        public Note SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
            }
        }
        IEnumerable<Note> notes { get; set; }
        public IEnumerable<Note> Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        public AppViewModel(INoteService NoteService)
        {
            noteService = NoteService;
            db = new NotesContext();
            db.Notes.OrderByDescending(x => x.Id).Load();
            Notes = db.Notes.Local.ToBindingList();
        }

        RelayCommand createNote;
        RelayCommand editNote;
        RelayCommand deleteNote;
        RelayCommand saveNote;
        public RelayCommand CreateNote
        {
            get
            {
                return createNote ??
                  (createNote = new RelayCommand((o) =>
                  {
                      wndNote wndNote = new wndNote(new Note());
                      wndNote.Owner = Application.Current.MainWindow;
                      wndNote.Title = "Новая заметка";
                      if (wndNote.ShowDialog() == true)
                      {
                          Note note = wndNote.Note;
                          note.DateCreate = System.DateTime.Now;
                          note.Author = "User";
                          db.Notes.Add(note);
                          db.SaveChanges();
                          Notes = Notes.OrderByDescending(x => x.Id);
                      }
                  }));
            }
        }
        public RelayCommand EditNote
        {
            get
            {
                return editNote ??
                  (editNote = new RelayCommand((o) =>
                  {
                      if (selectedNote == null) return;

                      Note note = selectedNote;
                      wndNote wndNote = new wndNote(new Note
                                                      {
                                                          Id = note.Id,
                                                          Title = note.Title,
                                                          Content = note.Content,
                                                          DateCreate = note.DateCreate,
                                                          DateUpdate = note.DateUpdate,
                                                          Author = note.Author
                                                      });
                      wndNote.Owner = Application.Current.MainWindow;
                      wndNote.Title = "Редактирование";

                      if (wndNote.ShowDialog() == true)
                      {
                          note = db.Notes.Find(wndNote.Note.Id);
                          if (note != null)
                          {
                              note.Title = wndNote.Note.Title;
                              note.Content = wndNote.Note.Content;
                              note.DateCreate = wndNote.Note.DateCreate;
                              note.DateUpdate = System.DateTime.Now;
                              note.Author = "User";
                              db.Entry(note).State = EntityState.Modified;
                              db.SaveChanges();
                              Notes = Notes.OrderByDescending(x => x.Id);
                          }
                      }
                  }));
            }
        }
        public RelayCommand DeleteNote
        {
            get
            {
                return deleteNote ??
                  (deleteNote = new RelayCommand((o) =>
                  {
                      if (selectedNote == null) return;
                      if (MessageBox.Show("Удалить заметку?", Application.Current.MainWindow.Title, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                      {
                          Note note = selectedNote;
                          db.Notes.Remove(note);
                          db.SaveChanges();
                      }
                  }));
            }
        }
        public RelayCommand SaveNote
        {
            get
            {
                return saveNote ??
                  (saveNote = new RelayCommand((o) =>
                  {
                      if (selectedNote == null) return;

                      Note note = selectedNote;
                      noteService.SaveFile(note.Title, note.Content);
                  }));
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

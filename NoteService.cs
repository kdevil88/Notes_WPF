using System.IO;
using System.Windows.Forms;

namespace Notes
{
    public interface INoteService
    {
        bool SaveFile(string title, string content);
    }

    public class NoteService : INoteService
    {
        public bool SaveFile(string title, string content)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = Path.ChangeExtension(title, ".txt");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(saveFileDialog.FileName, content);
                else
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

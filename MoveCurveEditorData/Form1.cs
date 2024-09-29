using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace MoveCurveEditorData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String getCEVersion(byte[] rawData)
        {
            byte[] headerData = new byte[4];
            byte[] CEV2 = { 0x43, 0x45, 0x56, 0x32 };
            Array.Copy(rawData, headerData, 4);

            if (rawData.Length == 1839104)
            {
                return "v1.x";
            } 
            else if (headerData.SequenceEqual(CEV2))
            {
                return "v2.0Alpha";
            } 
            else
            {
                return "v2.0";
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.String read_aup = txtFromAup.Text;
            System.String write_aup = txtToAup.Text;

            String readCev = "";     // 保存しているCurve Editorバージョン情報
            String writeCev = "";
            RawFilterProject? compData = null;

            if (read_aup.Length == 0)
            {
                txtLog.Text = "[エラー] 移動元のAUPファイルを指定してください。";
                return;
            }
            else if (!File.Exists(read_aup))
            {
                txtLog.Text = $"[エラー] \"{read_aup}\"が見つかりません。";
                return;
            }
            else if (!File.Exists(write_aup))
            {
                txtLog.Text = $"[エラー] \"{write_aup}\"が見つかりません。";
                return;
            }

            try
            {
                // 読込元のAUPファイル精査
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var aup = new AviUtlProject();
                using (var reader = new BinaryReader(File.OpenRead(read_aup)))
                {
                    aup.Read(reader);
                }

                bool hasCurveEditor = false;
                for (int i = 0; i < aup.FilterProjects.Count; i++)
                {
                    if (aup.FilterProjects[i].Name == "Curve Editor")
                    {
                        hasCurveEditor = true;
                        compData = (RawFilterProject)aup.FilterProjects[i];
                        readCev = getCEVersion(compData.DumpData());

                    }
                }
                if (!hasCurveEditor || compData == null)
                {
                    txtLog.Text = $"[エラー] 移動元のAUPファイルにCurve Editorの設定が存在しませんでした。";
                    return;
                }
                // Debug.WriteLine(BitConverter.ToString(compData.DumpData()));
            }
            catch (FileFormatException ex)
            {
                txtLog.Text = "[エラー] 移動元のAUPファイルが壊れている可能性があります。";
                return;
            }

            try
            {
                // 書込先のAUPファイル精査・書き換え
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var aup = new AviUtlProject();
                using (var reader = new BinaryReader(File.OpenRead(write_aup)))
                {
                    aup.Read(reader);
                }

                for (int i = 0; i < aup.FilterProjects.Count; i++)
                {
                    if (aup.FilterProjects[i].Name == "Curve Editor")
                    {
                        writeCev = getCEVersion(aup.FilterProjects[i].DumpData());
                        aup.FilterProjects[i] = compData;

                    }
                }

                // Curve Editor非導入下プロジェクトの場合
                if (writeCev == "")
                {
                    aup.FilterProjects.Add(compData);
                    writeCev = readCev;
                }

                // 書き出し
                File.Copy(write_aup, write_aup + "_backup", true);
                using (var writer = new BinaryWriter(File.OpenWrite(write_aup)))
                {
                    aup.Write(writer);
                }
            }
            catch (FileFormatException ex)
            {
                txtLog.Text = "[エラー] 移動先のAUPファイルが壊れている可能性があります。";
                return;
            }

            if (readCev.SequenceEqual(writeCev) || (readCev == "v1.x" && writeCev == "v2.x"))
            {
                txtLog.Text = "実行が完了しました。";
                var encoding = Encoding.GetEncoding("Shift-JIS");
            }
            else
            {
                txtLog.Text = $"[警告] 移動元と移動先のCurve Editorに互換性がありません。\nCurve Editor {readCev} で読み込むようにしてください。（コピー前: {writeCev}）";
            }
        }

        private void txtFromAup_DragEnter(object sender, DragEventArgs e)
        {
            // ドラッグドロップ時にカーソルの形状を変更
            e.Effect = DragDropEffects.All;
        }

        private void txtFromAup_DragDrop(object sender, DragEventArgs e)
        {
            // ファイルが渡されていなければ、何もしない
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            // 渡されたファイルに対して処理を行う
            foreach (var filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                txtFromAup.Text = filePath;
            }
        }

        private void txtToAup_DragEnter(object sender, DragEventArgs e)
        {
            // ドラッグドロップ時にカーソルの形状を変更
            e.Effect = DragDropEffects.All;
        }

        private void txtToAup_DragDrop(object sender, DragEventArgs e)
        {
            // ファイルが渡されていなければ、何もしない
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            // 渡されたファイルに対して処理を行う
            foreach (var filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                txtToAup.Text = filePath;
            }
        }

        private void btnFromAup_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AviUtl Project File (*.aup)|*.aup;*.aup_backup|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                txtFromAup.Text = filePath;
            }
        }

        private void btnToAup_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AviUtl Project File (*.aup)|*.aup;*.aup_backup|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                txtToAup.Text = filePath;
            }
        }
    }
}

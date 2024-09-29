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

            String readCev = "";     // �ۑ����Ă���Curve Editor�o�[�W�������
            String writeCev = "";
            RawFilterProject? compData = null;

            if (read_aup.Length == 0)
            {
                txtLog.Text = "[�G���[] �ړ�����AUP�t�@�C�����w�肵�Ă��������B";
                return;
            }
            else if (!File.Exists(read_aup))
            {
                txtLog.Text = $"[�G���[] \"{read_aup}\"��������܂���B";
                return;
            }
            else if (!File.Exists(write_aup))
            {
                txtLog.Text = $"[�G���[] \"{write_aup}\"��������܂���B";
                return;
            }

            try
            {
                // �Ǎ�����AUP�t�@�C������
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
                    txtLog.Text = $"[�G���[] �ړ�����AUP�t�@�C����Curve Editor�̐ݒ肪���݂��܂���ł����B";
                    return;
                }
                // Debug.WriteLine(BitConverter.ToString(compData.DumpData()));
            }
            catch (FileFormatException ex)
            {
                txtLog.Text = "[�G���[] �ړ�����AUP�t�@�C�������Ă���\��������܂��B";
                return;
            }

            try
            {
                // �������AUP�t�@�C�������E��������
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

                // Curve Editor�񓱓����v���W�F�N�g�̏ꍇ
                if (writeCev == "")
                {
                    aup.FilterProjects.Add(compData);
                    writeCev = readCev;
                }

                // �����o��
                File.Copy(write_aup, write_aup + "_backup", true);
                using (var writer = new BinaryWriter(File.OpenWrite(write_aup)))
                {
                    aup.Write(writer);
                }
            }
            catch (FileFormatException ex)
            {
                txtLog.Text = "[�G���[] �ړ����AUP�t�@�C�������Ă���\��������܂��B";
                return;
            }

            if (readCev.SequenceEqual(writeCev) || (readCev == "v1.x" && writeCev == "v2.x"))
            {
                txtLog.Text = "���s���������܂����B";
                var encoding = Encoding.GetEncoding("Shift-JIS");
            }
            else
            {
                txtLog.Text = $"[�x��] �ړ����ƈړ����Curve Editor�Ɍ݊���������܂���B\nCurve Editor {readCev} �œǂݍ��ނ悤�ɂ��Ă��������B�i�R�s�[�O: {writeCev}�j";
            }
        }

        private void txtFromAup_DragEnter(object sender, DragEventArgs e)
        {
            // �h���b�O�h���b�v���ɃJ�[�\���̌`���ύX
            e.Effect = DragDropEffects.All;
        }

        private void txtFromAup_DragDrop(object sender, DragEventArgs e)
        {
            // �t�@�C�����n����Ă��Ȃ���΁A�������Ȃ�
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            // �n���ꂽ�t�@�C���ɑ΂��ď������s��
            foreach (var filePath in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                txtFromAup.Text = filePath;
            }
        }

        private void txtToAup_DragEnter(object sender, DragEventArgs e)
        {
            // �h���b�O�h���b�v���ɃJ�[�\���̌`���ύX
            e.Effect = DragDropEffects.All;
        }

        private void txtToAup_DragDrop(object sender, DragEventArgs e)
        {
            // �t�@�C�����n����Ă��Ȃ���΁A�������Ȃ�
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            // �n���ꂽ�t�@�C���ɑ΂��ď������s��
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

using System;
using System.Windows.Forms;

namespace Serializer
{
    public partial class MainForm : Form
    {
        Person currentPerson;
        public MainForm()
        {
            InitializeComponent();
            ShowPersonData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string name = textName.Text;
            string address = textAddress.Text;
            string phone = textPhone.Text;
            if (name != null && address != null && phone != null)
            {
                int lastSerial = Person.FindSerialEdges()[1];
                currentPerson = new Person(name, address, phone);
                currentPerson.Serial = ++lastSerial;
                currentPerson.Serialize();
            }
        }

        private void ShowPersonData(int serial = 1)
        {
            if (Person.FindSerialEdges()[0] > 0)
            {
                Person resultPerson = Person.Deserialize($"person{serial.ToString("D2")}.dat");
                currentPerson = ((resultPerson) == null)? currentPerson: resultPerson;
                if (currentPerson != null)
                {
                    textName.Text = currentPerson.Name;
                    textAddress.Text = currentPerson.Address;
                    textPhone.Text = currentPerson.PhoneNumber;
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int serial = currentPerson.Serial;
            ShowPersonData(++serial);
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            int serial = currentPerson.Serial;
            ShowPersonData(--serial);
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            int first = Person.FindSerialEdges()[0];
            ShowPersonData(first);
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            int last = Person.FindSerialEdges()[1];
            ShowPersonData(last);
        }
    }
}

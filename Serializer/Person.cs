using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Serializer
{
    [Serializable]
    class Person : IDeserializationCallback
    {
        string name;
        string address;
        string phoneNumber;
        DateTime recordingDate;
        [NonSerialized]
        int serial;

        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public DateTime RecordingDate { get => recordingDate; set => recordingDate = value; }
        public int Serial { get => serial; set => serial = value; }

        public Person(string name, string address, string phone)
        {
            Name = name;
            Address = address;
            PhoneNumber = phone;
            RecordingDate = DateTime.Now;
        }

        public void Serialize()
        {
            if (true)
            {
                using(FileStream stream = new FileStream($"person{Serial.ToString("D2")}.dat", FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }
            }
        }

        public static Person Deserialize(string input)
        {
            try
            {
                using(FileStream stream = new FileStream(input, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Person person;
                    person = formatter.Deserialize(stream) as Person;
                    person.Serial = int.Parse(input.Substring(input.Length-6, 2));
                    return person;
                }
            } catch(FileNotFoundException)
            {
                MessageBox.Show("Deserialization error - file is not found!");
            }
            return null;
        }

        public static int[] FindSerial()
        {
            int[] lastSerial = new int[2];
            List<int> serials = new List<int>();
            DirectoryInfo projectDir = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo file in projectDir.GetFiles("person*.dat"))
            {
                int serial = int.Parse(file.Name.Substring(file.Name.Length - 6, 2));
                serials.Add(serial);
            }
            lastSerial[0] = serials.Min();
            lastSerial[1] = serials.Max();
            return lastSerial;
        }

        public void OnDeserialization(object sender)
        {
            
        }
    }
}

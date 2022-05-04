﻿using Newtonsoft.Json;
using Projekat_SIMS_IN_TIM3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_SIMS_IN_TIM3.Repository
{
    public class MedicineRepository
    {
        private readonly string fileLocation = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Data\\medicines.json";
        public List<Medicine> medicines { get; set; } = new List<Medicine>();

        public MedicineRepository()
        {
            readJson();
        }

        public void readJson()
        {
            if (!File.Exists(fileLocation))
            {
                File.Create(fileLocation).Close();
            }

            using (StreamReader r = new StreamReader(fileLocation))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    medicines = JsonConvert.DeserializeObject<List<Medicine>>(json);
                }
            }
        }

        public void WriteToJson()
        {
            string json = JsonConvert.SerializeObject(medicines);
            File.WriteAllText(fileLocation, json);
        }

        public List<Medicine> getAll()
        {
            readJson();
            return medicines;
        }


        public void createMedicine(Medicine medicine)
        {
            medicines.Add(medicine);
            WriteToJson();
        }

        public void updateMedicine(Medicine medicine)
        {
            readJson();
            int idx = medicines.FindIndex(obj => obj.Id == medicine.Id);
            medicines[idx] = medicine;
            WriteToJson();
           
        }

        public void delete(int medId)
        {
            readJson();
            int idx = medicines.FindIndex(obj => obj.Id == medId);
            medicines.RemoveAt(idx);
            WriteToJson();
        }

        public List<String> getAllVerified()
        {
            readJson();
            List<String> list = new List<String>();

                foreach (Medicine medicine in medicines)
            {
                if(medicine.IsVerified == true)
                {
                    string retVal = medicine.Name;
                    list.Add(retVal);
                }
            }
            return list;
        }

        public int getIdByName(string name)
        {
            readJson();
            int id = int.MinValue;
            id = medicines.FindIndex(obj => obj.Name == name);
            return id;
        }
    }
}

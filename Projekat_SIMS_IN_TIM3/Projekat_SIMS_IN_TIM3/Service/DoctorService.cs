﻿using Projekat_SIMS_IN_TIM3.Model;
using Projekat_SIMS_IN_TIM3.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_SIMS_IN_TIM3.Service
{
    public class DoctorService
    {
        private DoctorRepository doctorRepository = new DoctorRepository();
        public List<Doctor> getAll()
        {
            return doctorRepository.getAll();
        }

        public bool saveAndUpdate(Doctor doctor)
        {
            return doctorRepository.saveAndUpdate(doctor);
        }

        public bool delete(int id)
        {
            return doctorRepository.delete(id);
        }
    }
}
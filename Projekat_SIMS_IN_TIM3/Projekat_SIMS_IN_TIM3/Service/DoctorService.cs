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
        public List<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Doctor GetById(int id)
        {
            return doctorRepository.getById(id);
        }

        public int getIdByNameAndSurname(string name, string surname)
        {
            return doctorRepository.getIdByNameAndSurname(name, surname);
        }

        public int getIdByUsername(string usname)
        {
            return doctorRepository.getIdByUsername(usname);
        }

        public bool saveAndUpdate(Doctor doctor)
        {
            return doctorRepository.saveAndUpdate(doctor);
        }

        public bool delete(int id)
        {
            return doctorRepository.delete(id);
        }

        public List<string> nameSurnameSpec()
        {
            return doctorRepository.nameSurnameSpec();
        }

        public void addGrade(DoctorGradeDTO gradeDTO, int doctorId)
        {
            Doctor doctor = doctorRepository.getById(doctorId);

            doctor = applyGrades(doctor, gradeDTO);

            doctorRepository.delete(doctorId);
            doctorRepository.saveAndUpdate(doctor);
        }

        public Doctor applyGrades(Doctor doctor, DoctorGradeDTO gradeDTO)
        {

            doctor.KnowledgeGrades.Add(gradeDTO.KnowledgeGrade);
            doctor.HelpfulnessGrades.Add(gradeDTO.HelpfulnessGrade);
            doctor.PunctualityGrades.Add(gradeDTO.PunctualityGrade);
            doctor.PleasantnessGrades.Add(gradeDTO.PleasantnessGrade);

            return doctor;
        }


    }
}

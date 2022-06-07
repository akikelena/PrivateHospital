﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekat_SIMS_IN_TIM3.Model;
using Projekat_SIMS_IN_TIM3.Repository;

namespace Projekat_SIMS_IN_TIM3.Service
{
    public class DoctorGradeService
    {
        public DoctorGradeRepository DoctorGradeRepository = new DoctorGradeRepository();

        public void Create(DoctorGrade grade)
        {
            this.DoctorGradeRepository.Create(grade);
        }
        public List<DoctorGrade> GetAllByDoctorId(int id)
        {
            List<DoctorGrade> retVal = new List<DoctorGrade>();
            foreach (var grade in this.GetAll())
            {
                if (grade.doctorId == id)
                {
                    retVal.Add(grade);
                }
            }
            return retVal;
        }
        public List<DoctorGrade> GetAll()
        {
            return DoctorGradeRepository.GetAll();
        }
    }
}
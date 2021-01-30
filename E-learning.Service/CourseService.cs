﻿using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Service
{
  public class CourseService:ICourseService
  {
    ICourseRepository repository;

    public CourseService(ICourseRepository rep)
    {
      this.repository = rep;
    }

    public Kurs GetCourseById(int id)
    {
      return repository.GetCourseById(id);
    }

    public List<Kurs> GetAllCourses()
    {
      return repository.GetAllCourses();
    }

    public List<KursNivelTip> GetCourseSections(int id)
    {
      return repository.GetCourseSections(id);
    }
  }
}

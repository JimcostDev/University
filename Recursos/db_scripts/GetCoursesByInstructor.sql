/****** Script for SelectTopNRows command from SSMS  ******/
SELECT course.*
  FROM [University].[dbo].[CourseInstructor] courseInst
  JOIN [dbo].[Course] course ON course.CourseID = courseInst.CourseID --RELACION DE LA FK A LA PK
  WHERE courseInst.InstructorID = 5
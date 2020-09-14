SELECT course.*
  FROM [University].[dbo].[Enrollment] Enroll
  JOIN [dbo].[Course] course ON course.CourseID = Enroll.CourseID -- RELACION DE LA FK A LA PK
  WHERE Enroll.StudentID = 1009
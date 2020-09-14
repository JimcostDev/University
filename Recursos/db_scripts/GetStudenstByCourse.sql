SELECT student.*
  FROM [dbo].[Enrollment] Enroll 
  JOIN [dbo].[Student] student ON student.ID = Enroll.StudentID -- RELACION DE LA FK A LA PK
  WHERE CourseID = 1045

/*SELECT S.*
FROM [dbo].[Enrollment] E
JOIN [dbo].[Student] S ON S.ID = E.StudentID 
WHERE [CourseID] = 1045*/
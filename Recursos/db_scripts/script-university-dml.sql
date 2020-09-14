USE [University]
GO

INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Alexander', N'Carson', CAST(N'2005-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Alonso', N'Meredith', CAST(N'2002-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Anand', N'Arturo', CAST(N'2003-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Barzdukas', N'Gytis', CAST(N'2002-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Li', N'Yan', CAST(N'2002-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Justice', N'Peggy', CAST(N'2001-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Norman', N'Laura', CAST(N'2003-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([LastName], [FirstMidName], [EnrollmentDate]) VALUES (N'Olivetto', N'Nino', CAST(N'2005-09-01T00:00:00.000' AS DateTime))

INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (1045, N'Calculus', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (1050, N'Chemistry', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (2021, N'Composition', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (2042, N'Literature', 4)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (3141, N'Trigonometry', 4)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (4022, N'Microeconomics', 3)
INSERT [dbo].[Course] ([CourseID], [Title], [Credits]) VALUES (4041, N'Macroeconomics', 3)

INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (1050, 1009, 0)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (4022, 1016, 2)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (4041, 1011, 1)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (1045, 1009, 1)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (3141, 1009, 4)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (2021, 1009, 4)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (1050, 1013, 4)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (1050, 1012, 0)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (4022, 1010, 4)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (4041, 1009, 2)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (1045, 1015, 3)
INSERT [dbo].[Enrollment] ( [CourseID], [StudentID], [Grade]) VALUES (3141, 1014, 0)

INSERT [dbo].[Instructor] ( [LastName], [FirstMidName], [HireDate]) VALUES (N'Abercrombie', N'Kim', CAST(N'1995-03-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ( [LastName], [FirstMidName], [HireDate]) VALUES (N'Fakhouri', N'Fadi', CAST(N'2002-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ( [LastName], [FirstMidName], [HireDate]) VALUES (N'Harui', N'Roger', CAST(N'1998-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ( [LastName], [FirstMidName], [HireDate]) VALUES (N'Kapoor', N'Candace', CAST(N'2001-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ( [LastName], [FirstMidName], [HireDate]) VALUES (N'Zheng', N'Roger', CAST(N'2004-02-12T00:00:00.000' AS DateTime))

INSERT [dbo].[Department] ( [Name], [Budget], [StartDate], [InstructorID]) VALUES (N'English', CAST(350000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Department] ( [Name], [Budget], [StartDate], [InstructorID]) VALUES (N'Mathematics', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Department] ( [Name], [Budget], [StartDate], [InstructorID]) VALUES (N'Engineering', CAST(350000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Department] ( [Name], [Budget], [StartDate], [InstructorID]) VALUES (N'Economics', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 4)

INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (2, N'Smith 17')
INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (3, N'Gowan 27')
INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (4, N'Thompson 304')

INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (1050, 4)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (1050, 3)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (4022, 5)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (4041, 5)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (1045, 2)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (3141, 3)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (2021, 1)
INSERT [dbo].[CourseInstructor] ( [CourseID], [InstructorID]) VALUES (2042, 1)

USE [master]
GO

CREATE DATABASE [Lab1SchoolDatabase]
GO

USE [Lab1SchoolDatabase]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentFirstName] [nvarchar](50) NULL,
	[StudentLastName] [nvarchar](50) NULL,
	[SSN] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[Class] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [nvarchar](15) NOT NULL,
	[CourseName] [nvarchar](35) NOT NULL,
 CONSTRAINT [pk_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Grades]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId_FK] [int] NULL,
	[CourseId_FK] [nvarchar](15) NULL,
	[Grade] [int] NULL,
	[DateSet] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ShowGradesFromLastMonth]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ShowGradesFromLastMonth] AS
SELECT StudentFirstName, StudentLastName, CourseName, Grade, DateSet
FROM Grades
JOIN Students ON Grades.StudentId_FK = Students.Id
JOIN Courses ON Grades.CourseId_FK = Courses.Id
WHERE Grades.DateSet >= DATEADD(month, -1, GETDATE())
GO
/****** Object:  View [dbo].[GradesStatistics]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GradesStatistics] AS
SELECT CourseName, AVG(Grade) AS 'Average Grade', MAX(Grade) AS 'Highest Grade', MIN(Grade) AS 'Lowest Grade' FROM Grades
JOIN Courses ON Grades.CourseId_FK = Courses.Id
GROUP BY CourseName
GO
/****** Object:  View [dbo].[StudentList]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StudentList] AS
SELECT StudentFirstName, StudentLastName,SSN, [Address], PhoneNo, Class
FROM Students;
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseTeachers]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseTeachers](
	[FK_CourseId] [nvarchar](15) NULL,
	[FK_StaffId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[FK_CourseId] [nvarchar](15) NULL,
	[FK_StudentId] [int] NULL,
	[FK_ClassIdFromCourses] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 2023-12-10 09:32:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffFirstName] [nvarchar](50) NULL,
	[StaffLastName] [nvarchar](50) NULL,
	[SSN] [nvarchar](12) NULL,
	[Address] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](20) NULL,
	[Role] [nvarchar](15) NULL,
	[Class] [int] NULL,
 CONSTRAINT [PK__Staff__3214EC07CCF809A0] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([ClassId], [ClassName]) VALUES (1, N'Class1')
INSERT [dbo].[Classes] ([ClassId], [ClassName]) VALUES (2, N'Class2')
INSERT [dbo].[Classes] ([ClassId], [ClassName]) VALUES (3, N'Class3')
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
INSERT [dbo].[Courses] ([Id], [CourseName]) VALUES (N'GAMB', N'Gambling')
INSERT [dbo].[Courses] ([Id], [CourseName]) VALUES (N'GMTH', N'GameTheory')
INSERT [dbo].[Courses] ([Id], [CourseName]) VALUES (N'HIST', N'History')
GO
INSERT [dbo].[CourseTeachers] ([FK_CourseId], [FK_StaffId]) VALUES (N'HIST', 1)
INSERT [dbo].[CourseTeachers] ([FK_CourseId], [FK_StaffId]) VALUES (N'GMTH', 2)
INSERT [dbo].[CourseTeachers] ([FK_CourseId], [FK_StaffId]) VALUES (N'GAMB', 5)
GO
INSERT [dbo].[Enrollments] ([FK_CourseId], [FK_StudentId], [FK_ClassIdFromCourses]) VALUES (N'GAMB', NULL, 1)
INSERT [dbo].[Enrollments] ([FK_CourseId], [FK_StudentId], [FK_ClassIdFromCourses]) VALUES (N'GMTH', NULL, 2)
INSERT [dbo].[Enrollments] ([FK_CourseId], [FK_StudentId], [FK_ClassIdFromCourses]) VALUES (N'HIST', NULL, 3)
GO
SET IDENTITY_INSERT [dbo].[Grades] ON 

INSERT [dbo].[Grades] ([Id], [StudentId_FK], [CourseId_FK], [Grade], [DateSet]) VALUES (1, 2, N'GAMB', 4, CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Grades] ([Id], [StudentId_FK], [CourseId_FK], [Grade], [DateSet]) VALUES (2, 4, N'GAMB', 3, CAST(N'2023-12-03' AS Date))
INSERT [dbo].[Grades] ([Id], [StudentId_FK], [CourseId_FK], [Grade], [DateSet]) VALUES (3, 3, N'GMTH', 2, CAST(N'2023-12-01' AS Date))
INSERT [dbo].[Grades] ([Id], [StudentId_FK], [CourseId_FK], [Grade], [DateSet]) VALUES (4, 5, N'HIST', 5, CAST(N'2000-01-10' AS Date))
INSERT [dbo].[Grades] ([Id], [StudentId_FK], [CourseId_FK], [Grade], [DateSet]) VALUES (5, 6, N'HIST', 5, CAST(N'2000-01-10' AS Date))
SET IDENTITY_INSERT [dbo].[Grades] OFF
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (1, N'Pelle', N'Svanslös', N'199010101234', N'Botkyrka', N'0893210392', N'Teacher', 1)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (2, N'John', N'Smith', N'195601010987', N'Tyresö', N'7418529632', N'Principal', NULL)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (3, N'Maggie', N'Smith', N'194710305678', N'London', N'7894561203', N'Teacher', 2)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (4, N'George', N'Bentley', N'123412341234', N'Någonstans', N'012393213', N'Administrator', NULL)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (5, N'Mary', N'HadALamb', N'987609121234', N'France', N'8527419632', N'Teacher', 3)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (6, N'Ben', N'Kingsley', N'195848591234', N'Hawaii', N'0874956254', N'Administrator', NULL)
INSERT [dbo].[Staff] ([Id], [StaffFirstName], [StaffLastName], [SSN], [Address], [PhoneNo], [Role], [Class]) VALUES (8, N'Anders', N'Andersson', N'200306184444', N'Täby', N'0659874123', N'Teacher', NULL)
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (2, N'Pelle', N'Petersson', N'191712091234', N'Växjö', N'0129384756', 1)
INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (3, N'Sven-Göran', N'Göransson', N'200109481234', N'Hallunda', N'0785214563', 2)
INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (4, N'John', N'Lennon', N'192506154563', N'Liverpool', N'0978565984', 1)
INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (5, N'Doris', N'Fish', N'200612149876', N'Pacfic Ocean', N'0684597812', 3)
INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (6, N'Wanda', N'Fish', N'200612144587', N'Pacific Ocean', N'0569857412', 3)
INSERT [dbo].[Students] ([Id], [StudentFirstName], [StudentLastName], [SSN], [Address], [PhoneNo], [Class]) VALUES (14, N'Chris', N'Cumberbatch', N'175002056666', N'Frankfurt', N'0896574123', 3)
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
ALTER TABLE [dbo].[CourseTeachers]  WITH CHECK ADD FOREIGN KEY([FK_CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[CourseTeachers]  WITH CHECK ADD FOREIGN KEY([FK_StaffId])
REFERENCES [dbo].[Staff] ([Id])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([FK_ClassIdFromCourses])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([FK_CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD FOREIGN KEY([FK_StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([CourseId_FK])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[Grades]  WITH CHECK ADD FOREIGN KEY([StudentId_FK])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_StaffClassId] FOREIGN KEY([Class])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_StaffClassId]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_ClassId] FOREIGN KEY([Class])
REFERENCES [dbo].[Classes] ([ClassId])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_ClassId]
GO

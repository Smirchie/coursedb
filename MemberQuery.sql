/*ALTER PROCEDURE InsertOrUpdateMember
@Id int,
@FirstName nvarchar(32),
@MiddleName nvarchar(32),
@LastName nvarchar(32),
@Sex bit,
@Birthday date,
@Phone nvarchar(11),
@Email nvarchar(32)
AS
BEGIN
	IF(@Id=-1)
	BEGIN
		INSERT INTO Член_Партии(Имя, Отчество, Фамилия, Пол, Дата_Рождения, Телефон, Электронная_Почта) VALUES (@FirstName,@MiddleName,@LastName,@Sex,@Birthday,@Phone,@Email)
	END
	ELSE
	BEGIN
		UPDATE Член_Партии
		SET
			Имя = @FirstName,
			Фамилия = @LastName,
			Отчество = @MiddleName,
			Пол = @Sex,
			Дата_Рождения = @Birthday,
			Телефон = @Phone,
			Электронная_Почта = @Email
		WHERE
			Идентификатор_Члена_Партии = @Id
	END
END
ALTER PROCEDURE InsertOrUpdateEvent
@Id int,
@Date date,
@Desc nvarchar(256)
AS
BEGIN
	IF(@Id=-1)
	BEGIN
		INSERT INTO Событие(Дата_Проведения,Описание) VALUES (@Date, @Desc)
	END
	ELSE
	BEGIN
		UPDATE Событие
		SET
			Дата_Проведения = @Date,
			Описание = @Desc
		WHERE
			Идентификатор_События = @Id
	END
END

CREATE PROCEDURE DeleteFromEvent
@Id int
AS
BEGIN
DELETE FROM Событие WHERE Идентификатор_События = @Id
END

ALTER PROCEDURE LinkEventToMember
@EventId int,
@MemberId int
AS
BEGIN
	INSERT INTO [Связь Член_Партии Событие](Идентификатор_События,Идентификатор_Члена_Партии) VALUES (@EventId,@MemberId)
END

ALTER PROCEDURE DeleteDuplicatesFromRelation
AS
BEGIN
WITH cte AS (
    SELECT 
        Идентификатор_События, 
        Идентификатор_Члена_Партии,
        ROW_NUMBER() OVER (
            PARTITION BY 
                Идентификатор_События, 
				Идентификатор_Члена_Партии
            ORDER BY 
                Идентификатор_События, 
				Идентификатор_Члена_Партии
        ) row_num
     FROM 
        [Связь Член_Партии Событие]
)
DELETE FROM cte
WHERE row_num > 1;
END

SELECT Идентификатор_Члена_Партии
FROM [Связь Член_Партии Событие]
WHERE Идентификатор_События = 9

ALTER PROCEDURE InsertOrUpdateProject
@Name nvarchar(32),
@Desc nvarchar(256),
@Finance money,
@Id int,
@MemberId int
AS
BEGIN
	IF(@Id=-1)
	BEGIN
		INSERT INTO Проект(Название,Описание,Финансирование,Идентификатор_Члена_Партии) VALUES (@Name, @Desc,@Finance,@MemberId)
	END
	ELSE
	BEGIN
		UPDATE Проект
		SET
			Название = @Name,
			Описание = @Desc,
			Финансирование = @Finance,
			Идентификатор_Члена_Партии = @MemberId
		WHERE
			Идентификатор_Проекта = @Id
	END
END

CREATE PROCEDURE DeleteFromProject
@Id int
AS
BEGIN
DELETE FROM Проект WHERE Идентификатор_Проекта = @Id
END

SELECT Идентификатор_Члена_Партии, Фамилия
FROM Член_Партии
WHERE Идентификатор_Члена_Партии = 3

ALTER TABLE Отделение
ADD CONSTRAINT uq_MemberID UNIQUE (Идентификатор_Члена_Партии);

ALTER TABLE Отделение
ADD CONSTRAINT FK_Отделение_Член_Партии
FOREIGN KEY (Идентификатор_Члена_Партии)
REFERENCES Член_Партии(Идентификатор_Члена_Партии)

CREATE PROCEDURE InsertOrUpdateDept
@RegionId int,
@Address nvarchar(128),
@Site nvarchar(32),
@Phone nvarchar(11),
@Id int,
@MemberId int
AS
BEGIN
	IF(@Id=-1)
	BEGIN
		INSERT INTO Отделение(Номер_Региона,Адрес, Сайт, Телефон, Идентификатор_Члена_Партии) VALUES (@RegionId, @Address, @Site, @Phone,@MemberId)
	END
	ELSE
	BEGIN
		UPDATE Отделение
		SET
			Номер_Региона = @RegionId,
			Адрес = @Address,
			Сайт = @Site,
			Телефон = @Phone,
			Идентификатор_Члена_Партии = @MemberId
		WHERE
			Идентификатор_Отделения = @Id
	END
END

CREATE PROCEDURE DeleteFromDept
@Id int
AS
BEGIN
DELETE FROM Отделение WHERE Идентификатор_Отделения = @Id
END*/

ALTER PROCEDURE InsertOrUpdateAppeal
@LastName nvarchar(32),
@FirstName nvarchar(32),
@MiddleName nvarchar(32),
@Sex bit,
@Text nvarchar(2048),
@Phone nvarchar(11),
@Email nvarchar(32),
@Id int,
@DeptId int
AS
BEGIN
	IF(@Id=-1)
	BEGIN
		INSERT INTO Обращение(Фамилия, Имя, Отчество, Пол, Текст, Электронная_Почта,Идентификатор_Отделения, Телефон) VALUES (@LastName,@FirstName, @MiddleName, @Sex, @Text,@Email,@DeptId,@Phone)
	END
	ELSE
	BEGIN
		UPDATE Обращение
		SET
			Фамилия = @LastName,
			Имя = @FirstName,
			Отчество = @MiddleName,
			Пол = @Sex,
			Текст = @Text,
			Телефон = @Phone,
			Электронная_Почта = @Email,
			Идентификатор_Отделения = @DeptId
		WHERE
			Идентификатор_Обращения = @Id
	END
END

CREATE PROCEDURE DeleteFromAppeal
@Id int
AS
BEGIN
DELETE FROM Обращение WHERE Идентификатор_Обращения = @Id
END
CREATE DATABASE UkrposhtaDb;

USE UkrposhtaDb;

CREATE TABLE Company
(
    Id INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL
);

CREATE TABLE Position
(
    Id INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL
);

CREATE TABLE Department
(
    Id INT IDENTITY PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL
);

CREATE TABLE Employee
(
    Id INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(255),
    [Address] NVARCHAR(255),
    Phone NVARCHAR(20),
    BirthDate DATE,
    HireDate DATE,
    Salary DECIMAL(10, 2),
    DepartmentId INT REFERENCES Department(Id),    -- Зовнішній ключ до таблиці "Department"
    CompanyId INT REFERENCES Company(Id),      -- Зовнішній ключ до таблиці "Company"
    PositionId INT REFERENCES Position(Id)      -- Зовнішній ключ до таблиці "Position"
);


-- To Insert an Employee Record
CREATE PROCEDURE spAddEmployee
    @FullName NVARCHAR(255),
    @Address NVARCHAR(255),
    @Phone NVARCHAR(20),
    @BirthDate DATE,
    @HireDate DATE,
    @Salary DECIMAL(10, 2),
    @DepartmentId INT,
    @CompanyId INT,
    @PositionId INT
AS
BEGIN
    INSERT INTO Employee (FullName, [Address], Phone, BirthDate, HireDate, Salary, DepartmentId, CompanyId, PositionId)
    VALUES (@FullName, @Address, @Phone, @BirthDate, @HireDate, @Salary, @DepartmentId, @CompanyId, @PositionId);
END;

-- To Update an Employee Record
CREATE PROCEDURE spUpdateEmployee
    @Id INT,
    @FullName NVARCHAR(255),
    @Address NVARCHAR(255),
    @Phone NVARCHAR(20),
    @BirthDate DATE,
    @HireDate DATE,
    @Salary DECIMAL(10, 2),
    @DepartmentId INT,
    @CompanyId INT,
    @PositionId INT
AS
BEGIN
    UPDATE Employee
    SET FullName = @FullName,
        [Address] = @Address,
        Phone = @Phone,
        BirthDate = @BirthDate,
        HireDate = @HireDate,
        Salary = @Salary,
        DepartmentId = @DepartmentId,
        CompanyId = @CompanyId,
        PositionId = @PositionId
    WHERE Id = @Id;
END;

-- To Delete an Employee Record
CREATE PROCEDURE spDeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM Employee
    WHERE Id = @Id;
END;

CREATE PROCEDURE spGetAllCompany       
AS        
BEGIN        
    SELECT *        
    FROM Company     
    ORDER BY Id   
END;  

CREATE PROCEDURE spGetAllPosition  
AS        
BEGIN        
    SELECT *        
    FROM Position     
    ORDER BY Id   
END;  

CREATE PROCEDURE spGetAllDepartment       
AS        
BEGIN        
    SELECT *        
    FROM Department     
    ORDER BY Id   
END;  


CREATE PROCEDURE spGetAllEmployee
    @SearchString NVARCHAR(255) = NULL,
    @CompanyId INT = NULL,
    @DepartmentId INT = NULL,
    @PositionId INT = NULL
AS
BEGIN
    IF @SearchString IS NULL AND @CompanyId IS NULL AND @DepartmentId IS NULL AND @PositionId IS NULL
    BEGIN
        SELECT 
            E.*,
            D.[Name] AS DepartmentName,
            C.[Name] AS CompanyName,
            P.[Name] AS PositionName
        FROM Employee E
        INNER JOIN Department D ON E.DepartmentId = D.Id
        INNER JOIN Company C ON E.CompanyId = C.Id
        INNER JOIN Position P ON E.PositionId = P.Id
        ORDER BY E.Id;
    END
    ELSE
    BEGIN
        SELECT 
            E.*,
            D.[Name] AS DepartmentName,
            C.[Name] AS CompanyName,
            P.[Name] AS PositionName
        FROM Employee E
        INNER JOIN Department D ON E.DepartmentId = D.Id
        INNER JOIN Company C ON E.CompanyId = C.Id
        INNER JOIN Position P ON E.PositionId = P.Id
        WHERE
            (
                (@SearchString IS NULL OR
                (E.FullName LIKE '%' + @SearchString + '%' OR
                 E.Address LIKE '%' + @SearchString + '%' OR
                 E.Phone LIKE '%' + @SearchString + '%' OR
                 D.[Name] LIKE '%' + @SearchString + '%' OR
                 C.[Name] LIKE '%' + @SearchString + '%' OR
                 P.[Name] LIKE '%' + @SearchString + '%'))
                OR
                (@SearchString IS NOT NULL AND
                (E.FullName LIKE @SearchString + '%' OR
                 E.Address LIKE @SearchString + '%' OR
                 E.Phone LIKE @SearchString + '%' OR
                 D.[Name] LIKE @SearchString + '%' OR
                 C.[Name] LIKE @SearchString + '%' OR
                 P.[Name] LIKE @SearchString + '%'))
            )
            AND
            (
                (@CompanyId IS NULL OR E.CompanyId = @CompanyId) AND
                (@DepartmentId IS NULL OR E.DepartmentId = @DepartmentId) AND
                (@PositionId IS NULL OR E.PositionId = @PositionId)
            )
        ORDER BY E.Id;
    END
END;


-- Додавання даних до таблиці "Company"
INSERT INTO Company ([Name]) VALUES ('Google');
INSERT INTO Company ([Name]) VALUES ('Ukrposhta');
INSERT INTO Company ([Name]) VALUES ('Apple');

-- Додавання даних до таблиці "Position"
INSERT INTO Position ([Name]) VALUES ('CEO');
INSERT INTO Position ([Name]) VALUES ('Manager');
INSERT INTO Position ([Name]) VALUES ('Programmer');

-- Додавання даних до таблиці "Department"
INSERT INTO Department ([Name]) VALUES ('Logistics');
INSERT INTO Department ([Name]) VALUES ('Analytics');
INSERT INTO Department ([Name]) VALUES ('Development');

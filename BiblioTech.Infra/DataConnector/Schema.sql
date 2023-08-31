CREATE DATABASE BiblioTech;
GO

USE BiblioTech;
GO

CREATE TABLE Books (
    id VARCHAR(36) PRIMARY KEY DEFAULT NEWID(),
    ISBN VARCHAR(13) UNIQUE NOT NULL,
    title VARCHAR(255) NOT NULL,
    year_publication INT NOT NULL,
    description TEXT,
    quantity INT NOT NULL,
    publishing VARCHAR(50)
);

CREATE TABLE Users (
    id VARCHAR(36) PRIMARY KEY DEFAULT NEWID(),
    name VARCHAR(100) NOT NULL,
    CPF VARCHAR(11) UNIQUE NOT NULL,
    passwordHash VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    telephone VARCHAR(20) NOT NULL,
    adress VARCHAR(255) NOT NULL,
	role VARCHAR(50) NOT NULL
);

CREATE TABLE Loans (
    id VARCHAR(36) PRIMARY KEY DEFAULT NEWID(),
    user_email VARCHAR(100) NOT NULL,
    employee_email VARCHAR(100) NOT NULL,
    loan_Date  DATE NOT NULL,
    FOREIGN KEY (user_email) REFERENCES Users(email),
    FOREIGN KEY (employee_email) REFERENCES Users(email)
);

CREATE TABLE Reserves (
    id VARCHAR(36) PRIMARY KEY DEFAULT NEWID(),
    user_email VARCHAR(100) NOT NULL,
    book_id VARCHAR(36) NOT NULL,
    employee_email VARCHAR(100) NOT NULL,
    reserve_Date DATE NOT NULL,
    status_Reserve VARCHAR(20) CHECK (status_Reserve IN ('active', 'canceled')) DEFAULT 'active',
    FOREIGN KEY (user_email) REFERENCES Users(email),
    FOREIGN KEY (employee_email) REFERENCES Users(email),
    FOREIGN KEY (book_id) REFERENCES Books(id)
);

CREATE TABLE BookLoans (
    id VARCHAR(36) PRIMARY KEY DEFAULT NEWID(),
	loan_id VARCHAR(36) NOT NULL,
    book_id VARCHAR(36) NOT NULL,
	devolution_Date DATE NOT NULL,
	loan_Status VARCHAR(20) CHECK (loan_Status IN ('pending', 'returned')) DEFAULT 'pending',
	FOREIGN KEY (loan_id) REFERENCES Loans(id),
    FOREIGN KEY (book_id) REFERENCES Books(id)
);
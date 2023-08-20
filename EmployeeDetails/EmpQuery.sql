CREATE TABLE EmpDetails(
	empId INT NOT NULL PRIMARY KEY IDENTITY,
	name varchar(150) NOT NULL,
	email varchar(150) NOT NULL,
	department varchar(150) NOT NULL,
	phoneNumber varchar(15) NOT NULL,
)

INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES ('Karthik', 'karthik@gmail.com', 'Development', '1234567890')
INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES ('Arun', 'arun@gmail.com', 'Support', '4567890123')
INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES ('Godlin', 'godlin@gmail.com', 'Quality', '7890123456')
INSERT INTO EmpDetails(name, email, department, phoneNumber) VALUES ('Vignesh', 'vignesh@gmail.com', 'Testing', '0123789456')

SELECT * FROM EmpDetails

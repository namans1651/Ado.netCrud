select * from Company
select * from Shift
select * from Employees
select * from Country
select * from State
select * from city



select * from ShiftEndReasons

alter proc sp_GetEmployee
as
Begin
select [id], [name], [mobileNo], [email], [address],c.c_name,s.shift_name,c1.CountryName,st.StateName,ct.CityName,sss.Reason
from [dbo].[Employees]
join Company c  on Employees.FK_company_id=c.company_id
join Shift s on Employees.FK_shift_id = s.shift_id
join Country c1 on Employees.FK_Country_Id = c1.CountryID
join State st on Employees.FK_State_Id = st.StateID
join City ct on Employees.FK_City_Id = ct.CityID
join ShiftEndReasons sss on Employees.FK_ShiftReasonId = sss.ShiftEndReasonId
end

exec sp_getEmployee


create proc sp_getShiftEndReason
as
begin
select ShiftEndReasonId,Reason 
from ShiftEndReasons
end




CREATE TABLE ShiftEndReasons
(
ShiftEndReasonId INT PRIMARY KEY IDENTITY,
Reason VARCHAR(250)    
);

INSERT INTO ShiftEndReasons (Reason) VALUES ('Shift completed successfully'),
('Employee reached the end of their scheduled hours'),('Early departure'),('Shift terminated')
,('Employee took an unscheduled break and did not return');




alter proc sp_AddEmployee(@name nvarchar(30),@mobile nvarchar(20),@email nvarchar(30),@address nvarchar(30),@shiftid int,
@fkcompId int,@fkCountryid int , @fkStateid int,@fkCityid int,@shiftendreasonid int,@shiftEndTime time)
as
begin
insert into Employees(name,mobileNo,email,address,FK_shift_id,FK_company_id,FK_Country_Id,FK_State_Id,FK_City_Id,FK_ShiftReasonId,ShiftEndTime)
values(@name,@mobile,@email,@address,@shiftid,@fkcompId,@fkCountryid,@fkStateid,@fkCityid,@shiftendreasonid,@shiftEndTime)
end


exec sp_GetAllShift

alter proc sp_getEmployeeById(@id int)
as
Begin
select [id], [name], [mobileNo], [email], [address], [FK_shift_id], [FK_company_id],FK_Country_Id,FK_State_Id,FK_City_Id
from Employees
where id = @id
end


alter proc sp_UpdateEmployee
(@id int,@name nvarchar(30),@mob_number nvarchar(20),@email nvarchar(30),@address nvarchar(20),@shiftId int,@fkcompId int,
@fkCountryId int, @fkStateId int , @fkCityId int,@fkShiftReasonId int , @shiftendtime time)
as
begin
update Employees
set name = @name,mobileNo = @mob_number,email= @email,address = @address,FK_shift_id=@shiftId,FK_company_id=@fkcompId,
FK_Country_Id = @fkCountryId,FK_State_Id = @fkStateId, FK_City_Id = @fkCityId,FK_ShiftReasonId  = @fkShiftReasonId,ShiftEndTime = @shiftendtime
where id=@id
end


CREATE TABLE Country (
CountryID INT IDENTITY(1,1) PRIMARY KEY,
CountryName NVARCHAR(100) 
);

CREATE TABLE State (
StateID INT IDENTITY(1,1) PRIMARY KEY,
StateName NVARCHAR(100),
FK_CountryID INT,
FOREIGN KEY (FK_CountryID) REFERENCES Country(CountryID)
);

CREATE TABLE City (
CityID INT IDENTITY(1,1) PRIMARY KEY,
CityName NVARCHAR(100) ,
FK_StateID INT ,
FOREIGN KEY (FK_StateID) REFERENCES State(StateID)
);

INSERT INTO Country (CountryName) 
VALUES 
('India'), 
('United States');

INSERT INTO State (StateName, FK_CountryID) 
VALUES 
('Maharashtra', 1), 
('Uttar Pradesh', 1), 
('Haryana', 1);

INSERT INTO State (StateName, FK_CountryID) 
VALUES 
('California', 2), 
('Texas', 2), 
('Florida', 2);


INSERT INTO City (CityName, FK_StateID) 
VALUES 
('Mumbai', 1), 
('Pune', 1), 
('Nagpur', 1),
('Lucknow', 2), 
('Meerut', 2), 
('Muzaffarnagar', 2),
('Gurgaon', 3), 
('Panipat', 3), 
('karnaal', 3);

INSERT INTO City (CityName, FK_StateID) 
VALUES 

('Los Angeles', 4), 
('San Francisco', 4), 
('San Diego', 4),
('Houston', 5), 
('Dallas', 5), 
('Austin', 5),
('Miami', 6), 
('Orlando', 6), 
('Tampa', 6);












INSERT INTO Country (Country_Name, isActive, isDelete)
VALUES ('India', 1, 0),
       ('United States', 1, 0);

	   
INSERT INTO State (StateName, FK_Country_Id, isActive, isDelete)
VALUES ('Karnataka', 1, 1, 0),
       ('Maharashtra', 1, 1, 0),
       ('Tamil Nadu', 1, 1, 0);

	   
INSERT INTO State (StateName, FK_Country_Id, isActive, isDelete)
VALUES ('California', 2, 1, 0),
       ('Texas', 2, 1, 0),
       ('Florida', 2, 1, 0);





CREATE PROCEDURE sp_EmployeeCountryState
    @EmployeeId INT,
    @CountryId INT,
    @StateId INT
AS
BEGIN
update Employees set FK_Country_Id=@CountryId,FK_State_ID = @StateId
      where id = @EmployeeId

END

alter proc sp_GetEmployee
as
Begin
select [id], [name], [mobileNo], [email], [address], c.c_name,s.shift_name,c1.CountryName,st.StateName,ct.CityName
from [dbo].[Employees]
join Company c  on Employees.FK_company_id=c.company_id
join Shift s on Employees.FK_shift_id = s.shift_id
join Country c1 on Employees.FK_Country_Id = c1.CountryID
join State st on Employees.FK_State_Id = st.StateID
join City ct on Employees.FK_City_Id = ct.CityID
end
[id], [name], [mobileNo], [email], [address], [FK_shift_id], [FK_company_id], [FK_Country_Id], [FK_State_Id], [FK_City_Id], [FK_ShiftReasonId], [ShiftEndTime]
exec sp_GetEmployee


alter proc sp_jsonAdd(@CompanyJson nvarchar(max))
as 
begin
insert into Company(c_name,c_address,c_pincode,c_state,c_city,c_contactNo)
select Name,Address,PinCode,State,City,ContactNumber
from OpenJson(@CompanyJson) 
with( 
Name nvarchar(40) '$.Name',
Address nvarchar(40) '$.Address',
PinCode Nvarchar(10) '$.PinCode',
State nvarchar(30) '$.State',
City nvarchar(30) '$.City',
ContactNumber nvarchar(15) '$.ContactNumber'
)
end


alter proc sp_jsonAdd(@CompanyJson nvarchar(max))
as 
begin

create table #TempCompanyTable (
Name nvarchar(40),
Address nvarchar(40),
PinCode Nvarchar(10),
State nvarchar(30),
City nvarchar(30),
ContactNumber nvarchar(15)
)
insert into #TempCompanyTable(Name, Address, PinCode, State, City, ContactNumber)
--insert into Company(c_name,c_address,c_pincode,c_state,c_city,c_contactNo)

select Name,Address,PinCode,State,City,ContactNumber
from OpenJson(@CompanyJson) 
with( 
Name nvarchar(40) '$.Name',
Address nvarchar(40) '$.Address',
PinCode Nvarchar(10) '$.PinCode',
State nvarchar(30) '$.State',
City nvarchar(30) '$.City',
ContactNumber nvarchar(15) '$.ContactNumber'
)


insert into Company(c_name,c_address,c_pincode,c_state,c_city,c_contactNo)
select Name,Address,PinCode,State,City,ContactNumber
from #TempCompanyTable
end;





alter Proc spGetCountries
AS
Begin
SELECT CountryId, CountryName 
FROM Country
END

alter Proc spGetStates
@CountryId INT
AS
Begin
SELECT StateID, StateName,FK_CountryId
FROM State 
Where FK_CountryId = @CountryId;
END

alter Proc spGetCity
@StateId INT
AS
Begin
SELECT CityID,CityName,FK_StateID
FROM City 
Where FK_StateID = @StateId;
END

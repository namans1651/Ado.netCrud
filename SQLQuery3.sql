
use NewDatabase
select * from  [dbo].[Company]
select * from [dbo].[Employees]
select * from [dbo].[Shift]
select * from Country
select * from State
truncate table country

truncate table shift
drop table Company
drop table Shift
ALTER TABLE Shift
ALTER COLUMN end_time DATETIME;

create table Company(
company_id int primary key identity(1,1),
c_name nvarchar(30),
c_address nvarchar(40),
c_pincode nvarchar(10),
c_state nvarchar(30),
c_city nvarchar(30),
c_contactNo nvarchar(15)
)

create table Employees(
id int primary key identity(1,1),
name nvarchar(50),
mobileNo nvarchar(15),
email nvarchar(30),
address nvarchar(40),
FK_shift_id int ,
FK_company_id int 
)

create table Shift(
shift_id int primary key identity(1,1),
shift_name nvarchar(20),
start_time datetime,
end_time datetime,
fk_company_id int,
imagePath nvarchar(max)
)






-- Add the CompanyId column to the Shift table
ALTER TABLE Shift
ADD CompanyId INT;

-- Add the foreign key constraint
ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Company
FOREIGN KEY (fk_company_id) REFERENCES Company(company_id);



alter proc sp_Addcompany(@name nvarchar(30),@address nvarchar(30),@pincode nvarchar(10),@state nvarchar(20),@city nvarchar(20),@contact nvarchar(15))
as
Begin
insert into Company(c_name,c_address,c_pincode,c_state,c_city,c_contactNo)
values(@name,@address,@pincode,@state,@city,@contact)
end

alter proc sp_GetCompany
as 
Begin
select company_id ,c_name ,c_address ,c_pincode ,c_state ,c_city ,c_contactNo
from Company
end


alter proc sp_updateCompany(@id int,@name nvarchar(30),@address nvarchar(30),@pincode nvarchar(10),@state nvarchar(20),@city nvarchar(20),@contact nvarchar(15))
as
begin
update Company
set c_name = @name,c_address = @address,c_pincode= @pincode,c_state = @state,c_city=@city,c_contactNo = @contact
where company_id=@id
end

alter proc sp_getCompanyById(@id int)
as
Begin
select company_id,c_name,c_address,c_pincode,c_state,c_city,c_contactNo
from Company
where company_id = @id
end

alter proc sp_DeleteCompany(@id int)
as
begin
delete from Company
where company_id=@id
end

---------------------------------------------------------------------------------------

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


alter proc sp_getEmployeeById(@id int)
as
Begin
select [id], [name], [mobileNo], [email], [address], [FK_shift_id], [FK_company_id]
from Employees
where id = @id
end

alter proc sp_UpdateEmployee
(@id int,@name nvarchar(30),@mob_number nvarchar(20),@email nvarchar(30),@address nvarchar(20),@shiftId int,@fkcompId int)
as
begin
update Employees
set name = @name,mobileNo = @mob_number,email= @email,address = @address,FK_shift_id=@shiftId,FK_company_id=@fkcompId
where id=@id
end


EXEC sp_AddEmployee @name = 'Rohit', @mobile = '1234567890', @email = 'aa@example.com', @address = '123 ABC', @shiftid = 1, @fkcompId = 1;


alter proc sp_AddEmployee(@name nvarchar(30),@mobile nvarchar(20),@email nvarchar(30),@address nvarchar(30),@shiftid int,
@fkcompId int,@fkCountryid int , @fkStateid int,@fkCityid int )
as
begin
insert into Employees(name,mobileNo,email,address,FK_shift_id,FK_company_id,FK_Country_Id,FK_State_Id,FK_City_Id)
values(@name,@mobile,@email,@address,@shiftid,@fkcompId,@fkCountryid,@fkStateid,@fkCityid)
end


alter proc sp_DeleteEmployee(@id int)
as 
begin
delete from Employees
where id = @id
end

------------------------------------------------------------------------------------------------

alter proc sp_GetAllShift
as
Begin
Select shift_id,shift_name,start_time,end_time,c.c_name,ImagePath
from Shift
inner join Company c on shift.fk_company_id = c.company_id
end


alter proc sp_AddTheShift(@name nvarchar(30),@start_Time time,@end_Time time,@compId int,@imagepath nvarchar(max),@Id int output
)
as
Begin
insert into Shift([shift_name], [start_time], [end_time], [fk_company_id], [imagePath])
values(@name,@start_Time,@end_Time,@compId,@imagepath)
set @Id = SCOPE_IDENTITY();
end


alter proc sp_getShiftById(@id int)
as
Begin
select [shift_id], [shift_name], [start_time], [end_time], [fk_company_id],ImagePath
from Shift
where shift_id = @id
end
select* from Shift

alter proc sp_UpdateShift
(@id int,@name nvarchar(30),@startTime Datetime,@endTime datetime,@fk_compId int,@imagePath nvarchar(max))
as
begin
update Shift
set  [shift_name] = @name, [start_time] =@startTime, [end_time]=@endTime, [fk_company_id]=@fk_compId,[ImagePath]=@imagePath
where shift_id=@id
end



alter proc sp_DeleteShift(@id int)
as 
begin
delete from Shift
where shift_id = @id
end





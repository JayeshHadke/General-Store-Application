namespace My_Store_API
{
	public class DatabaseScript
	{
		/*
		create database myStore
		use myStore
		create table users(userId int primary key identity(1,1), userName varchar(40) not null,password varchar(20) not null,userRole varchar(20) not null check(userRole = 'customer' or userRole='manager' or userRole = 'admin'),userPhoneNo bigint ,userEmail varchar(40));
		insert into users values('jayesh','jay','admin',9823903784,'jayeshhadkejrh@gmail.com');
		select * from users
		create table items(itemId int primary key identity(1,1),itemName varchar(30) not null,itemPrice float not null,quantity int not null);
		select * from items;
		 */
	}
}

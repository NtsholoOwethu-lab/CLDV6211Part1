--DATABASE CREATION
USE master
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'eventeasedatabase')
DROP DATABASE EventEasedatabase
CREATE DATABASE EventEasedatabase


-- Create Venue table 
CREATE TABLE Venue (
	VenueId INT IDENTITY(1,1) PRIMARY KEY, 
	VenueName NVARCHAR(100) NOT NULL, 
	Location NVARCHAR(100) UNIQUE NOT NULL, 
	Capacity INT NOT NULL, 
	ImageUrl NVARCHAR(100) NOT NULL 
);


-- Create Event table 
CREATE TABLE EventDetails (
	EventId INT IDENTITY(1,1) PRIMARY KEY, 
	EventName NVARCHAR(100) NOT NULL,
	EventDate DATE NOT NULL, 
	Descriptions NVARCHAR(255) NOT NULL, 
        VenueId INT,  -- Add the column VenueId
        CONSTRAINT FK_EventDetails_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueId)

);


--Create Bookings table (linking Students and Companies) 
CREATE TABLE Bookings (  
	BookingId INT IDENTITY(1,1) PRIMARY KEY, 
	VenueId INT NOT NULL, 
	EventId INT NOT NULL, 
	BookingDate DATE NOT NULL, 
	CONSTRAINT FK_Bookings_Venue FOREIGN KEY (VenueId) REFERENCES Venue(VenueId),
    CONSTRAINT FK_Bookings_EventDetails FOREIGN KEY (EventId) REFERENCES EventDetails(EventId)

	 
);




--Insert sample data 
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl) 
VALUES ('Smith Hall', 'Sandton', '20000', 'erfasgaer;grhaentgusdrhuihaeohahhahaeruia'),
('iie msa hall', 'ruimsig', '500', 'lkwewahg;argo[ahgargauirgha;huherthuioawerht');




INSERT INTO EventDetails (EventName,EventDate, Descriptions, VenueId )
VALUES ('Summer Ball', '2025-06-01', 'A fun ball in the summer', 1), 
('Movie night', '2025-06-22', 'A fun night with friends',2);

INSERT INTO Bookings (VenueId, EventId, BookingDate) 
VALUES (1, 1, '2025-09-01' ), 
(2, 2, '2015-06-15' ); 

SELECT * FROM Venue;
SELECT * FROM EventDetails;
SELECT * FROM Bookings;

--drop table Internships;
--drop table Students;
--drop table Company;

-- Below codes work for Azure database to drop tables
--drop table Bookings;
--drop table [dbo].Venue;
--drop table [dbo].Event;
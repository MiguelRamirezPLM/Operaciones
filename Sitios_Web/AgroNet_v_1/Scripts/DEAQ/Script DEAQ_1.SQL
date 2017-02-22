
-- ************************************ DEAQ ************************************


CREATE TABLE ContentTypes
( 
	ContentTypeId        tinyint  NOT NULL ,
	ContentType          varchar(50)  NOT NULL ,
	Active               bit  NOT NULL 
)
go

ALTER TABLE ContentTypes
	ADD CONSTRAINT XPKParticipatProductsType PRIMARY KEY  CLUSTERED (ContentTypeId ASC)
go

----- 

CREATE TABLE NewProducts
( 
	ProductId            int  NOT NULL ,
	PharmaFormId         int  NOT NULL ,
	DivisionId           int  NOT NULL ,
	CategoryId           int  NOT NULL ,
	EditionId            int  NOT NULL 
)
go

ALTER TABLE NewProducts
	ADD CONSTRAINT XPKNewProducts PRIMARY KEY  CLUSTERED (ProductId ASC,PharmaFormId ASC,DivisionId ASC,CategoryId ASC,EditionId ASC)
go

-----

ALTER TABLE participantproducts
add   ContentTypeId tinyint  NULL 


ALTER TABLE ParticipantProducts
      ADD CONSTRAINT R_121 FOREIGN KEY (ContentTypeId) REFERENCES ContentTypes(ContentTypeId)
            ON DELETE NO ACTION
            ON UPDATE NO ACTION
go

-----

INSERT INTO [DEAQ].[dbo].[ContentTypes] ([ContentTypeId] ,[ContentType] ,[Active])
     VALUES (1, 'Nuevo', 1)
     
INSERT INTO [DEAQ].[dbo].[ContentTypes] ([ContentTypeId] ,[ContentType] ,[Active])
     VALUES (2, 'Con Cambio', 1)
     
INSERT INTO [DEAQ].[dbo].[ContentTypes] ([ContentTypeId] ,[ContentType] ,[Active])
     VALUES (3, 'Sin Cambio', 1)          

 
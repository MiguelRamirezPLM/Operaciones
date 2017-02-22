
-- ************************************ PLMUsers  ************************************

insert into tables (ApplicationId, Description, Active) Values (28 ,'ContentTypes',1) 
insert into tables (ApplicationId, Description, Active) Values (28 ,'DivisionInformation',1)
insert into tables (ApplicationId, Description, Active) Values (28 ,'MentionatedProducts',1)

-----

update tables  set Description=replace(Description,'DEAQ', '')
where ApplicationId = 28

IF OBJECT_ID('dbo.plm_spGetICDByGuides') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetICDByGuides

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetSpecialitiesByGuiesByICD
	
	   Description:	    Retrieves Specialities  by Guies and ICD 

	   EXECUTE          dbo.plm_spGetSpecialitiesByGuiesByICD @prefix
	
	*/
CREATE PROCEDURE dbo.plm_spGetICDByGuides

		 @prefix			varchar(15) = null,
		 @icdKey            varchar(5) = null
	
AS
BEGIN

	
    SELECT DISTINCT  
        
         I.[ICDId]
        ,I.[ICDKey]
        ,I.[SpanishDescription]
        ,I.[EnglishDescription]
        ,I.[Active]
      
    FROM dbo.[CodePrefixes] CP
            INNER JOIN DistributionTools DT
        ON CP.PrefixId=DT.PrefixId
            INNER JOIN CountryTools CT
        ON DT.CountryId=CT.CountryId
            INNER JOIN ElectronicInformation EI
        ON CT.ElectronicId=EI.ElectronicId
            INNER JOIN ICDTools IT
        ON EI.ElectronicId=IT.ElectronicId
             INNER JOIN ICD I
        ON IT.ICDId=I.ICDId
             INNER JOIN InformationTypes  TS
       ON EI.InfoTypeId=TS.InfoTypeId  
       
     WHERE  CP.PrefixName=@prefix 
             AND I.ICDKey like '%' + @icdKey + '%'
    
      
      RETURN @@ROWCOUNT
END

